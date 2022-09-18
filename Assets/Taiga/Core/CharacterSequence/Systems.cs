using Entitas;

namespace Taiga.Core.CharacterSequence
{
    public class CharacterSequenceSystems : Feature
    {
        public CharacterSequenceSystems(Contexts contexts)
        {
            Add(new UpdateCharacter_WhenSequenceDamagedCommitted(contexts));
            Add(new UpdateCharacter_WhenSequenceMoveCommitted(contexts));
            Add(new DestroySequence_WhenSequenceFinished(contexts));
            Add(new MarkCommited_WhenSequenceCommit(contexts));
        }
    }
}
