using Core.Movement.Abstractions;
using Core.Physics.Collisions;
using Core.Physics.PhysicsQueries;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Movement.Core.Abstractions;
using Movement.Core.Rules;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsActors;
using Unity.Common;
using UnityEngine;

namespace Unity.Physics.Collisions
{
    public class CrouchController : SelfRegister<IInitializable<IGameContext>>, IColliderModifier, IInitializable<IGameContext>, ICheckCanStand
    {
        [SerializeField] private BoxCollider2D _collider;

        private IRuleState _ruleState;
        private ICrouchState _crouchState;
        private IFitCheck _fitChecker;
        private IPhysicsActor _actor;

        [SerializeField] private float _standHeight;
        [SerializeField] private float _crouchHeight;

        [SerializeField] private int _priority;
        public int Priority => _priority;

        public void Initialize(IGameContext context)
        {
            var ruleProvider = ProviderLookUp.Require<IRuleStateProvider>(this);
            _ruleState = ruleProvider.RuleStateView;

            var actorProvider = ProviderLookUp.Require<IActorProvider>(this);
            _actor = actorProvider.Actor;

            _fitChecker = context.PhysicsServices.FitCheckService;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_ruleState != null, $"Failed to find IRuleStateProvider");
            _ruleState.TryGet<ICrouchState>(out _crouchState);
        }

        public void ModifyHeightFromBottom(float newHeight)
        {
            Vector2 currentSize = _collider.size;
            Vector2 currentOffset = _collider.offset;
            float delta = newHeight - currentSize.y;
            // Whatever delta you use to change the size, half it, and add it to the offset to compensate

            currentSize.y = newHeight;
            currentOffset.y += delta / 2;

            // if delta is positive, we're standing up, perform a fit check
            bool willFit = true;
            if (delta > 0)
            {
                willFit = CheckCanStand(_actor.Body.RayConfig);
            }

            if (willFit)
            {
                _collider.size = currentSize;
                _collider.offset = currentOffset;
            }
            else
            {
                Debug.LogError($"Player will not fit here.");
            }
        }

        private void FixedUpdate()
        {
            if (_crouchState.IsCrouching && _collider.size.y != _crouchHeight)
            {
                ModifyHeightFromBottom(_crouchHeight);
            }
            else if (!_crouchState.IsCrouching && _collider.size.y == _crouchHeight)
            {
                ModifyHeightFromBottom(_standHeight);
            }
        }

        public bool CheckCanStand(RaycastConfiguration rayConfig)
        {
            // perform a raycast to the new height using the rayconfig to check if you can stand

            Vector2 currentSize = _collider.size;
            float deltaHeight = _standHeight - currentSize.y;
            Vector2 dir = Vector2.up;
            float length = deltaHeight + 2 * rayConfig.SkinWidth;
            for (int i = 0; i < rayConfig.RaycastCountVertical; i++)
            {
                Vector2 origin = rayConfig.Origins.TopLeft + (rayConfig.RaySpacingX * i);
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, length, rayConfig.CollisionLayer);

                Debug.DrawRay(origin, dir * length, Color.green, .5f);

                if (hit)
                {
                    return false;
                }
            }
            return true;
        }
    }
}