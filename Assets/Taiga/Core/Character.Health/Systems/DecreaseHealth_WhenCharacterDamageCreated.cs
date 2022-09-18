using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using UnityEngine;

namespace Taiga.Core.CharacterHealth
{
    internal class DecreaseHealth_WhenCharacterDamageCreated : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public DecreaseHealth_WhenCharacterDamageCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterContext = game.AsCharacterContext();
            foreach (var entity in entities)
            {
                var damage = entity.characterDamage;
                var characterEntity = characterContext
                    .GetCharacter(damage.characterId)
                    .Value
                    .entity;

                var currentHealthPoint = characterEntity
                    .character_HealthPoint
                    .value;

                var newHealthPoint = Mathf.Max(0, currentHealthPoint - damage.damage);
                characterEntity.ReplaceCharacter_HealthPoint(newHealthPoint);
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterDamage);
        }

    }
}
