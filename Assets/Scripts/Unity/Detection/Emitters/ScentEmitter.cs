using Core.Detection.Olfactory;
using Core.Detection.Olfactory.DataStructures;
using Core.Detection.Services;
using Core.Game.HealthSystem.Health;
using Game.Core.Execution;
using Game.Core.Health;
using Infrastructure.Unity.Registries;
using Primitives.Detection;
using Primitives.Health;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Detection.Emitters
{
    public class ScentEmitter : SelfRegister<IInitializable<IGameContext>>, IScentEmitter, IInitializable<IGameContext>
    {
        [SerializeField] private Transform _directionTransform;
        // [SerializeField] private EntityType _entity;
        [SerializeField] private SerializedInterface<IHealthComponentProvider> _healthComponentMono;
        private IHealthComponent _healthComponent => _healthComponentMono.Interface.HealthComponent;
        private IScentDepositService _scentDeposit;


        // Olfactory data
        private Vector2 _origin;
        private HealthState _healthState => _healthComponent.StateOfHealth;
        private float _lifeTime;
        private float _intensity;

        [SerializeField] private int _priority = 0;
        public int Priority => _priority;

        public void Initialize(IGameContext context)
        {
            _scentDeposit = context.DetectionServices.ScentDepositService;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_scentDeposit != null, $"Failed to set scent deposit service");
        }

        public void EmitScent()
        {
            // build the olfactory data
            OlfactoryData data = new OlfactoryData(
                _origin,
                CalcLifeTime(),
                CalcIntensity(),
                _directionTransform.localScale.x
                                                    );
            // deposit it
            _scentDeposit.Deposit(data);
        }
        private float CalcLifeTime()
        {
            // Intensity should scale with how injured the entity is
            switch (_healthState)
            {
                case HealthState.Fine:
                    {
                        return 0;
                    }
                case HealthState.Wounded:
                    {
                        return 5f;
                    }
                case HealthState.Injured:
                    {
                        return 10f;
                    }
                case HealthState.CriticallyInjured:
                    {
                        return 15f;
                    }
                case HealthState.Dead:
                    {
                        return 20;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
        private float CalcIntensity()
        {
            // Intensity should scale with how injured the entity is
            switch (_healthState)
            {
                case HealthState.Fine:
                    {
                        return 0;
                    }
                case HealthState.Wounded:
                    {
                        return .25f;
                    }
                case HealthState.Injured:
                    {
                        return .5f;
                    }
                case HealthState.CriticallyInjured:
                    {
                        return 0.75f;
                    }
                case HealthState.Dead:
                    {
                        return 1;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
    }
}