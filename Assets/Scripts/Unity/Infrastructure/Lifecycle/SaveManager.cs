using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using AI.Core.Behavior;
using Core.Game.Lifecycle;
using NUnit.Framework.Constraints;
using Primitives.Audio.EntityKeys;
using Primitives.SaveData;
using Unity.Common.Unity;
using Unity.Utility;
using UnityEngine;

namespace Unity.Infrastructure.Lifecycle
{
    public class SaveManager : MonoBehaviour, ISaveManager
    {
        [SerializeField] private SerializedInterface<ISaveRegistry> _saveRegistryMono;
        private ISaveRegistry _saveRegistry => _saveRegistryMono.Interface;
        private IReadOnlyCollection<ISaveOrchestrator> _orchestrators => _saveRegistry.SaveOrchestrators;

        [SerializeField] private string _fileName = "save.json";
        private string _savePath => Path.Combine(UnityEngine.Application.persistentDataPath, _fileName);

        public static SaveManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"INSTANTIATED SaveManager {GetEntityId()}");
        }
        private void OnDestroy()
        {
            Debug.Log($"DESTROYED SaveManager {GetEntityId()}");
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public void SerializeEntities()
        {
            List<OrchestratorSaveData> data = new();
            foreach (var orchestrator in _orchestrators)
            {
                data.Add(orchestrator.OrchestrateSerialization());
            }
            GameSaveData saveData = new GameSaveData
            {
                SaveTime = DateTime.Now,
                Data = data
            };
            WriteToDisk(saveData);
        }

        public void SnapShotEntities()
        {
            foreach (var orchestrator in _orchestrators)
            {
                orchestrator.OrchestrateSnapShot();
            }
        }

        public void RevertEntities()
        {
            // Before you can revert you need to make sure you at least have serialized data to revert to
            if (!HasSaveData())
            {
                Debug.LogError($"Unable to get serialized data to revert to. Check file at: {_savePath}");
                return;
            }

            // Now distribute the data to everyone
            LoadEntitiesSnapShot();

            // Finally, revert everyone
            foreach (var orchestrator in _orchestrators)
            {
                orchestrator.OrchestrateRevert();
            }
        }

        public void ResetEntities()
        {
            foreach (var orchestrator in _orchestrators)
            {
                orchestrator.OrchestrateSnapShot();
            }
        }

        public void LoadEntitiesSnapShot()
        {
            GameSaveData? saveData = ReadFromDisk();
            if (!saveData.HasValue)
            {
                Debug.LogError($"Failed to read save data at: {_savePath}");
            }
            foreach (OrchestratorSaveData data in saveData.Value.Data)
            {
                ISaveOrchestrator orchestrator = GetOrchestrator(data.Key, data.Id);
                orchestrator.OrchestrateLoadSnapShot(data);
            }
        }
        private ISaveOrchestrator GetOrchestrator(EntityKey key, int id)
        {
            foreach (var orchestrator in _orchestrators)
            {
                if (orchestrator.Key == key && orchestrator.Id == id)
                {
                    return orchestrator;
                }
            }
            Debug.LogError($"Unable to find orchestrator with EntityKey: {key} and Id: {id}");
            return null;
        }
        private void WriteToDisk(GameSaveData saveData)
        {
            FileUtil.WriteToDisk(_savePath, saveData);
        }

        private GameSaveData? ReadFromDisk()
        {
            // Is it worth it to cache this value? It shouldn't change often..
            return FileUtil.ReadFromDisk(_savePath);
        }

        private bool HasSaveData()
        {
            GameSaveData? saveData = ReadFromDisk();
            if (!saveData.HasValue) return false;

            if (saveData.Value.Data == null || saveData.Value.Data.Count <= 0) return false;

            return true;
        }
    }
}