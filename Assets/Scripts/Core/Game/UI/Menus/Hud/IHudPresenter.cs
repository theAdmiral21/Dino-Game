namespace Game.Core.UI.Hud
{
    public interface IHudPresenter
    {
        public void UpdateVisuals();
        public void Hide();
        public void Show();
    }
}