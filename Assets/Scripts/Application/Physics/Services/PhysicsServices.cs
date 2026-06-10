using Physics.Application.Abstractions;
using Physics.Core.Services;

namespace Physics.Application.Services
{
    public class PhysicsServices : IPhysicsServices
    {
        public IForceMoveActor ForceMoveActor { get; private set; }

        public PhysicsServices(IMoveActor moveActor)
        {
            ForceMoveActor = new ForceMoveActor(moveActor);
        }
    }
}