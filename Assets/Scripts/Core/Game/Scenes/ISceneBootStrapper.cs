using System;
using System.Collections.Generic;
using Game.Core.Execution;
using Primitives.Common.Scenes;

namespace Game.Core.Scenes
{
    public interface ISceneBootStrapper
    {
        public event Action OnReady;
        public IReadOnlyCollection<IInitializable<IGameContext>> Systems { get; }
        public void ConsumePersistent();
        public void InitializeScene();
        public void PostInitializeScene();
        public void TearDown(SceneId _);
    }
}