using System.Collections.Generic;
using System.Linq;
using Game.Core.Execution;
using Infrastructure.Unity;
using UnityEditor;
using UnityEngine;

namespace Editor.InitOrder
{
    public class InitOrderWindow : EditorWindow
    {
        [MenuItem("Tools/Game/Init Order Viewer")]
        public static void Open() => GetWindow<InitOrderWindow>("Init Order");
        private List<IInitializable<IGameContext>> _systems = new();

        private void OnGUI()
        {
            if (GUILayout.Button("Refresh")) Refresh();

            EditorGUILayout.LabelField("Initialization Order", EditorStyles.boldLabel);
            foreach (var system in _systems.OrderBy(s => s.Priority).ToList())
            {
                var mb = system as MonoBehaviour;
                if (mb == null) continue;

                var so = new SerializedObject(mb);
                var priorityProp = so.FindProperty("_priority");

                // Make a nice widow
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(mb.GetType().Name, GUILayout.Width(200));
                EditorGUI.BeginChangeCheck();
                int newVal = EditorGUILayout.DelayedIntField(priorityProp.intValue, GUILayout.Width(50));
                if (EditorGUI.EndChangeCheck())
                {
                    priorityProp.intValue = newVal;
                    so.ApplyModifiedProperties();
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        private void Refresh()
        {
            _systems.Clear();
            // In play mode: walk the actual registry
            if (UnityEngine.Application.isPlaying)
            {
                var registry = FindFirstObjectByType<SceneInitializationRegistry>(); // or however you access _systemRegistry
                if (registry != null)
                    _systems.AddRange(registry.Systems);
            }
            else
            {
                // Out of play mode: find every MonoBehaviour in the scene implementing the interface
                var all = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
                foreach (var mb in all)
                    if (mb is IInitializable<IGameContext> init)
                        _systems.Add(init);
            }
        }
    }
}