using Entitas;

namespace Taiga.Core.CharacterAction
{

    public class CharacterActionSystems : Feature
    {
        public CharacterActionSystems(Contexts contexts)
        {
            Add(new FinishAction_WhenActionSkipCreated(contexts));
            Add(new FinishAction_WhenAllSequencesFinished(contexts));
            Add(new RemoveAction_WhenActionFinished(contexts));
        }
    }

}
