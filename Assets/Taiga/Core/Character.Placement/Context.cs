using System.Linq;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.Health;
using Taiga.Core.Character.Move;
using Taiga.Core.Character.Placement;
using UnityEngine;

namespace Taiga.Core.Character.Placement
{
    public static class CharacterPlacementContextExtensions
    {
        public static CharacterPlacementContext AsCharacterPlacementContext(this GameContext game)
        {
            return new CharacterPlacementContext(game);
        }
    }

    public struct CharacterPlacementContext
    {
        GameContext game;

        public CharacterPlacementContext(GameContext game)
        {
            this.game = game;
        }

        public CharacterEntity? GetCharacter(Vector2Int position)
        {
            var entities = game.GetEntitiesWithCharacter_Position(position);
            if (entities.Count == 0)
            {
                return null;
            }
            return entities.First().AsCharacter(game);
        }

        public bool Raycast(
            Vector2Int origin,
            MapDirection direction,
            int length,
            out CharacterEntity character
        )
        {
            var unitVector = direction.GetUnitVector();
            for (var i = 1; i <= length; i++)
            {
                var position = origin + (unitVector * i);
                var _character = GetCharacter(position);
                if (_character != null)
                {
                    character = _character.Value;
                    return true;
                }
            }
            character = default;
            return false;
        }

    }
}
