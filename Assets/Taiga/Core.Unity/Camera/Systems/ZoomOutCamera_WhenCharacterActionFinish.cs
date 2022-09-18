using System.Collections.Generic;
using Entitas;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterAction.Attack;
using Taiga.Core.CharacterTurn;
using UnityEngine;

namespace Taiga.Core.Unity.Camera
{
    internal class ZoomOutCamera_WhenCharacterActionFinish : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public ZoomOutCamera_WhenCharacterActionFinish(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                // var characterId = entity
                //     .AsCharacterTurn(game)
                //     .CharacterId;


                // game.cameraEntity
                //     .AsGameObject()
                //     .GetComponent<GameCamera>()
                //     .FocusOnCharacter(characterId);

                game.cameraEntity
                    .AsGameObject()
                    .GetComponent<GameCamera>()
                    .ZoomOutAndUndim();

            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                CharacterActionEvents.OnActionFinished.Added()
            );
        }
    }

}
