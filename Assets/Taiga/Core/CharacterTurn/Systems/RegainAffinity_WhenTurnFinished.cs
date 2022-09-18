using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character.Health;
using UnityEngine;

namespace Taiga.Core.CharacterTurn
{
    internal class RegainAffinity_WhenTurnFinished : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public RegainAffinity_WhenTurnFinished(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var finishedTurn = entity.AsCharacterTurn(game);
                var characters = game
                    .AsCharacterHealthContext()
                    .AliveCharacters
                    .Where(character => character.Id != finishedTurn.CharacterId);

                var maxAffinity = game
                    .GetProvider<ICharacterTurnPreset>()
                    .MaxTurnAffinity;

                foreach (var character in characters)
                {
                    var characterEntity = character.entity;

                    var currentAffinity = characterEntity
                        .character_TurnAffinity
                        .value;

                    var regainAffinity = characterEntity
                        .character_TurnProperty
                        .affinityRegainRate;

                    var newAffinity = currentAffinity + regainAffinity;
                    newAffinity = Mathf.Min(maxAffinity, newAffinity);
                    characterEntity.ReplaceCharacter_TurnAffinity(newAffinity);
                }
            }

        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterTurn_Finished);
        }

    }

}
