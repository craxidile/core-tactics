using System.Collections.Generic;

namespace Taiga.Core.CharacterAction
{

    public static class CharacterActionEntityExtensions
    {
        public static CharacterActionEntity AsCharacterAction(this IGameScopedEntity entity)
        {
            return new CharacterActionEntity(entity.context, entity.entity);
        }

        public static CharacterActionEntity AsCharacterAction(this GameEntity entity, GameContext context)
        {
            return new CharacterActionEntity(context, entity);
        }
    }

    public struct CharacterActionEntity : IGameScopedEntity
    {
        public CharacterActionEntity(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public int CharacterId => entity.characterAction.characterId;

        public CharacterActionType ActionType => entity.characterAction.type;

        public bool IsExecuted => entity.isCharacterAction_Execute;

        public void Cancel()
        {
            entity.isCharacterAction_Cancel = true;
        }

        public void Execute()
        {
            if (entity.isCharacterAction_Executable)
            {
                entity.isCharacterAction_Execute = true;
            }
        }

        public bool CanExecute
        {
            get => entity.isCharacterAction_Executable;
            internal set
            {
                entity.isCharacterAction_Executable = value;
            }
        }

    }
}
