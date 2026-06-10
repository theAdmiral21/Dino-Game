using Game.Core.Scenes;

namespace Game.Core.Execution
{
    public interface ISceneRegistry
    {
        public void Register(IInitializable<IGameContext> initializable);

        public void SetBootStrapper(ISceneBootStrapper bootStrapper);
    }
}