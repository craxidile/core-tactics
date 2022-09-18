using UnityEngine;
using Taiga.Core.CharacterAction;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using Sirenix.Serialization;

namespace Taiga.Core.Unity.CharacterActions
{

    public delegate void CharacterActionDelegate(CharacterActionType action);

    public class CharacterActionsPresenter : SerializedMonoBehaviour
    {

        public GameObject root;

        [OdinSerialize] public Dictionary<CharacterActionType, Button> buttonByAction;

        private void Awake()
        {
            foreach (var pair in buttonByAction)
            {
                var action = pair.Key;
                var button = pair.Value;
                button.onClick.AddListener(() => callback?.Invoke(action));
            }
            root.SetActive(false);
        }

        CharacterActionDelegate callback;
        public void AskForActions(CharacterActionType[] actions, CharacterActionDelegate callback)
        {
            root.SetActive(true);
            this.callback = callback;

            foreach (var button in buttonByAction.Values)
            {
                button.gameObject.SetActive(false);
            }

            foreach (var action in actions)
            {
                buttonByAction[action].gameObject.SetActive(true);
            }
        }

        public void ClearAsking()
        {
            root.SetActive(false);
            callback = null;

            foreach (var button in buttonByAction.Values)
            {
                button.gameObject.SetActive(false);
            }
        }

    }
}
