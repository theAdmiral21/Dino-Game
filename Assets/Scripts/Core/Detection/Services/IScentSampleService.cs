using System.Collections.Generic;
using Core.Detection.Olfactory.DataStructures;
using UnityEngine;

namespace Core.Detection.Services
{
    public interface IScentSampleService
    {
        public List<OlfactoryData> Sample(Vector2 position, float scentRadius);
    }
}