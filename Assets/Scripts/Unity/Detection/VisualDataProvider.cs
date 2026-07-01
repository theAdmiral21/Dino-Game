using Core.Detection.Visual;
using Core.Game.HealthSystem.Health;
using Physics.Core.PhysicsActors;
using Primitives.Detection;
using Primitives.Health;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Detection
{
    public class VisualDataProvider : MonoBehaviour, IVisualDataProvider
    {
        public HealthState Health => _healthComponent.HealthComponent.StateOfHealth;
        public Vector2 Velocity => _actor.Brain.FrameData.CurrentState.Velocity;

        [SerializeField] private SerializedInterface<IHealthComponentProvider> _healthComponentMono;
        private IHealthComponentProvider _healthComponent => _healthComponentMono.Interface;

        [SerializeField] private SerializedInterface<IPhysicsActor> actorMono;
        private IPhysicsActor _actor => actorMono.Interface;

        public Vector2 Facing => new Vector2(_facingTransform.localScale.x, 0);

        public PlayerStatus Status => PlayerStatus.Unknown;

        [SerializeField] private Transform _facingTransform;
    }
}