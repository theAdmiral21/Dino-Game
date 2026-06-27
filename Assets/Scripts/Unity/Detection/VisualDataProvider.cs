using Core.Detection.Visual;
using Core.Game.HealthSystem.Health;
using Physics.Core.PhysicsActors;
using Primitives.Health;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Detection
{
    public class VisualDataProvider : MonoBehaviour, IVisualDataProvider
    {
        public HealthState Health => _healthComponent.HealthComponent.StateOfHealth;
        public float Facing => _facingTransform.localScale.x;
        public Vector2 Velocity => _actor.Brain.FrameData.CurrentState.Velocity;

        [SerializeField] private SerializedInterface<IHealthComponentProvider> _healthComponentMono;
        private IHealthComponentProvider _healthComponent => _healthComponentMono.Interface;

        [SerializeField] private SerializedInterface<IPhysicsActor> actorMono;
        private IPhysicsActor _actor => actorMono.Interface;

        [SerializeField] private Transform _facingTransform;
    }
}