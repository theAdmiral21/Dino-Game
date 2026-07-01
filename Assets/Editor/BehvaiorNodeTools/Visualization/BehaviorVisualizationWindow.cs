using System;
using System.Collections.Generic;
using System.Linq;
using AI.Core.Behavior;
using Codice.Client.Common.TreeGrouper;
using Core.Ai.Behavior.Visualization;
using Core.Ai.BlackBoard;
using Core.Ai.BlackBoard.DataStructures;
using Unity.Ai.BlackBoard;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PackVisualizerWindow : EditorWindow
    {
        private IPackManager _selectedPack;
        private IPackMember _selectedMember;
        private List<IPackMember> _packMembers = new();
        private PackData _packData;
        private List<string> _memberNames = new();
        private int _ndx;

        [MenuItem("Tools/Behavior/Pack Visualizer")]
        public static void Open()
        {
            GetWindow<PackVisualizerWindow>("Pack Visualizer");
        }

        private void OnEnable()
        {
            EditorApplication.update += OnEditorUpdate;
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

        private void OnGUI()
        {
            if (!UnityEngine.Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Enter Play Mode to inspect packs", MessageType.Info);
                return;
            }

            // 1. Find and select a pack
            var selectedObject = Selection.activeGameObject;
            // if (selectedObject == null) return;
            IPackManager potentialManager = selectedObject.GetComponentInChildren<IPackManager>();
            // Once a manager is set, don't forget it until a new one is selected
            if (potentialManager != null)
            {
                _selectedPack = potentialManager;
            }


            // 2. List members of selected pack, click to select one
            GetPackMembers();

            // Set up the view
            EditorGUILayout.BeginHorizontal();

            // Dino list on the left
            EditorGUILayout.BeginVertical(GUILayout.Width(200));
            EditorGUILayout.LabelField("Pack Members", EditorStyles.boldLabel);
            _ndx = GUILayout.SelectionGrid(_ndx, _memberNames.ToArray(), 1, EditorStyles.radioButton);
            _selectedMember = _packMembers[_ndx];
            EditorGUILayout.EndVertical();

            // Divider
            GUILayout.Box("", GUILayout.Width(2), GUILayout.ExpandHeight(true));
            // Right panel — tree
            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField("Behavior Tree", EditorStyles.boldLabel);
            DrawTree();
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }

        private void GetPackMembers()
        {
            _packData = _selectedPack.Coordinator.Data;
            _packMembers.Clear();
            _memberNames.Clear();
            List<int> keys = _packData.Members.Keys.ToList();
            foreach (int key in keys)
            {
                _packMembers.Add(_packData.Members[key]);
                _memberNames.Add($"Member {_packData.Members[key].MemberId}");
            }
        }

        private void DrawTree()
        {
            if (_selectedMember != null)
            {
                // Draw the tree for the selected member
                IInspectableNode root = _selectedMember.RaptorController.RootNode;
                if (root == null) return;

                // EditorGUILayout.LabelField("Behavior Tree", EditorStyles.boldLabel);
                DrawNode(root, 0);
            }
        }

        private void DrawNode(IInspectableNode node, int depth)
        {
            EditorGUI.indentLevel = depth;

            // Assign a color based on result
            Color prev = GUI.color;
            bool ranRecently = (Time.time - node.LastTickTime) < 0.1f;
            GUI.color = !ranRecently ? Color.gray : node.LastResult switch
            {
                NodeResult.Running => Color.yellow,
                NodeResult.Success => Color.green,
                NodeResult.Failure => Color.red,
                _ => Color.white,
            };

            EditorGUILayout.LabelField($"{node.DisplayName} [{node.LastResult}]");
            GUI.color = prev;

            foreach (var child in node.Children)
            {
                DrawNode(child, depth + 1);
            }
        }
    }
}