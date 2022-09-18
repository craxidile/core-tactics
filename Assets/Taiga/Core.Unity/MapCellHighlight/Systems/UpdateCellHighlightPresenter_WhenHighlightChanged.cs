using System.Diagnostics;
using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterTurn;
using Taiga.Core.Map;
using Debug = UnityEngine.Debug;

namespace Taiga.Core.Unity.MapCellHighlight
{
    internal class UpdateCellHighlightPresenter_WhenHighlightChanged : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public UpdateCellHighlightPresenter_WhenHighlightChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var gameObject = entity.AsGameObject();

                var presenter = gameObject.GetComponent<MapCellHighlightPresenter>();

                if (entity.hasMapCell_OverrideHighlight)
                {
                    presenter.SetCursorHighlight(entity.mapCell_OverrideHighlight.color);
                    if (entity.mapCell_OverrideHighlight.focused)
                    {
                        presenter.SetFocus();
                    }
                    else
                    {
                        presenter.ClearFocus();
                    }
                }
                else if (entity.hasMapCell_Highlight)
                {
                    presenter.SetHighlight(
                        color: entity.mapCell_Highlight.color
                    );
                    presenter.ClearFocus();
                }
                else
                {
                    presenter.ClearHighlight();
                    presenter.ClearFocus();
                }
            }
        }

        protected override bool Filter(GameEntity entity) => !game.isGame_MapCellHighlightDisable;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.MapCell_Highlight.AddedOrRemoved(),
                GameMatcher.MapCell_OverrideHighlight.AddedOrRemoved()
            );
        }
    }
}
