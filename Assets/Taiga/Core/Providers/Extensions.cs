using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Taiga.Core
{
    public static class ContextExtensions
    {
        public static void AddProvider<T>(this Contexts contexts, T provider) where T : IProvider
        {
            AddProvider(contexts.game, provider);
        }

        public static void AddProvider(this Contexts contexts, Type providerType, IProvider provider)
        {
            AddProvider(contexts.game, providerType, provider);
        }

        public static T GetProvider<T>(this Contexts contexts) where T : IProvider
        {
            return GetProvider<T>(contexts.game);
        }

        public static void AddProvider<T>(this GameContext game, T provider) where T : IProvider
        {
            game.AddProvider(typeof(T), provider);
        }

        public static void AddProvider(this GameContext game, Type providerType, IProvider provider)
        {
            if (!game.hasProviders)
            {
                game.SetProviders(new Dictionary<Type, IProvider>());
            }

            var providers = game.providers.providers;
            Assert.IsFalse(
                providers.ContainsKey(providerType),
                $"Provider of type '{providerType}' already added."
            );
            providers.Add(providerType, provider);
            game.ReplaceProviders(providers);
        }

        public static T GetProvider<T>(this GameContext game) where T : IProvider
        {
            game.providers.providers.TryGetValue(typeof(T), out var provider);
            Assert.IsNotNull(
                provider,
                "Provider of type '" + typeof(T) + "' is not exist."
            );
            return (T)provider;
        }
    }
}
