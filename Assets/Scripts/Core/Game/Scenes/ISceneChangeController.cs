namespace Game.Core.Scenes
{
    public interface ISceneChangeController
    {
        public bool RequestSceneChange(ISceneChangeRequest request);
    }
}