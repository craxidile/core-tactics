using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterSequence;
using Taiga.Core.Unity.Character;

namespace Taiga.Core.Unity.CharacterAnimation
{

    public class CharacterAnimationSystems : Feature
    {
        public CharacterAnimationSystems(Contexts contexts)
        {
            Add(new SetPosition_WhenCharacterCreated(contexts));
        }
    }

    public class SetPosition_WhenCharacterCreated : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public SetPosition_WhenCharacterCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var characterPosition = entity.AsCharacter_Placement(game);
                var gameObject = entity.AsGameObject();
                var characterAnimator = gameObject.GetComponent<CharacterAnimator>();
                characterAnimator.SetPlacement(
                    characterPosition.Facing,
                    characterPosition.Position
                );
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.AllOf(
                    CharacterEvents.OnCharacterCreated,
                    GameObjectLinkEvents.OnGameObjectLinked
                )
            );
        }
    }

}
