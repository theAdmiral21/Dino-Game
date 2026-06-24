using System.Collections.Generic;

namespace Game.Core.Effects
{
    public interface IEffectOrchestrator
    {
        public void EnqueueEffectResults(List<IEffectResult> effectResults);
    }
}