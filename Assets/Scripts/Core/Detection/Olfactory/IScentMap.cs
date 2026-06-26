using System.Collections.Generic;
using Core.Detection.Olfactory.DataStructures;
using Core.Detection.Services;
using UnityEngine;

namespace Core.Detection.Olfactory
{
    public interface IScentMap : IScentDepositService, IScentSampleService
    {
        // public void Deposit(OlfactoryData data);

        // public List<OlfactoryData> Sample(Vector2 position, float scentRadius);

        public void Decay(float dt);
    }
}