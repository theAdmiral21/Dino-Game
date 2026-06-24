using System.Collections.Generic;
using Game.Core.Effects;

namespace PlayerController.Application.Abstractions
{
    public interface IEffectDriver
    {
        public void EnqueueEffectResults(List<IEffectResult> effectResults);
    }
}