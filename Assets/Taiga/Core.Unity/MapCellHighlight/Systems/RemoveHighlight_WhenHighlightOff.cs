using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterTurn;
using Taiga.Core.Map;
using UnityEngine;

namespace Taiga.Core.Unity.MapCellHighlight
{
    internal class RemoveHighlight_WhenHighlightOff : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public RemoveHighlight_WhenHighlightOff(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.hasMapCell_Highlight)
                {
                    entity.RemoveMapCell_Highlight();
                }

                if (entity.hasMapCell_OverrideHighlight)
                {
                    entity.RemoveMapCell_OverrideHighlight();
                }
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.MapCell_HighlightOff);
        }
    }
}
