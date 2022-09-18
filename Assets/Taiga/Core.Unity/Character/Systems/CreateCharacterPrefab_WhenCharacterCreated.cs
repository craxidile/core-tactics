using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Taiga.Core.Character;
using Taiga.Core.Unity.CharacterAnimation;
using UnityEngine;

namespace Taiga.Core.Unity.Character
{
    internal class CreateCharacterPrefab_WhenCharacterCreated : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public CreateCharacterPrefab_WhenCharacterCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var character = entity.AsCharacter(game);
                var architypeId = character.ArchitypeId;

                var prefab = game
                    .GetProvider<ICharacterPrefabPreset>()
                    .CharacterPrefab;

                Debug.Log($">>check_null<< {architypeId}");
                var runtimeAnimatorController = game.GetProvider<ICharacterAnimatorPreset>()
                    .GetAnimatorController(architypeId);

                var animationConfig = game.GetProvider<ICharacterAnimatorPreset>()
                    .GetAnimationConfig(architypeId);

                var characterGameObject = GameObject
                    .Instantiate(prefab);
                characterGameObject.name = $"Character ({architypeId})";

                var characterAnimator = characterGameObject
                    .GetComponent<CharacterAnimator>();

                var animator = characterAnimator.animator;
                characterAnimator.animationConfig = animationConfig;

                animator.runtimeAnimatorController = runtimeAnimatorController;

                entity.AsEntity_GameObject(game)
                    .Link(characterGameObject);
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterEvents.OnCharacterCreated);
        }
    }

}
