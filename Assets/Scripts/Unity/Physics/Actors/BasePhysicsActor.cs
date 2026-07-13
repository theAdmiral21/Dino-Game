using Core.Game.Lifecycle;
using Core.Physics.Collisions;
using Game.Core.Execution;
using Gameplay.Common.Unity;
using Infrastructure.Unity;
using Infrastructure.Unity.Registries;
using Movement.Core.Abstractions;
using Movement.Core.Movement.DataStructures;
using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;
using Primitives.Physics;
using Primitives.SaveData;
using UnityEngine;

namespace Physics.Unity.Actors
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class BasePhysicsActor : SelfRegister<IInitializable<IGameContext>>,
                                            IInitializable<IGameContext>,
                                            IPhysicsActor,
                                            IActorEventBusProvider,
                                            IExternalForceReceiver,
                                            IRevertable
    {
        public ActorType Actor => _actor;
        [SerializeField] private ActorType _actor;

        public IActorBrain Brain { get; protected set; }

        public IKinematicBody Body { get; protected set; }

        public string Name => _name;
        [SerializeField] private string _name;

        public Vector2 MoveVector { get; set; }

        public bool ReadyToDestroy { get; private set; }

        public bool IsAsleep { get; private set; }

        [SerializeField] private int _priority = 0;
        public int Priority => _priority;

        public IActorEventBus ActorEventBus => Brain.ActorEventBus;

        public ICollisionHandler CollisionHandler { get; private set; }

        protected LayerMask _collisionLayer;
        protected IBoundsProvider _bounds;
        protected ITransformProvider _transformProvider;

        private KinematicSaveData? _saveData;
        protected virtual void Awake()
        {
            base.Awake();
            RegistryGateway.Register<IPhysicsActor>(this);
            RegistryGateway.Register<IRevertable>(this);

            Collider2D collider = GetComponent<Collider2D>();
            _bounds = new UnityColliderBoundsProvider(collider);
            _transformProvider = new UnityTransformProvider(transform);
            // Debug.Log($"[BASE] player bounds: {_bounds}");
            // The collision layer should always be collision
            // _collisionLayer = LayerMask.GetMask("Collision");
            _collisionLayer = Physics2D.GetLayerCollisionMask(gameObject.layer);

            // Attempt to get the collision handler, if it's null nothing should happen
            CollisionHandler = GetComponent<ICollisionHandler>();


            // Debug.Log($"BasePhysicsActor.Awake() - bounds: {_bounds} collider: {GetComponent<Collider2D>()} - Frame: {Time.frameCount}");
        }
        private new void OnDestroy()
        {
            base.OnDestroy();
            // Remove yourself from the physics registry
            RegistryGateway.Deregister<IPhysicsActor>(this);
        }
        public abstract void Initialize(IGameContext context);

        public abstract void PostInitialize(IGameContext context);

        public abstract void EnqueueActionRequest(IActionRequest newRequest);

        public void MarkForDestruction()
        {
            if (!ReadyToDestroy)
            {
                ReadyToDestroy = true;
            }
        }

        public void Destruct()
        {
            if (ReadyToDestroy)
            {
                Destroy(gameObject);
            }
        }

        public void Sleep()
        {
            // Sleep the player -> Stops updates
            IsAsleep = true;
            // Clear any forces acting on the actor -> zeros any movement
            Brain.KinematicState.ClearForces();
            // Clear any action results -> Clears any approved future movement
            if (Brain.ActionResults != null)
            {
                Brain.ActionResults.Clear();
            }
        }

        public void WakeUp()
        {
            // wake up the player -> Restart updates
            IsAsleep = false;
            // Clear any forces acting on the actor -> zeros any movement while disabled
            Brain.KinematicState.ClearForces();
            // Clear any action results -> Clears any approved movement accrued while asleep
            if (Brain.ActionResults != null)
            {
                Brain.ActionResults.Clear();
            }
        }

        public void ReceiveImpulse(IActionRequest result)
        {
            EnqueueActionRequest(result);
        }

        public void ReceiveContinuous(IActionRequest result)
        {
            EnqueueActionRequest(result);
        }

        public void Revert()
        {
            if (_saveData.HasValue)
            {
                Debug.LogError($"Implement reverting for the physics actor.");
            }
        }

        public void TakeSnapShot()
        {
            _saveData = new KinematicSaveData
            {
                Position = Brain.FrameData.PhysicsContext.GlobalPosition,
                // Velocity = Brain.FrameData.CurrentState.Velocity,
                // ExternalVelocity = Brain.FrameData.CurrentState.Velocity,
                // Gravity = Brain.FrameData.CurrentState.Gravity,
            };
        }

        public void SerializeSnapShot()
        {
            throw new System.NotImplementedException();
        }
    }
}