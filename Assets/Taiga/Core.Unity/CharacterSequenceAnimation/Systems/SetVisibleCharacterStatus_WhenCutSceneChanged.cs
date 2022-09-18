using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Health;
using Taiga.Core.Unity.Character;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{
    internal class SetVisibleCharacterStatus_WhenCutSceneChanged : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public SetVisibleCharacterStatus_WhenCutSceneChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characters = game
                .AsCharacterHealthContext()
                .AliveCharacters;

            foreach (var character in characters)
            {
                var presenter = character
                    .AsGameObject()
                    .GetComponent<CharacterStatusPresenter>();
                presenter.SetVisible(!game.isGame_CharacterSequenceCutScene);
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.Game_CharacterSequenceCutScene.AddedOrRemoved()
            );
        }
    }
}
