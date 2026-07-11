using AI.Core.Behavior;
using Core.Ai.Behavior.Visualization;
using Editor.BehaviorNodeTools.BehaviorView.Elements;
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
        private PackMemberSelector _selector = new();


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


        private void OnEnable()
        {
            EditorApplication.update += OnEditorUpdate;



            // Setup the window
            AddGraphView();
            AddSelector();

        }

        private void OnDisable()
        {
            EditorApplication.update -= OnEditorUpdate;
        }

        private void OnEditorUpdate()
        {
            if (!UnityEngine.Application.isPlaying || _selector.SelectedMember == null) return;
            var liveRoot = _selector.SelectedMember.RaptorController.RootNode;
            if (liveRoot != null)
                ApplyLiveOverlay(_graphView.RootNode, liveRoot);
        }

        private void AddGraphView()
        {
            _graphView = new BehaviorGraph();
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

        private void AddSelector()
        {
            var selectorContainer = new IMGUIContainer(() =>
       {
           if (UnityEngine.Application.isPlaying)
               _selector.DrawIMGUI();
       });
            rootVisualElement.Add(selectorContainer); // consider styling this into a fixed-height sidebar

        }

        private void ApplyLiveOverlay(BaseNode graphNode, IInspectableNode runtimeNode)
        {
            bool ranRecently = (Time.time - runtimeNode.LastTickTime) < 0.1f;
            graphNode.SetResultColor(!ranRecently ? Color.gray : runtimeNode.LastResult switch
            {
                NodeResult.Running => Color.yellow,
                NodeResult.Success => Color.green,
                NodeResult.Failure => Color.red,
                _ => Color.white,
            });

            var graphChildren = graphNode.ChildNodes; // however you're tracking these
            var runtimeChildren = runtimeNode.Children;
            for (int i = 0; i < graphChildren.Count && i < runtimeChildren.Count; i++)
                ApplyLiveOverlay(graphChildren[i], (IInspectableNode)runtimeChildren[i]);
        }

    }
}