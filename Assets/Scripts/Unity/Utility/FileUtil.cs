using System.IO;
using Primitives.SaveData;
using UnityEngine;

namespace Unity.Utility
{
    public static class FileUtil
    {

        public static void WriteToDisk(string path, object saveData)
        {
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(path, json);
            Debug.Log($"Saved data to: {path}");
            Debug.Log(json);
        }

        public static GameSaveData? ReadFromDisk(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogError($"File at {path} does not exist");
                return null;
            }

            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameSaveData>(json);
        }

        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}