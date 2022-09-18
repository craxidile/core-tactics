using Taiga.Core.Character;

namespace Taiga.Core.CharacterHealth
{

    public class CharacterHealthSystems : Feature
    {
        public CharacterHealthSystems(Contexts contexts)
        {
            Add(new DecreaseHealth_WhenCharacterDamageCreated(contexts));
            Add(new Dead_WhenCharacterHealthChangedToZero(contexts));
        }
    }
}
