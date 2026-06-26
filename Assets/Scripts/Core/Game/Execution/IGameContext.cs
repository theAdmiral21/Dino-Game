using Core.Detection.Services;
using Game.Core.Audio;
using Game.Core.Cameras;
// using Game.Core.Quests;
using Game.Core.Scenes;
// using Game.Core.ScoreSystem;
using Game.Core.State.Services;
// using Infrastructure.Core.Diagnostics;
using Infrastructure.Core.Services;
using Physics.Core.Services;
using Primitives.EventBus.Abstractions;
// using StoryTellers.Core.Services;

namespace Game.Core.Execution
{
    public interface IGameContext
    {
        // public IDiagnosticSink Diagnostics { get; }

        public IGameStateServices GameStateServices { get; }

        public IInfrastructureServices QuitServices { get; }

        public ISceneServices SceneServices { get; }

        public IAudioService AudioService { get; }

        public IEventBus EventBus { get; }

        public IPhysicsServices PhysicsServices { get; }

        // public IScoreService ScoreService { get; }

        public ICameraService CameraService { get; }

        public IPlayerServices PlayerServices { get; }

        // public IConversationService ConversationService { get; }

        public ISceneContextService SceneContextService { get; }

        // public IChoreStatusProvider ChoreStatusProvider { get; }
        public IDetectionServices DetectionServices { get; }

    }
}