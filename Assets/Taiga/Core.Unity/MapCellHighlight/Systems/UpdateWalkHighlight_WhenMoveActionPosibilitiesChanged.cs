using System.Collections.Generic;
using Entitas;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterAction.Move;
using Taiga.Core.Map;

namespace Taiga.Core.Unity.MapCellHighlight
{
    internal class UpdateWalkHighlight_WhenMoveActionPosibilitiesChanged : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public UpdateWalkHighlight_WhenMoveActionPosibilitiesChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var preset = game.GetProvider<IMapCellHighlightPreset>();
            foreach (var entity in entities)
            {
                var highlightPositions = entity.AsCharacterAction_Move(game)
                    .PossiblePositions;

                var blockagePositions = entity.AsCharacterAction_Move(game)
                    .CharacterBlockages;

                var mapContext = game.AsMapContext();

                foreach (var highlightPosition in highlightPositions)
                {
                    var cell = mapContext.GetMapCell(highlightPosition);
                    var color = preset.GetColor(
                        HighlightMode.Walk,
                        highlight: false
                    );
                    cell.entity.ReplaceMapCell_Highlight(color);
                }

                foreach (var blockagePosition in blockagePositions)
                {
                    var cell = mapContext.GetMapCell(blockagePosition);
                    var color = preset.GetColor(
                        HighlightMode.Blockage,
                        highlight: false
                    );
                    cell.entity.ReplaceMapCell_Highlight(color);
                }
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.AsCharacterAction(game).ActionType == CharacterActionType.Move;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                CharacterActionEvents.OnActionCreated
            );
        }
    }
}
