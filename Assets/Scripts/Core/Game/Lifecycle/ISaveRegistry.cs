using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Core.Game.Lifecycle
{
    public interface ISaveRegistry
    {
        public IReadOnlyCollection<IResetable> Resetables { get; }
        // public IReadOnlyCollection<ISnapShotable> SnapShotables { get; }
        public IReadOnlyCollection<ISaveOrchestrator> SaveOrchestrators { get; }
    }
}