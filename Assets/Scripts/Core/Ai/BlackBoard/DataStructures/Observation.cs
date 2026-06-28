using UnityEngine;

namespace Core.Ai.BlackBoard.DataStructures
{
    [System.Serializable]
    public struct Observation<T>
    {
        public float TimeOfObservation;
        public T Data;
        // Another secret that I think is justified
        public float Age => Time.time - TimeOfObservation;
        public bool IsStale(float maxAge) => Age > maxAge;
    }
}