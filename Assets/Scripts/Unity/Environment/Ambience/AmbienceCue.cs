using System.Collections.Generic;
using Core.Environment.Ambience;
using Primitives.Environment.Platforms;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Environment.Ambience
{
    [System.Serializable]
    public struct AmbienceCue
    {
        public ObjectiveId TriggerObjective;
        public List<SerializedInterface<IAmbienceEffect>> Effects;
    }
}