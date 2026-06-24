using System.Collections.Generic;
using Movement.Core.Abstractions;
using Movement.Core.Movement.Abstractions;
using Physics.Core.DataStructures;
using Primitives.Physics;

namespace Physics.Core.PhysicsActors
{
    public interface IActorBrain : IActionRequestHandler,
                                    IActionResultViewer,
                                    IActorEventBusProvider
    {
        public ActorFrameData FrameData { get; set; }
        public KinematicResult KinematicState { get; }
        public RaycastConfiguration RaycastConfig { get; }
        public PhysicsContext CurrentContext { get; set; }
        public void Tick(float dt);
        public List<IActionResult> Think(ActorActionContext context);
        public void ResolveRequests();
        public void RegisterCapability(object capability);
        public T GetCapability<T>() where T : class;
    }
}