using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{

    [Unique]
    public sealed class Game_CharacterSequenceCutScene : IComponent
    {
    }

    public sealed class CharacterSequence_Animating : IComponent
    {
    }

    public sealed class CharacterSequence_PostAnimating : IComponent
    {
    }

}
