using Enemy.Core.Detectors;
using UnityEngine;

namespace Enemy.Application.DataStructures
{
    public class SearchData : ISearchData
    {
        public Vector2 CurrentPosition { get; private set; }

        public Vector2 Destination { get; private set; }

        public SearchData(Vector2 currentPos, Vector2 dest)
        {
            CurrentPosition = currentPos;
            Destination = dest;
        }
    }
}