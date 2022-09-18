using Taiga.Core.Character.HateCount.Systems;

namespace Taiga.Core.Character.HateCount
{
    public class CharacterHateCountSystems : Feature
    {
        public CharacterHateCountSystems(Contexts contexts)
        {
            Add(new IncreaseHateCount_WhenAttackActionExecuted(contexts));
            Add(new DecreaseHateCount_WhenDamagedSequenceStarted(contexts));
        }
    }
}