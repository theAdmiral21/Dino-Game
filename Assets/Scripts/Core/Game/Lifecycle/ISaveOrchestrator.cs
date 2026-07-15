using System.Collections.Generic;
using Primitives.Audio.EntityKeys;
using Primitives.SaveData;

namespace Core.Game.Lifecycle
{
    public interface ISaveOrchestrator
    {
        public int Id { get; }
        public EntityKey Key { get; }
        public IRevertable[] DiscoverRevertables();
        public void OrchestrateSnapShot();
        public void OrchestrateRevert();
        public OrchestratorSaveData OrchestrateSerialization();
        public void OrchestrateLoadSnapShot(OrchestratorSaveData saveData);
    }
}