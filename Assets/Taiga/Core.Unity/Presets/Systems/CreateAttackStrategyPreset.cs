using Entitas;
using Taiga.Core.Character.Attack;

namespace Taiga.Core.Unity.Preset
{
    internal class CreateAttackStrategyPreset : IInitializeSystem
    {

        GameContext game;

        public CreateAttackStrategyPreset(Contexts contexts)
        {
            this.game = contexts.game;
        }

        public void Initialize()
        {
            game.AddProvider<IAttackStrategyPreset>(new AttackStrategyPreset());
        }
    }
}
