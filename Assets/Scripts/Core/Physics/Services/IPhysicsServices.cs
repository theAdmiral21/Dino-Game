using Core.Physics.PhysicsQueries;

namespace Physics.Core.Services
{
    public interface IPhysicsServices
    {
        public IForceMoveActor ForceMoveActor { get; }
        public IFitCheck FitCheckService
        {
            get;

        }
    }
}