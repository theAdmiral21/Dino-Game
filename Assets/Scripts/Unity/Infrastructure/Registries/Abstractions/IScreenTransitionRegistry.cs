using System.Collections.Generic;
using Game.Core.UI.Menus.Transitions;
using Infrastructure.Core.Registries;

namespace Infrastructure.Unity.Registries
{
    public interface IScreenTransitionRegistry
    {
        public IReadOnlyCollection<IScreenTransition> Transitions { get; }
    }
}