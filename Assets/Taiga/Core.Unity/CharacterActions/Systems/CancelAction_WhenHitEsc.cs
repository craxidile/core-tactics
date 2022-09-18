using Entitas;
using Taiga.Core.CharacterAction;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterActions
{
    public class CancelAction_WhenHitEsc : IExecuteSystem
    {
        GameContext game;

        public CancelAction_WhenHitEsc(Contexts contexts)
        {
            this.game = contexts.game;
        }

        public void Execute()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var actionContext = game.AsCharacterActionContext();
                var currentAction = actionContext.CurrentAction;
                if (currentAction != null)
                {
                    currentAction.Value.Cancel();
                }
            }
        }
    }
}
