using System.Collections.Generic;
using Core.Game.Lifecycle;
using Infrastructure.Unity.Registries;
using Primitives.Audio.EntityKeys;
using Primitives.SaveData;
using UnityEngine;

namespace Unity.Infrastructure.Lifecycle
{
    public class SaveOrchestrator : SelfRegister<ISaveOrchestrator>, ISaveOrchestrator
    {
        public int Id { get; private set; }

        public EntityKey Key => _key;
        [SerializeField] private EntityKey _key;

        private IRevertable[] _revertables;
        private void Awake()
        {
            base.Awake();
            _revertables = DiscoverRevertables();
        }

        public IRevertable[] DiscoverRevertables()
        {
            IRevertable[] revertables = GetComponentsInChildren<IRevertable>();
            return revertables;
        }

        public void OrchestrateLoadSnapShot(OrchestratorSaveData saveData)
        {
            for (int i = 0; i < _revertables.Length; i++)
            {
                // revertable and saveData.Data are the same length. Each entry refers to the other
                _revertables[i].LoadSnapShot(saveData.Data[i]);
            }
        }

        public void OrchestrateRevert()
        {
            for (int i = 0; i < _revertables.Length; i++)
            {
                _revertables[i].Revert();
            }
        }

        public OrchestratorSaveData OrchestrateSerialization()
        {
            List<string> data = new();
            for (int i = 0; i < _revertables.Length; i++)
            {
                data.Add(_revertables[i].SerializeSnapShot());
                Debug.Log($"[SaveOrchestrator] {data[i]}");
            }
            return new OrchestratorSaveData
            {
                Id = Id,
                Key = Key,
                Data = data
            };
        }

        public void OrchestrateSnapShot()
        {
            for (int i = 0; i < _revertables.Length; i++)
            {
                _revertables[i].TakeSnapShot();
            }
        }
    }
}