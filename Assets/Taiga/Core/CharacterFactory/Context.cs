using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.HateCount;
using Taiga.Core.Character.Health;
using Taiga.Core.Character.Move;
using Taiga.Core.Character.Placement;
using Taiga.Core.Character.SpecialAttack.Entities;
using Taiga.Core.CharacterTurn;
using UnityEngine;

namespace Taiga.Core.CharacterFactory
{
    public static class CharacterFactoryContextExtensions
    {
        public static CharacterFactoryContext AsCharacterFactoryContext(this GameContext game)
        {
            return new CharacterFactoryContext(game);
        }
    }

    public struct CharacterFactoryContext
    {
        GameContext game;

        public CharacterFactoryContext(GameContext game)
        {
            this.game = game;
        }

        public CharacterEntity Create(
            int ownerPlayerId,
            string architypeId,
            int level,
            Vector2Int position,
            MapDirection direction
        )
        {
            var architype = game
                .GetProvider<ICharacterArchitypePreset>()
                .GetCharacterArchitypeProperty(architypeId);

            var character = game
                .AsCharacterContext()
                .CreateCharacter(
                    ownerPlayerId: ownerPlayerId,
                    architypeId: architypeId,
                    level: level
                );

            character
                .AsCharacter_Placement()
                .SetPlacement(
                    position: position,
                    facing: direction
                );

            character
                .AsCharacter_Attack()
                .SetupProperty(
                    attackType: architype.attackType,
                    attack: architype.attack,
                    defend: architype.defend,
                    accuracy: architype.accuracy,
                    evasion: architype.evasion,
                    critical: architype.critical
                );


            if (architype.specialAttackType != null)
            {
                character
                    .AsCharacter_Attack()
                    .SetupSpecialAttackProperty(
                        attackType: architype.specialAttackType.Value,
                        attack: architype.specialAttack,
                        maxSpecialAttackUnit: architype.maxSpecialAttackUnit,
                        specialAttackUnitUsage: architype.specialAttackUnitUsage,
                        attackControllers: architype.attackControllers,
                        specialAttackUnitBounds: architype.specialAttackUnitBounds
                    );
            }

            character
                .AsCharacter_Turn()
                .SetProperty(
                    affinityRegainRate: architype.turnAffinityRagainRate
                );

            character
                .AsCharacter_Movement()
                .SetProperty(
                    length: architype.moveLength
                );

            character
                .AsCharacter_Health()
                .SetupHealth(
                    point: architype.health
                );

            character
                .AsCharacter_SpecialAttack()
                .SetupSpecialAttack(
                    point: 0
                );

            character
                .AsCharacter_HateCount()
                .SetupHateCount(
                    point: 0
                );

            return character;
        }
    }
}