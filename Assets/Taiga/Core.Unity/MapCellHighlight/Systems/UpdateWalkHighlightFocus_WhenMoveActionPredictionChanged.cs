using System.Collections.Generic;
using Entitas;
using Taiga.Core.CharacterAction.Move;
using Taiga.Core.Map;
using Taiga.Core.Unity.Audio;
using UnityEngine;

namespace Taiga.Core.Unity.MapCellHighlight
{
    internal class UpdateWalkHighlightFocus_WhenMoveActionPredictionChanged : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public UpdateWalkHighlightFocus_WhenMoveActionPredictionChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var selectionHighlightEntities = game
                .GetGroup(GameMatcher.MapCell_OverrideHighlight)
                .GetEntities();

            var preset = game.GetProvider<IMapCellHighlightPreset>();
            foreach (var entity in selectionHighlightEntities)
            {
                if (entity.hasMapCell_OverrideHighlight)
                {
                    entity.RemoveMapCell_OverrideHighlight();
                }
            }

            foreach (var entity in entities)
            {
                var highlightPosition = entity
                    .AsCharacterAction_Move(game)
                    .FocusPosition
                    .Value;

                var mapContext = game.AsMapContext();
                var cell = mapContext.GetMapCell(highlightPosition);
                var color = preset.GetColor(HighlightMode.Walk, highlight: true);
                cell.entity.ReplaceMapCell_OverrideHighlight(color, false);
            }

            Debug.Log($">>check_null<< {SoundEffectManager.Instance == null}");
            SoundEffectManager.Instance.PlaySoundEffect(SoundEffect.CursorActiveOnFloor);
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                CharacterActionMoveEvents.OnFocus.Added()
            );
        }
    }
}
