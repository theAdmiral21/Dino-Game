using System.Text;
using Physics.Application.Abstractions;
using Physics.Application.Collisions;
using Physics.Core.Abstractions;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsQueries;
using Physics.Features;
using Physics.Features.Movement;
using Physics.Unity.Movement;
using Physics.Unity.PhysicsQueries;
using Unity.Common.Unity;
using UnityEngine;

namespace Physics.Unity.Physics
{
    public class SimulationDriver : MonoBehaviour, ISimulationDriver
    {
        [SerializeField] bool _debugResults;
        [SerializeField] SerializedInterface<IRaycastController> _raycastMono;
        private IRaycastController _raycastController => _raycastMono.Interface;
        private IMovementResolver _movementResolver;
        private ISolveKinematics _kinematicSolver;
        private IIntegrator _integrator;
        private ICheckPathOverlap _pathOverlap;
        private ICornerResolver _cornerResolver;
        private void Awake()
        {
            _movementResolver = new MovementResolver(_raycastController);
            _kinematicSolver = new KinematicSolver();
            _integrator = new Integrator();
            // _pathOverlap = new CheckPathOverlap();
            // _cornerResolver = new CornerResolver(_pathOverlap);
        }
        public ActorFrameData Step(ActorFrameData frameData, float dt)
        {
            frameData.CurrentState.Dt = dt;

            if (_debugResults)
            {
                var _debugString = new StringBuilder();
                _debugString.AppendLine($"Results for {frameData.DebugName}");
                foreach (var result in frameData.Results)
                {
                    _debugString.AppendLine($"{result}, approved: {result.Approved}");
                }
                Debug.Log(_debugString);


            }
            if (frameData.Results != null)
            {
                frameData = AdvanceSimulation(frameData);
            }
            return frameData;
        }


        private ActorFrameData AdvanceSimulation(ActorFrameData frameData)
        {
            _kinematicSolver.Solve(frameData);

            frameData.CurrentState = _integrator.Integrate(ref frameData.CurrentState);

            if (frameData.PhysicsContext.MotionProvider != null)
            {
                frameData.CurrentState.FrameDelta += frameData.PhysicsContext.MotionProvider.DeltaPosition;
            }

            // Perform corner resolution BEFORE movement resolution

            MovementResolution resolution = _movementResolver.ResolveMovement(frameData.CurrentState.FrameDelta, frameData.RaycastConfig);

            frameData.CurrentState.FrameDelta = resolution.FrameDelta;

            // If you're grounded, don't apply corner correction
            if (frameData.PhysicsContext.IsGrounded || frameData.PhysicsContext.IsOnPlatform)
            {
                frameData.CurrentState.CornerNudge = Vector2.zero;
            }
            else
            {
                // Debug.Log($"Added corner correction: {resolution.CornerNudge} to {frameData.DebugName}");
                frameData.CurrentState.CornerNudge = resolution.CornerNudge;
            }
            return frameData;
        }



    }
}