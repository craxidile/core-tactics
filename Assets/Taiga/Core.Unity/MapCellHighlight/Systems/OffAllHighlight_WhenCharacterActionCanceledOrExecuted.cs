using System.Collections.Generic;
using Entitas;
using Taiga.Core.CharacterAction;

namespace Taiga.Core.Unity.MapCellHighlight
{
    internal class OffAllHighlight_WhenCharacterActionCanceledOrExecuted : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public OffAllHighlight_WhenCharacterActionCanceledOrExecuted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var mapCellEntities = game.GetGroup(GameMatcher.MapCell_Highlight);
            foreach (var mapCellEntity in mapCellEntities)
            {
                mapCellEntity.isMapCell_HighlightOff = true;
            }

            mapCellEntities = game.GetGroup(GameMatcher.MapCell_OverrideHighlight);
            foreach (var mapCellEntity in mapCellEntities)
            {
                mapCellEntity.isMapCell_HighlightOff = true;
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.AnyOf(
                    CharacterActionEvents.OnActionCanceled,
                    CharacterActionEvents.OnActionExecute
                )
            );
        }
    }
}
