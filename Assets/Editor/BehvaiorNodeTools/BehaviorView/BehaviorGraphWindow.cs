using System;
using Unity.AI.BehaviorTree;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.BehaviorNodeTools.BehaviorView
{
    public class BehaviorGraphWindow : EditorWindow
    {
        private BehaviorGraph _graphView;
        private BehaviorNodeSO _data;

        [MenuItem("Tools/Behavior/Behavior Graph View")]
        public static void Open()
        {
            GetWindow<BehaviorGraphWindow>("Behavior Graph View");
        }

        public void CreateGUI()
        {
            Debug.Log($"Creating gui");
            // create an object field
            ObjectField treeData = new ObjectField("Root Node")
            {
                objectType = typeof(BehaviorNodeSO),
            };
            // configure the data on drop
            treeData.RegisterValueChangedCallback(UpdateData);
            // add the field and graph to the view
            rootVisualElement.Add(treeData);

            // Setup the window
            // AddGraphView();
        }

        private void UpdateData(ChangeEvent<UnityEngine.Object> evt)
        {
            _data = evt.newValue as BehaviorNodeSO;
            if (_data != null)
            {
                _graphView.DrawFromData(_data);
            }
        }

        // private void OnGUI()
        // {
        //     // Set up the view
        //     EditorGUILayout.BeginHorizontal();
        //     // Add a space for selecting managers
        //     EditorGUILayout.BeginVertical(GUILayout.Width(200));

        //     // create an object field
        //     ObjectField treeData = new ObjectField("Root Node")
        //     {
        //         objectType = typeof(BehaviorNodeSO),
        //     };
        //     // configure the data on drop
        //     treeData.RegisterValueChangedCallback(UpdateData);

        //     // add the field and graph to the view
        //     rootVisualElement.Add(treeData);
        //     EditorGUILayout.EndVertical();

        //     EditorGUILayout.EndHorizontal();
        // }

        private void OnEnable()
        {
            EditorApplication.update += OnEditorUpdate;



            // Setup the window
            AddGraphView();

        }

        private void OnDisable()
        {
            EditorApplication.update -= OnEditorUpdate;
        }

        private void OnEditorUpdate()
        {
            if (UnityEngine.Application.isPlaying)
            {
                Repaint();
            }
        }

        private void AddGraphView()
        {
            _graphView = new BehaviorGraph();
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

    }
}