using Taiga.Core.Character;
using Taiga.Core.CharacterAction;

namespace Taiga.Core.Unity.CharacterCooldownRank
{
    public class CharacterCooldownRankSystems : Feature
    {
        public CharacterCooldownRankSystems(Contexts contexts)
        {
            Add(new UpdateCooldownRank_WhenCharacterOrderChanged(contexts));
        }
    }
}
