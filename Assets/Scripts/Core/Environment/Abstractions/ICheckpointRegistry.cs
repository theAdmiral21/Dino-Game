using System.Collections.Generic;
using Environment.Core.Level;
using Infrastructure.Core.Registries;

namespace Environment.Core.Abstractions
{
    public interface ICheckpointRegistry
    {
        public IReadOnlyCollection<ICheckpoint> Checkpoints { get; }
    }
}