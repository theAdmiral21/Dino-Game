using Game.Application.UI.Menus.Abstractions;
using Game.Core.Scenes;

namespace Game.Application.Scenes.Abstractions
{
    public interface IMenuSceneContext : ISceneDefinition
    {
        public IPageToken RootPage { get; }
    }
}