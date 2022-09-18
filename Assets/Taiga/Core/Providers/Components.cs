using System;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Taiga.Core
{
    [Unique]
    public sealed class Providers : IComponent
    {
        public Dictionary<Type, IProvider> providers;
    }

    public interface IProvider
    {
    }
}
