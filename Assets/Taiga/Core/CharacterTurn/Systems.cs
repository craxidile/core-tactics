using Entitas;

namespace Taiga.Core.CharacterTurn
{
    public class CharacterTurnSystems : Feature
    {
        public CharacterTurnSystems(Contexts contexts)
        {
            Add(new CreatePossibleActions_WhenCharacterTurnCreated(contexts));
            Add(new RemovePossibleAction_WhenActionFinished(contexts));
            Add(new ReduceCharacterAffinity_WhenActionFinished(contexts));
            Add(new FinishCharacterTurn_WhenActionSkipFinished(contexts));
            Add(new FinishTurn_WhenNoPossibleActions(contexts));
            Add(new RegainAffinity_WhenTurnFinished(contexts));
            Add(new ReorderCharacter_WhenCharacterAffinityChanged(contexts));
            Add(new CreateCharacterTurn_WhenTurnFinished_OrGameRoundStarted(contexts));
        }
    }
}
