using System;

namespace Game.Core.UI.Menus.Abstractions
{
    /// <summary>
    /// Generic interface for playing an effect and waiting for that effect to complete before completing a page navigation.
    /// </summary>
    public interface IUIPageTransisiton
    {
        public void PlayEnterTransition(Action onEnterComplete);

        public void PlayExitTransition(Action onExitComplete);
    }
}