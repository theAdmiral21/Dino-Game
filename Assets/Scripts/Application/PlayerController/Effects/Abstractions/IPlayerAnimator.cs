using Core.Movement.Inputs;
using Game.Core.Effects;
using Movement.Core.Abstractions;
using PlayerController.Application.Effects.DataStructures;
using Primitives.Physics;

namespace PlayerController.Application.Effects.Abstractions
{
    public interface IPlayerAnimator
    {
        public PlayerAnimatorState CurrentState { get; }

        /// <summary>
        /// Method for animating continuous movement. Think running, falling, idle, etc.
        /// </summary>
        /// <param name="physicsContext"></param>
        /// <param name="ruleState"></param>
        public void SyncAnimation(IActorInput inputValues, PhysicsContext physicsContext, in IRuleState ruleState);

        /// <summary>
        /// Method for animation one-shot animations. Think jumping, landing, barking, pouncing, etc.
        /// </summary>
        /// <param name="effect"></param>
        public void ApplyEffect(IEffectResult effect);

    }
}