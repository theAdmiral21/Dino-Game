using System;
using System.Collections.Generic;
using System.Linq;
using Codice.Client.Common.GameUI;
using Environment.Core.Level;
using Primitives.Checkpoints;
using Unity.Environment.Checkpoints.DataStructures;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Unity.Tools.DebugTools
{
    [CustomEditor(typeof(SpawnAt))]
    public class SpawnAtEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // make a toggle field for spawning the player
            SpawnAt spawnTool = (SpawnAt)target;

            bool newSpawnDebug = EditorGUILayout.Toggle("Spawn Debug Player", spawnTool.SpawnDebugPlayer);

            if (newSpawnDebug != spawnTool.SpawnDebugPlayer)
            {
                Undo.RecordObject(spawnTool, "Toggle Spawn Debug");
                spawnTool.SpawnDebugPlayer = newSpawnDebug;
                EditorUtility.SetDirty(spawnTool);
            }


            // make a selector
            var selector = (SpawnAt)target;
            // get the check points
            Dictionary<string, CheckpointId> spawnPoints = GetSpawnPoints();

            // Build the drop down
            string[] names = spawnPoints.Keys.ToArray();
            int currentIndex = Array.IndexOf(names, selector.SelectedSpawn.ToString());

            int newIndex = EditorGUILayout.Popup("Spawn Point", currentIndex, names);

            if (newIndex != currentIndex)
            {
                Undo.RecordObject(selector, "Change Spawn Point");
                selector.SelectedSpawn = spawnPoints[names[newIndex]];
                EditorUtility.SetDirty(selector);
            }
        }

        private Dictionary<string, CheckpointId> GetSpawnPoints()
        {
            Dictionary<string, CheckpointId> checkpoints = new();

            GameObject[] gObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");

            foreach (var gObject in gObjects)
            {
                ICheckpoint checkpoint = gObject.GetComponent<ICheckpoint>();
                if (checkpoint != null)
                {
                    string name = checkpoint.Id.ToString();
                    checkpoints[name] = checkpoint.Id;
                }
            }
            return checkpoints;
        }

    }
}