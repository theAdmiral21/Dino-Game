using System.Collections.Generic;
using Core.Physics.Triggers;

namespace Core.Physics.Abstractions
{
    public interface IDetectTrigger
    {
        public void ResolveTriggers();
    }
}