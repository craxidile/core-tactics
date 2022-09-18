using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character.HateCount;
using Taiga.Core.Character.SpecialAttack.Entities;
using Taiga.Core.CharacterSequence;
using UnityEngine;

namespace Taiga.Core.Character.SpecialAttack.Systems
{
    public class AddDamagedPoint_WhenActionExecuted : ReactiveSystem<GameEntity>
    {
        private GameContext game;

        public AddDamagedPoint_WhenActionExecuted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterContext = game.AsCharacterContext();

            foreach (var entity in entities)
            {
                var sequence = entity
                    .AsCharacterSequence(game);
                
                Debug.Log($">>hate_count_point<< expected_character (decrease) {sequence.CharacterId}");
                
                var character = characterContext
                    .GetCharacter(sequence.CharacterId)
                    .Value;

                var hateCount = character.AsCharacter_HateCount();
                Debug.Log($">>hate_count_point<< decrease");
                hateCount.DecreasePoint();
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity
                .AsCharacterSequence(game)
                .IsDamaged();
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterSequenceEvents.OnSequence);
        }
    }
}