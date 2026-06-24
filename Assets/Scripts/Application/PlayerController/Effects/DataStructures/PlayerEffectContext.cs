using System.Collections.Generic;
using PlayerController.Core.Effects.Abstractions;
using Movement.Core.State.DataStructures;
using Core.Movement.Inputs.DataStructures;

namespace PlayerController.Application.Effects.DataStructures
{
    [System.Serializable]
    public class PlayerEffectContext
    {
        public List<IEffectRequest> CurrentRequests;
        public PlayerRuleState RuleState;
        public InputState InputValues;
    }
}