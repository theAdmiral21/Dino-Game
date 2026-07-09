using Core.Physics.PhysicsQueries;
using Physics.Application.Abstractions;
using Physics.Core.Services;

namespace Physics.Application.Services
{
    public class PhysicsServices : IPhysicsServices
    {
        public IForceMoveActor ForceMoveActor { get; private set; }
        public IFitCheck FitCheckService { get; private set; }

        public PhysicsServices(IMoveActor moveActor, IFitCheck raycastController)
        {
            ForceMoveActor = new ForceMoveActor(moveActor);
            FitCheckService = raycastController;
        }
    }
}