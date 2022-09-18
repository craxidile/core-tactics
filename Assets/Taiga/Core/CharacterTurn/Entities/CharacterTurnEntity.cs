using System.Collections.Generic;
using Taiga.Core.CharacterAction;
using UnityEngine;

namespace Taiga.Core.CharacterTurn
{

    public static class CharacterTurnEntityExtensions
    {
        public static CharacterTurnEntity AsCharacterTurn(this IGameScopedEntity entity)
        {
            return new CharacterTurnEntity(entity.context, entity.entity);
        }

        public static CharacterTurnEntity AsCharacterTurn(this GameEntity entity, GameContext context)
        {
            return new CharacterTurnEntity(context, entity);
        }
    }

    public struct CharacterTurnEntity : IGameScopedEntity
    {
        public CharacterTurnEntity(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public int CharacterId => entity.characterTurn.characterId;

        public CharacterActionType[] PossibleActions => entity.characterTurn_PossibleActions.actions;

    }
}
