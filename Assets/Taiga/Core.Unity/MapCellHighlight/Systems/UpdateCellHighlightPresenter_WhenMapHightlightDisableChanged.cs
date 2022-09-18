using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterTurn;
using Taiga.Core.Map;
using UnityEngine;

namespace Taiga.Core.Unity.MapCellHighlight
{
    internal class UpdateCellHighlightPresenter_WhenMapHightlightDisableChanged : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public UpdateCellHighlightPresenter_WhenMapHightlightDisableChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var disable = game.isGame_MapCellHighlightDisable;

            var highlightEntities = game
                .GetGroup(GameMatcher.MapCell_Highlight)
                .GetEntities();

            foreach (var highlightEntity in highlightEntities)
            {
                var gameObject = highlightEntity.AsGameObject();
                var presenter = gameObject.GetComponent<MapCellHighlightPresenter>();
                if (disable)
                {
                    presenter.ClearHighlight();
                }
                else
                {
                    presenter.SetHighlight(
                        color: highlightEntity.mapCell_Highlight.color
                    );
                }
            }

            var overrideHighlightEntities = game
                .GetGroup(GameMatcher.MapCell_OverrideHighlight)
                .GetEntities();

            foreach (var highlightEntity in overrideHighlightEntities)
            {
                var gameObject = highlightEntity.AsGameObject();
                var presenter = gameObject.GetComponent<MapCellHighlightPresenter>();
                if (disable)
                {
                    presenter.ClearHighlight();
                }
                else
                {
                    presenter.SetHighlight(
                        color: highlightEntity.mapCell_OverrideHighlight.color
                    );
                }
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.Game_MapCellHighlightDisable.AddedOrRemoved()
            );
        }
    }
}
