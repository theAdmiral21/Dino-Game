using System.Collections.Generic;
using Game.Core.Effects;
using PlayerController.Core.Effects.Abstractions;

namespace PlayerController.Application.Abstractions
{
    public interface IEffectDriver
    {
        public void EnqueueEffectResults(List<IEffectResult> effectResults);
    }
}