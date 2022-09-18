using System.Linq;
using Entitas;
using UnityEngine;

namespace Taiga.Core.Unity.Preset
{

    public class ScriptablePresetSystems<T> : IInitializeSystem where T : ScriptableObject
    {
        Contexts contexts;

        public ScriptablePresetSystems(Contexts contexts)
        {
            this.contexts = contexts;
        }

        public void Initialize()
        {
            Debug.Log($">>provider<< {typeof(T)}");
            var preset = GameObject
                .FindObjectOfType<ScriptableProvider<T>>()
                .provider;

            var providerTypes = typeof(T)
                .GetInterfaces()
                .Where(i => i.GetType() != typeof(IProvider))
                .Where(i => i.GetInterfaces().Any(t => t == typeof(IProvider)));

            foreach (var providerType in providerTypes)
            {
                contexts.AddProvider(providerType, preset as IProvider);
            }
        }
    }
}
