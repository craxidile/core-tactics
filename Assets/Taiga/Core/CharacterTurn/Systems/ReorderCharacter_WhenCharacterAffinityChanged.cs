using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Health;
using UnityEngine;

namespace Taiga.Core.CharacterTurn
{

    internal class ReorderCharacter_WhenCharacterAffinityChanged : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public ReorderCharacter_WhenCharacterAffinityChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            // TODO: Not rotate characters
            // var characterHealthContext = game.AsCharacterHealthContext();
            // var characterIds = characterHealthContext
            //     .AliveCharacters
            //     .Select(character => character.Id);
            //
            // game.ReplaceGame_CharacterTurnOrder(characterIds);
            
            // TODO: Copy this to use
            // Description: Arrange Character IDs by Hate Count
            // var characterIds = characterHealthContext
            //                 .AliveCharacters
            //                 .OrderByDescending(character => character.entity.character_HateCountPoint)
            //                 .Select(character => character.Id);

            // TODO: Rotate characters
            var characterHealthContext = game.AsCharacterHealthContext();
            var characterIds = characterHealthContext
                .AliveCharacters
                .OrderByDescending(character => character.entity.character_TurnAffinity.value)
                .ThenByDescending(character => character.entity.character_TurnProperty.affinityRegainRate)
                .Select(character => character.Id);

            // TODO: Remove this later
            characterIds = characterIds.Where(ci => ci <= 1);

            game.ReplaceGame_CharacterTurnOrder(characterIds);
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Character_TurnAffinity);
        }

    }

}
