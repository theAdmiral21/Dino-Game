using System.Collections.Generic;
using Environment.Core.Level;

namespace Environment.Core.Abstractions
{
    public interface ICheckpointRegistry
    {
        public IReadOnlyCollection<ICheckpoint> Checkpoints { get; }
    }
}