using UnityEngine;

namespace Core.Detection.Olfactory.DataStructures
{
    /// <summary>
    /// A struct used to store data emitted by a scent emitter. Olfactory data has a limited life time as a scent degrades over time. As scents decay so does their ScentIntensity eventually reaching zero and becoming imperceptible.
    /// </summary>
    public struct OlfactoryData
    {
        public float ScentIntensity { get; private set; }
        public readonly Vector2 ScentPosition;
        // public readonly HealthState Health;
        // public readonly EntityType Entity; // does this even matter? Am I going to have the dinos chasing one another?
        public readonly float ScentDirection; // this is a float because you have to follow the trail left or right. If the trail goes cold, you have to find it.
        private float _lifeCounter;
        private float _lifeTime;
        private float _maxIntensity;

        public OlfactoryData(Vector2 scentPosition, float lifetime, float intensity, float scentDirection)
        {
            ScentPosition = scentPosition;
            _lifeTime = lifetime;
            _lifeCounter = lifetime;
            _maxIntensity = intensity;
            ScentIntensity = _maxIntensity;
            // Health = health;
            // Entity = entity;
            ScentDirection = scentDirection;
        }

        public void DecayIntensity(float dt)
        {
            _lifeCounter -= dt;
            if (_lifeCounter > 0)
            {
                // degrade the intensity
                ScentIntensity *= _lifeCounter / _lifeTime;
            }
        }
    }
}