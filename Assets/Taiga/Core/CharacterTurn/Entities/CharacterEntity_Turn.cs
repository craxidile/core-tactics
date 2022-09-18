namespace Taiga.Core.CharacterTurn
{

    public static class CharacterEntity_TurnExtensions
    {
        public static CharacterEntity_Turn AsCharacter_Turn(this IGameScopedEntity entity)
        {
            return new CharacterEntity_Turn(entity.context, entity.entity);
        }

        public static CharacterEntity_Turn AsCharacter_Turn(this GameEntity entity, GameContext context)
        {
            return new CharacterEntity_Turn(context, entity);
        }
    }

    public struct CharacterEntity_Turn : IGameScopedEntity
    {
        public CharacterEntity_Turn(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public int TurnAffinity => entity.character_TurnAffinity.value;

        public void SetProperty(int affinityRegainRate)
        {
            var preset = context.GetProvider<ICharacterTurnPreset>();

            entity.AddCharacter_TurnProperty(
                newAffinityRegainRate: affinityRegainRate
            );

            entity.AddCharacter_TurnAffinity(
                newValue: preset.MaxTurnAffinity
            );
        }

    }
}
