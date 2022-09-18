namespace Taiga.Core.CharacterSequence
{
    public interface ICharacterSequenceEntity_DamageSource
    {

        public CharacterSequenceEntity_Damaged CreateDamaged(
            int characterId,
            int damage,
            MapDirection bumpDirection
        );

        public CharacterSequenceEntity_Blocked CreateBlocked(
            int characterId,
            MapDirection bumpDirection
        );

    }
}
