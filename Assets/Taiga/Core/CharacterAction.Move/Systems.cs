using Entitas;

namespace Taiga.Core.CharacterAction.Move
{
    public class CharacterActionMoveSystems : Feature
    {
        public CharacterActionMoveSystems(Contexts contexts)
        {
            Add(new CalculatePossibilities_WhenActionCreated(contexts));
            Add(new PredictPathway_WhenFocused(contexts));
            Add(new CreateActionSequence_WhenActionExecuted(contexts));
        }
    }
}
