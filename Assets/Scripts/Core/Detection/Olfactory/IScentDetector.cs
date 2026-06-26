using System;
using Core.Detection.Olfactory.DataStructures;

namespace Core.Detection.Olfactory
{
    public interface IScentDetector
    {
        public event Action<OlfactoryData> ScentEvent;
        public float Sensitivity { get; }
        public void Scent();
    }
}