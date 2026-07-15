using System;
using System.Collections.Generic;

namespace Primitives.SaveData
{
    [System.Serializable]
    public struct GameSaveData
    {
        public DateTime SaveTime;
        public List<OrchestratorSaveData> Data;
    }
}