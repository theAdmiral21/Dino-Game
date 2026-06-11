namespace Game.Systems.Scnes.Application.Abstractions
{
    public interface ILoadingScreenPresenter
    {
        /// <summary>
        /// Method called before a scene starts loading. It is meant to be used to show a loading screen or fade in.
        /// </summary>
        public void ShowLoading();

        /// <summary>
        /// Method called when a scene is ready for the player. It is used to hide whatever you did in ShowLoading.
        /// </summary>
        public void HideLoading();
    }
}