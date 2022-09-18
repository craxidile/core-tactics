using System.Collections.Generic;
using Entitas;
using Taiga.Core.CharacterTurn;

namespace Taiga.Core.Unity.Camera
{

    public class CameraSystems : Feature
    {
        public CameraSystems(Contexts contexts)
        {
            Add(new CreateCamera_WhenStarted(contexts));
            Add(new FocusOnCharacter_WhenCharacterTurnCreated(contexts));
            Add(new ZoomInCamera_WhenAttackStrategySelectionChanged(contexts));
            Add(new ZoomOutCamera_WhenCharacterActionFinish(contexts));
            Add(new RotateCamera_WhenArrowInput(contexts));
        }
    }
}
