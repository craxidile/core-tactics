using Entitas;

namespace Taiga.Core.CharacterSequence
{
    public static class CharacterSequenceEvents
    {
        public static IMatcher<GameEntity> OnSequence => GameMatcher.CharacterSequence;

        public static IMatcher<GameEntity> OnSequenceCommit => GameMatcher.CharacterSequence_Commit;

        public static IMatcher<GameEntity> OnSequenceCommited => GameMatcher.CharacterSequence_Commited;

        public static IMatcher<GameEntity> OnSequenceFinish => GameMatcher.CharacterSequence_Finish;
    }
}
