using System;
using Gameplay.Common.Application.Abstractions;
using Gameplay.Common.Core.Abstractions;
using Infrastructure.Unity;
using Infrastructure.Unity.Registries;
using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;
using Physics.Unity.Actors;
using Physics.Unity.Physics;
using Primitives.Physics;



// using Movement.Application.Abstractions;
using UnityEngine;

namespace Gameplay.Common.Unity
{
    public class TriggerVolume : SelfRegister<ITriggerVolume>, ITriggerVolume, ITriggerEnterEvent, ITriggerStayEvent, ITriggerExitEvent
    {
        [SerializeField] protected bool _isSingleUse;
        [SerializeField] protected Collider2D _collider;
        [SerializeField] protected string _triggerTarget;
        public int Id { get; protected set; }
        public IBoundsProvider BoundsProvider => _boundsProvider;
        protected IBoundsProvider _boundsProvider;
        private bool _fired = false;
        public Action<IPhysicsActor> OnVolumeEntered;
        public Action<IPhysicsActor> OnVolumeStay;
        public Action<IPhysicsActor> OnVolumeExited;

        public new void Awake()
        {
            base.Awake();
            _boundsProvider = new UnityColliderBoundsProvider(_collider);
            RegistryGateway.Register<ITriggerVolume>(this);
        }
        public void OnTriggerEntered(IPhysicsActor entity)
        {
            if (_fired) return;
            // Debug.Log($"Triggered by entity: {entity}");
            var actorTransform = entity.GetComponent<Transform>();

            if (actorTransform != null && actorTransform.gameObject.CompareTag(_triggerTarget))
            {
                Debug.Log($"Player entered");
                OnVolumeEntered?.Invoke(entity);

                if (_isSingleUse)
                {
                    _fired = true;
                }
            }
        }

        public void OnTriggerExited(IPhysicsActor entity)
        {
            if (_fired) return;
            // Debug.Log($"Triggered by entity: {entity}");
            // var actor = entity.GetComponent<GameObject>();

            // if (actor != null && actor.CompareTag(_triggerTarget))
            if (entity != null && entity.Body.BodyType == BodyType.Kinematic)
            {
                // Debug.Log($"Player exited");
                OnVolumeExited?.Invoke(entity);

                if (_isSingleUse)
                {
                    _fired = true;
                }
            }
        }

        private new void OnDestroy()
        {
            base.OnDestroy();
            RegistryGateway.Deregister<ITriggerVolume>(this);
        }

        public void OnTriggerStayed(IPhysicsActor entity)
        {
            if (_fired) return;
            // Debug.Log($"Triggered by entity: {entity}");
            // var actor = entity.GetComponent<GameObject>();

            // if (actor != null && actor.CompareTag(_triggerTarget))
            if (entity != null && entity.Body.BodyType == BodyType.Kinematic)
            {
                // Debug.Log($"Player stayed");
                OnVolumeStay?.Invoke(entity);

                if (_isSingleUse)
                {
                    _fired = true;
                }
            }
        }
    }
}