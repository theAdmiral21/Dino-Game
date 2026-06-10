using UnityEngine;

namespace Environment.Core.Level
{
    public interface ISpawnPoint
    {
        public Vector2 Spawn { get; }
        public int Index { get; }
    }
}