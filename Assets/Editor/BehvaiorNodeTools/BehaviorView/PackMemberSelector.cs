using System.Collections.Generic;
using Core.Ai.BlackBoard;
using Unity.Ai.BlackBoard;
using UnityEditor;
using UnityEngine;

namespace Editor.BehaviorNodeTools.BehaviorView
{
    public class PackMemberSelector
    {
        public IPackManager SelectedPack;
        public IPackMember SelectedMember;

        private List<IPackManager> _packManagers = new();
        private List<string> _managerNames = new();
        private List<IPackMember> _packMembers = new();
        private List<string> _memberNames = new();
        private int _managerNdx;
        private int _memberNdx;

        public void Refresh()
        {
            PackManager[] managers = UnityEngine.Object.FindObjectsByType<PackManager>(FindObjectsSortMode.None);
            _packManagers.Clear();
            _managerNames.Clear();
            foreach (var manager in managers)
            {
                var temp = manager.gameObject.GetComponentInChildren<IPackManager>();
                if (temp == null) continue;
                _packManagers.Add(temp);
                _managerNames.Add($"Manager {manager.GetInstanceID()}");
            }
        }

        public void DrawIMGUI()
        {
            if (GUILayout.Button("Refresh Managers")) Refresh();
            if (_packManagers.Count == 0) return;

            _managerNdx = GUILayout.SelectionGrid(_managerNdx, _managerNames.ToArray(), 1, EditorStyles.radioButton);
            SelectedPack = _packManagers[_managerNdx];

            var packData = SelectedPack.Coordinator.Data;
            _packMembers.Clear();
            _memberNames.Clear();
            foreach (int key in packData.Members.Keys)
            {
                _packMembers.Add(packData.Members[key]);
                _memberNames.Add($"Member {packData.Members[key].MemberId}");
            }

            _memberNdx = GUILayout.SelectionGrid(_memberNdx, _memberNames.ToArray(), 1, EditorStyles.radioButton);
            SelectedMember = _packMembers[_memberNdx];
        }
    }
}