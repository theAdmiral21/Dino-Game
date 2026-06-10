
namespace Game.Core.UI.Menus.Abstractions
{
    public interface IUITween
    {
        public bool Blocking { get; }
        public void PlayForward();
        public void PlayBackwards();
    }
}