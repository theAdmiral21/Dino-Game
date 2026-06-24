using UnityEngine;

namespace Enemy.Core.Detectors
{
    public interface ISearchData
    {
        public Vector2 CurrentPosition { get; }
        public Vector2 Destination { get; }
    }
}