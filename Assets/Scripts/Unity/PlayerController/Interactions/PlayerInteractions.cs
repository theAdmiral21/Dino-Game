using System.Collections.Generic;
using Environment.Core.Interactions;
using Game.Core.Execution;
using Game.Core.Inputs;
using Infrastructure.Core.Inputs;
using Infrastructure.Unity.Registries;
using Movement.Core.Movement.Abstractions;
using Physics.Core.PhysicsActors;
using PlayerController.Core.Info;
using PlayerController.Core.Interactions;
using PlayerController.Core.Movement.Abstractions;
using Unity.Common.Unity;
using UnityEngine;

namespace PlayerController.Unity.Interactions
{
    public class PlayerInteractions : SelfRegister<IInitializable<IGameContext>>, IInteract, IInitializable<IGameContext>
    {
        [Header("Interaction settings")]
        [SerializeField] bool _drawDebug;
        [SerializeField] private LayerMask _interactLayer;
        [SerializeField] private Vector2 _boxSize;

        [Header("Required references")]
        [SerializeField] private Transform _facingTransform;
        [SerializeField] private Transform _rootTransform;
        private Vector3 _facingScale;
        [SerializeField] private SerializedInterface<IActorEventBusProvider> _actorEventBusMono;
        private IActorEventBus _actorEventBus => _actorEventBusMono.Interface.ActorEventBus;
        [SerializeField] private SerializedInterface<IPlayerInfoProvider> _playerInfoMono;
        private IPlayerInfoProvider _playerInfo => _playerInfoMono.Interface;
        [SerializeField] private SerializedInterface<IPlayerActionMapManager> _actionMapManagerMono;
        private IPlayerActionMapManager _playerActionMap => _actionMapManagerMono.Interface;
        [SerializeField] private SerializedInterface<IConversationInputReader> _conversationMono;
        private IConversationInputReader _conversationReader => _conversationMono.Interface;

        [SerializeField] private SerializedInterface<IUIInputProvider> _menuInputReaderMono;
        private IUIInputProvider _menuInputReader => _menuInputReaderMono.Interface;

        private ContactFilter2D _contactFilter;
        private Vector2 _parentPosition => transform.parent.position;
        private InteractContext _context
        {
            get
            {
                if (_cachedContext == null)
                {
                    _cachedContext = new InteractContext(
                        _playerInfo.PlayerInfo.CharacterId,
                        _playerActionMap,
                        _conversationReader,
                        _menuInputReader,
                        transform.parent.position);
                }
                return _cachedContext;
            }
        }
        private InteractContext _cachedContext;

        [SerializeField] private int _priority = 0;
        public int Priority => _priority;

        private void Awake()
        {
            base.Awake();
            _contactFilter = new ContactFilter2D();
            _contactFilter.SetLayerMask(_interactLayer);
            _contactFilter.useLayerMask = true;
            _facingScale = _rootTransform.localScale;
        }
        public void Initialize(IGameContext context)
        {
            _actorEventBus.OnActionApproved += HandleInteract;
        }
        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_actorEventBus != null, "Failed to set actor event bus");
        }

        private void HandleInteract(IActionResult result)
        {
            if (result is InteractResult) Interact();

        }
        public void Interact()
        {
            // Debug.Log($"Attempting to interact");
            // FInd the closest interactable object
            IInteractable interactable = GetInteractables();
            // If we didn't find anything return
            if (interactable == null) return;

            // Debug.Log($"Got interactables");

            if (interactable.CanInteract())
            {
                if (interactable is IContextInteractable contextInteraction)
                {
                    contextInteraction.Interact(_context);
                }
                else
                {
                    // call the interact method
                    interactable.Interact();
                }
            }
        }

        private IInteractable GetInteractables()
        {
            // Debug.LogError($"This needs to take into account whatever direction the player is facing");
            // reset the distance
            float dist = Mathf.Infinity;

            // Perform a rectangle cast in front of the player
            List<Collider2D> results = new();
            UpdateFacing();
            int hits = Physics2D.OverlapBox(transform.position, _boxSize, 0f, _contactFilter, results);

            if (_drawDebug) DrawBox();

            IInteractable target = null;

            // Debug.Log($"Got {hits} hits");

            for (int i = 0; i < hits; i++)
            {
                var col = results[i];

                // Ignore self
                if (col.transform == transform || col.transform.IsChildOf(transform))
                    continue;

                // Debug.Log($"{col.name}");
                var interactable = col.GetComponent<IInteractable>();
                if (interactable == null) continue;

                float checkDistance = GetDistance(col);
                // Debug.Log($"Distance: {checkDistance}");
                if (checkDistance < dist)
                {
                    target = interactable;
                    dist = checkDistance;
                }
            }
            return target;
        }

        private void UpdateFacing()
        {
            _facingScale.x = Mathf.Sign(_facingTransform.localScale.x);
            _rootTransform.localScale = _facingScale;
        }

        private void DrawBox()
        {
            Vector2 center = transform.position;
            Vector2 extents = _boxSize / 2;

            Vector2 topLeft = new Vector2(center.x - extents.x, center.y + extents.y);
            Vector2 bottomLeft = new Vector2(center.x - extents.x, center.y - extents.y);
            Vector2 topRight = new Vector2(center.x + extents.x, center.y + extents.y);
            Vector2 bottomRight = new Vector2(center.x + extents.x, center.y - extents.y);

            Debug.DrawLine(topLeft, topRight, Color.red, 0.5f);
            Debug.DrawLine(topLeft, bottomLeft, Color.red, 0.5f);
            Debug.DrawLine(topRight, bottomRight, Color.red, 0.5f);
            Debug.DrawLine(bottomLeft, bottomRight, Color.red, 0.5f);
        }

        private float GetDistance(Collider2D hit)
        {
            // Figure out how far the hit is from the player
            return Vector2.Distance(hit.transform.position, _parentPosition);
        }
    }
}