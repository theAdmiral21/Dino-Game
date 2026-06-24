using System.Collections.Generic;
using Game.Core.UI.Menus.Transitions;

namespace Infrastructure.Unity.Registries
{
    public interface IScreenTransitionRegistry
    {
        public IReadOnlyCollection<IScreenTransition> Transitions { get; }
    }
}