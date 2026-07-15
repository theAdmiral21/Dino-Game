using System.Collections.Generic;
using Primitives.Audio.EntityKeys;

namespace Primitives.SaveData
{
    [System.Serializable]
    public struct OrchestratorSaveData
    {
        public EntityKey Key;
        public int Id;
        public List<string> Data;
    }
}