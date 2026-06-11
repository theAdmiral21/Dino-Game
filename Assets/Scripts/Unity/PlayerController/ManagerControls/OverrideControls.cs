using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;
using Physics.Core.Services;
using PlayerController.Core.ManagerControls.Abstractions;
using PlayerController.Unity.Inputs;
using Unity.Common.Unity;
using UnityEngine;

namespace PlayerController.Unity.ManagerControls
{
    public class OverrideControls : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>, IOverrideControls
    {
        public bool PlayerEnabled => _playerEnabled;

        public int Priority => 36; // I've been assigning these at random.. I should probably stop doing that

        private bool _playerEnabled = true;
        [SerializeField] private PlayerActionMapManager _actionMapManager;
        [SerializeField] private SerializedInterface<IPhysicsActor> _physicsActor;
        [SerializeField] private PlayerActionOrchestrator _playerActionOrchestrator;
        private IForceMoveActor forceMoveActor;

        [Header("Debug Options")]
        public bool DisableInputs;
        public bool DisablePhysics;
        public bool ClearRequests;

        private void Update()
        {
            if (DisableInputs)
            {
                _actionMapManager.DisableInputs();
            }
            else
            {
                if (!_actionMapManager.IsEnabled)
                {
                    _actionMapManager.EnableInputs();
                }
            }

            if (DisablePhysics)
            {
                _physicsActor.Interface.Sleep();
            }
            else
            {
                if (_physicsActor.Interface.IsAsleep)
                {
                    _physicsActor.Interface.WakeUp();
                }
            }

            if (DisableInputs)
            {
                // _playerActionOrchestrator.ClearRequestList();
            }


        }

        public void ResetPlayer()
        {
            throw new System.NotImplementedException();
        }

        public void HaltCoroutines()
        {
            // I don't think I have any of these
            throw new System.NotImplementedException();
        }

        public void SetPlayerActive(bool setActive)
        {
            gameObject.SetActive(setActive);
            _playerEnabled = setActive;
        }

        public void EnablePlayer()
        {
            // Clear pending requests
            // _playerActionOrchestrator.ClearRequestList();
            // Enable physics
            _physicsActor.Interface.WakeUp();
            // Reactivate the root game object
            SetPlayerActive(true);
            // Enable inputs
            // Debug.Log($"Inputs Enabled - Frame: {Time.frameCount}");
            _actionMapManager.EnableInputs();
        }

        public void DisablePlayer()
        {
            // Disable inputs
            // Debug.Log($"Inputs disabled - Frame: {Time.frameCount}");
            _actionMapManager.DisableInputs();
            // Clear pending requests
            // _playerActionOrchestrator.ClearRequestList();
            // Disable physics
            _physicsActor.Interface.Sleep();
            // Deactivate the root game object
            SetPlayerActive(false);
            Debug.Log($"Player disabled: - Frame: {Time.frameCount}");
        }

        public void OverrideMove(Vector2 newPosition)
        {
            if (PlayerEnabled)
            {
                DisablePlayer();
            }

            // Move the player
            forceMoveActor.ForceMoveTo(_physicsActor.Interface, newPosition);

        }

        public void Initialize(IGameContext context)
        {
            forceMoveActor = context.PhysicsServices.ForceMoveActor;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(forceMoveActor != null, "Failed to initialize forceMoveActor");
        }
    }
}