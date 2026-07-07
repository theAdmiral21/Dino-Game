using Gameplay.Common.Unity;
using Movement.Core.Abstractions;
using Physics.Core.PhysicsActors;
using UnityEngine;

namespace Unity.Environment
{
    [RequireComponent(typeof(Collider2D))]
    public class PassableObject : MonoBehaviour
    {
        [SerializeField] private TriggerVolume _triggerVolume;

        private int _passableLayer;

        private void Awake()
        {
            _passableLayer = LayerMask.NameToLayer("Passable");

            _triggerVolume.OnVolumeEntered += HandleEntered;
            _triggerVolume.OnVolumeStay += HandleStayed;
            _triggerVolume.OnVolumeExited += HandleExited;
        }

        private void OnDestroy()
        {
            _triggerVolume.OnVolumeEntered -= HandleEntered;
            _triggerVolume.OnVolumeStay -= HandleStayed;
            _triggerVolume.OnVolumeExited -= HandleExited;
        }

        private void HandleExited(IPhysicsActor actor)
        {
            // upon exiting reset the collision for the actor
            actor.Body.RayConfig.SetLayerPassable(_passableLayer, false);
        }

        private void HandleEntered(IPhysicsActor actor)
        {
            // if something enters the trigger

            // if the actor can climb
            var climbState = actor.Brain.GetCapability<IClimbState>();
            // Debug.Log($"Got climb state: {climbState}");
            if (climbState == null) return;

            // if the actor is climbing
            if (climbState.IsClimbing)
            {
                actor.Body.RayConfig.SetLayerPassable(_passableLayer, true);
            }
        }

        private void HandleStayed(IPhysicsActor actor)
        {
            // if the actor can climb
            var climbState = actor.Brain.GetCapability<IClimbState>();
            // Debug.Log($"Got climb state: {climbState}");
            if (climbState == null) return;

            // if the actor is climbing
            if (climbState.IsClimbing)
            {
                actor.Body.RayConfig.SetLayerPassable(_passableLayer, true);
            }
        }


    }
}