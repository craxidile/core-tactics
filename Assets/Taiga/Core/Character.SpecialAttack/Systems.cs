using Entitas;
using Taiga.Core.Character.SpecialAttack.Systems;

namespace Taiga.Core.Character.SpecialAttack
{
    public class CharacterSpecialAttackSystems : Feature
    {
        public CharacterSpecialAttackSystems(Contexts contexts)
        {
            Add(new AddMovePoint_WhenActionExecuted(contexts));
            Add(new AddWaitPoint_WhenActionExecuted(contexts));
            Add(new AddAttackPoint_WhenActionExecuted(contexts));
            Add(new AddDamagedPoint_WhenActionExecuted(contexts));
            Add(new SubtractSpecialAttackPoint_WhenActionExecuted(contexts));
        }
    }
}