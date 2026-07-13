using Primitives.Checkpoints;
using UnityEngine;

namespace Primitives.SaveData
{
    [System.Serializable]
    public struct CheckpointSaveData
    {
        // Will this need to track it's old GUID?
        public CheckpointId Id;
    }
}