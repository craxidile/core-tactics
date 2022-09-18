using UnityEngine;

namespace Taiga.Core.Unity.Preset
{
    public abstract class ScriptableProvider<T> : MonoBehaviour where T : ScriptableObject
    {
        public T provider;
    }

}
