using System.Collections.Generic;
using Entitas;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterSequence;
using UnityEngine;

namespace Taiga.Core.Character.HateCount.Systems
{
    public class DecreaseHateCount_WhenDamagedSequenceStarted : ReactiveSystem<GameEntity>
    {
        private GameContext game;

        public DecreaseHateCount_WhenDamagedSequenceStarted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var action = entity
                    .AsCharacterAction(game);

                var characterId = action.CharacterId;
                Debug.Log($">>hate_count_point<< (decrease) expected_character {characterId}");

                var character = game
                    .AsCharacterContext()
                    .GetCharacter(characterId)
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