using Game.Core.Audio;
using Game.Core.Cameras;
using Game.Core.Execution;
using Game.Core.Scenes;
using Game.Core.State.Services;
using Infrastructure.Core.Services;
using Physics.Core.Services;
using Primitives.EventBus.Abstractions;

namespace Game.Application.DataStructures
{
    public class GameContext : IGameContext
    {
        public IInfrastructureServices QuitServices { get; }
        public IGameStateServices GameStateServices { get; }
        public ISceneServices SceneServices { get; }
        public IAudioService AudioService { get; }
        public IEventBus EventBus { get; }
        public ICameraService CameraService { get; }

        public IPhysicsServices PhysicsServices { get; }

        public IPlayerServices PlayerServices { get; }


        public ISceneContextService SceneContextService { get; }


        public GameContext(
            IGameStateServices gameStateServices,
            IInfrastructureServices quitServices,
            ISceneServices sceneServices,
            IAudioService audioService,
            IEventBus eventBus,
            ICameraService cameraServices,
            IPhysicsServices physicsServices,
            IPlayerServices playerServices,
            ISceneContextService sceneContextService
        )
        {
            GameStateServices = gameStateServices;
            QuitServices = quitServices;
            SceneServices = sceneServices;
            AudioService = audioService;
            EventBus = eventBus;
            CameraService = cameraServices;
            PhysicsServices = physicsServices;
            PlayerServices = playerServices;
            SceneContextService = sceneContextService;
        }
    }
}