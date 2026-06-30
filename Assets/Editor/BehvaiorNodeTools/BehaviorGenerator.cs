using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.Callbacks;
using System;

namespace Editor
{
    public static class BehaviorGenerator
    {
        private const string ASSET_PATH = @"Assets/Scripts/Unity/Ai/BehaviorTree/Assets/";
        private const string SO_PATH = @"Assets/Scripts/Unity/Ai/BehaviorTree/BehaviorNodes/";
        private const string CLASS_PATH = @"Assets/Scripts/Application/Ai/BehaviorTreeNodes/Actions/";

        [MenuItem("Tools/Behavior/Create New Behavior")]
        public static void OpenWindow()
        {
            BehaviorCreatorWindow.Open();
        }
        public static void CreateBehavior(string behaviorName, string contextName)
        {
            if (string.IsNullOrWhiteSpace(behaviorName))
            {
                Debug.LogError("Invalid behavior name");
                return;
            }
            if (string.IsNullOrWhiteSpace(contextName))
            {
                Debug.LogError("Invalid context name");
                return;
            }

            // This should convert RaptorContext to just Raptor
            string shortHand = contextName.Replace("Context", string.Empty);
            if (string.IsNullOrEmpty(shortHand))
            {
                Debug.LogError($"shortHand name for the context name was null after removing \"context\"");
                throw new IOException();
            }

            // Write the templates
            WriteNodeFile(behaviorName);
            WriteSOFile(behaviorName, contextName, shortHand);

            // Give editor prefs the data it needs to build your SO
            EditorPrefs.SetString("SOName", $"{shortHand}{behaviorName}SO");
            EditorPrefs.SetString("SOShortHand", $"{shortHand}");

            // Refresh the asset data base
            AssetDatabase.Refresh();
        }

        private static void WriteNodeFile(string behaviorName)
        {
            string filePath = $"{CLASS_PATH}{behaviorName}.cs";
            VerifyUnique(filePath);
            File.WriteAllText(filePath, BehaviorTemplateProvider.GetNodeClassTemplate(behaviorName));
        }

        private static void WriteSOFile(string behaviorName, string contextName, string shortHand)
        {
            string filePath = $"{SO_PATH}{shortHand}/{shortHand}{behaviorName}SO.cs";
            VerifyUnique(filePath);
            File.WriteAllText(filePath, BehaviorTemplateProvider.GetNodeSOTemplate(behaviorName, contextName, shortHand));
        }

        [DidReloadScripts]
        private static void GenerateSO()
        {
            string soName = EditorPrefs.GetString("SOName", String.Empty);
            if (string.IsNullOrEmpty(soName)) return;
            // Consume the key
            EditorPrefs.DeleteKey("SOName");

            string shortHand = EditorPrefs.GetString("SOShortHand", String.Empty);
            if (string.IsNullOrEmpty(shortHand)) return;
            // Consume the key
            EditorPrefs.DeleteKey("SOShortHand");

            Type type = GetTypeByName($"Unity.AI.BehaviorTree.{soName}");

            if (type == null)
            {
                Debug.LogError($"Could not find generated type: {soName}");
                return;
            }

            ScriptableObject asset = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(asset, $"{ASSET_PATH}{shortHand}/{soName}.asset");
            AssetDatabase.SaveAssets();

            Debug.Log($"✅ Generated new behavior asset successfully.");
        }

        private static void VerifyUnique(string filePath)
        {
            if (File.Exists(filePath))
            {
                throw new IOException();
            }
        }

        private static Type GetTypeByName(string fullName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(fullName);
                if (type != null) return type;
            }
            return null;
        }
    }
}