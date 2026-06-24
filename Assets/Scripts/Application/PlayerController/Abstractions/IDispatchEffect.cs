using System;
using Core.Movement.Inputs.DataStructures;
using Game.Core.Effects;
using Movement.Core.State.DataStructures;
using PlayerController.Core.Effects.Abstractions;

namespace PlayerController.Application.Abstractions
{
    public interface IDispatchEffect
    {
        public Type EffectType { get; }

        IEffectResult DispatchUntyped(
            IEffectRequest request,
            in InputState inputs,
            ref PlayerRuleState ruleState);
    }
}