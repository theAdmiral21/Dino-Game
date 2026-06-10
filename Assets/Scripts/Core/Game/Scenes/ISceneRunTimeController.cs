namespace Game.Core.Scenes
{
    public interface ISceneRunTimeController
    {
        /// <summary>
        /// Called after the scene is first loaded.
        /// </summary>
        public void OnSceneLoaded();

        /// <summary>
        /// Called while the scene is running
        /// </summary>
        public void Tick();

        /// <summary>
        /// Called when leaving the scene
        /// </summary>
        public void OnSceneUnloaded();
    }
}