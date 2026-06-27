using System;
using Core.Ai.BlackBoard;
using Core.Ai.State.BehaviorContext;
using Core.Detection.Audio.DataStructures;
using Core.Detection.DataStructures;
using Core.Detection.Olfactory.DataStructures;
using Core.Detection.Visual;
using Core.Detection.Visual.DataStructures;
using UnityEngine;

namespace Core.Detection
{
    public interface IDetectorBrain
    {
        public AlertLevel Alertness { get; }
        public void Tick(float dt);
        public void OnAudioEvent(AudioData data);
        public void OnScentEvent(OlfactoryData data);
        public void SetPerceptionContext(IPerceptionContext context);
        public PerceptionState DrawConclusions();
    }
}