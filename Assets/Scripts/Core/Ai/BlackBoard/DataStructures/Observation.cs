using UnityEngine;

namespace Core.Ai.BlackBoard.DataStructures
{
    public struct Observation<T>
    {
        public float TimeOfObservation;
        public T Data;
        // Another secret that I think is justified
        public float Age => Time.time - TimeOfObservation;
        public bool IsStale(float maxAge) => Age > maxAge;
    }
}