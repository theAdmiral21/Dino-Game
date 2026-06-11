using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Effects;
using PlayerController.Application.Abstractions;
using PlayerController.Application.Effects.DataStructures;
using PlayerController.Core.Effects.Abstractions;

namespace PlayerController.Application.Effects.Dispatchers
{
    public sealed class EffectDispatcher
    {
        private List<IEffectResult> _effectResults = new List<IEffectResult>();
        private Dictionary<Type, IDispatchEffect> _dispatchers;
        public EffectDispatcher(IEnumerable<IDispatchEffect> dispatchers)
        {
            _dispatchers = dispatchers.ToDictionary(
                d => d.EffectType,
                d => d
            );
        }
        public List<IEffectResult> DispatchEffects(PlayerEffectContext context)
        {
            _effectResults.Clear();
            foreach (IEffectRequest req in context.CurrentRequests)
            {
                if (_dispatchers.TryGetValue(req.GetType(), out var dispatcher))
                {
                    IEffectResult result = dispatcher.DispatchUntyped(
                       req,
                       context.InputValues,
                       ref context.RuleState
                   );
                    _effectResults.Add(result);
                }
            }
            return _effectResults;
        }
    }

}