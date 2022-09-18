using Taiga.Core.Character;
using Taiga.Core.CharacterAction;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{

    public class CharacterSequenceAnimationSystems : Feature
    {
        public CharacterSequenceAnimationSystems(Contexts contexts)
        {
            // Sequence animation
            Add(new StartAnimation_WhenInitialSequenceCreated(contexts));
            Add(new StartConsequenceAnimation_WhenSequenceAttackAnimated(contexts));
            Add(new AnimateWalk_WhenAnimationStarted(contexts));
            Add(new AnimateAttack_WhenAnimationStarted(contexts));
            Add(new AnimateBlocked_WhenAnimationStarted(contexts));
            Add(new AnimateDamaged_WhenAnimationStarted(contexts));
            Add(new EnterCutScene_WhenNonMoveAnimatingStarted(contexts));
            Add(new ExitCutScene_WhenNonMoveAnimatingFinished(contexts));
            Add(new SetVisibleCharacterStatus_WhenCutSceneChanged(contexts));
            Add(new CommitSequence_WhenAnimationFinished(contexts));

            // Post sequence animation
            Add(new StartPostAnimation_WhenAllSequenceCommited(contexts));
            Add(new AnimateCharacterStatus_WhenPostAnimationStarted(contexts));
            Add(new WaitAndFinishSequence_WhenPostAnimationFinished(contexts));


            // Facing
            Add(new AdjustAttackFacing_WhenAnimationStarted(contexts));
            Add(new AdjustDamagedFacing_WhenAnimationStarted(contexts));
        }
    }
}
