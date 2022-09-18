using Entitas;

namespace Taiga.Core.Unity.Character
{
    public class CharacterSystems : Feature
    {
        public CharacterSystems(Contexts contexts)
        {
            Add(new CreateCharacterPrefab_WhenCharacterCreated(contexts));
            Add(new UpdateCharacterStatus_WhenCharacterCreated(contexts));
            Add(new UpdateCharacterStatus_WhenChracterTurnFinished(contexts));
            Add(new UpdateCharacterStatus_WhenChracterTurnCreated(contexts));
            Add(new SetInvisibleCharacterStatus_WhenCharacterDied(contexts));
        }
    }
}
