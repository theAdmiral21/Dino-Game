using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;
using Editor.BehaviorNodeTools.BehaviorView.Elements;
using System.Drawing;
using Unity.AI.BehaviorTree;
using NPC.Application.BehaviorContexts;
using System.Linq;
using Core.Ai.Behavior.Visualization;

namespace Editor.BehaviorNodeTools.BehaviorView
{
    public class BehaviorGraph : GraphView
    {
        private const float HorizontalSpacing = 175f;
        private const float VerticalSpacing = 150f;
        private float _nextLeafSlot; // ticks forward every time we place a leaf
        public BehaviorNodeSO RootSO;
        public BaseNode RootNode;
        private List<BaseNode> _nodes = new();
        public BehaviorGraph()
        {
            AddGrid();

            AddManipulators();
        }
        public void DrawFromData(BehaviorNodeSO data)
        {
            // Clean up the graph
            this.DeleteElements(this.graphElements.ToList());
            _nodes.Clear();

            // Setup the graph with the new data
            RootSO = data;
            _nextLeafSlot = 0f;
            BuildTree(data, 0, null);
            RootNode = _nodes[0];
        }
        private BaseNode BuildTree(BehaviorNodeSO node, int depth, BaseNode parent)
        {
            BaseNode created;

            // every node — leaf or composite — claims its own row before children are considered
            float rowY = _nextLeafSlot * VerticalSpacing;
            _nextLeafSlot += 1f;

            switch (node)
            {
                case SequenceSO sequence:
                    created = AddSequencerNode(sequence, parent);
                    _nodes.Add(created);
                    created.SetPosition(new Rect(depth * HorizontalSpacing, rowY, 160, 80));
                    foreach (var child in sequence.Children)
                        BuildTree(child, depth + 1, created);
                    break;

                case SelectorSO selector:
                    created = AddSelectorNode(selector, parent);
                    _nodes.Add(created);
                    created.SetPosition(new Rect(depth * HorizontalSpacing, rowY, 160, 80));
                    foreach (var child in selector.Children)
                        BuildTree(child, depth + 1, created);
                    break;

                default:
                    created = AddBehaviorNode(node, parent);
                    _nodes.Add(created);
                    created.SetPosition(new Rect(depth * HorizontalSpacing, rowY, 160, 80));
                    break;
            }

            return created;
        }



        private void AddManipulators()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ContentZoomer());
            // this.AddManipulator(new SelectionDragger());
        }

        private void AddGrid()
        {
            GridBackground grid = new GridBackground();
            grid.StretchToParentSize();

            Insert(0, grid);
        }

        private BehaviorNode AddBehaviorNode(BehaviorNodeSO data, BaseNode parent)
        {
            BehaviorNode node = new BehaviorNode(data, parent);
            node.Initialize();
            ConnectNodes(node, parent);
            AddElement(node);
            return node;
        }

        private CompositeNode AddSelectorNode(SelectorSO data, BaseNode parent)
        {
            CompositeNode compNode = new CompositeNode(data, parent);
            compNode.Initialize();
            ConnectNodes(compNode, parent);
            AddElement(compNode);
            return compNode;
        }

        private CompositeNode AddSequencerNode(SequenceSO data, BaseNode parent)
        {
            CompositeNode compNode = new CompositeNode(data, parent);
            compNode.Initialize();
            ConnectNodes(compNode, parent);
            AddElement(compNode);

            return compNode;
        }

        private void ConnectNodes(BaseNode child, BaseNode parent)
        {
            if (parent == null) return;

            Edge edge = child.InputPort.ConnectTo(parent.OutputPort);

            // Don't forget to add your node!
            AddElement(edge);

            child.RefreshPorts();
            parent.RefreshPorts();
        }

        // Override port connections
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();

            foreach (var port in ports)
            {
                // Do not connect a port to itself
                if (startPort == port)
                    continue;

                // Do not connect ports on the same node
                if (startPort.node == port.node)
                    continue;

                // Do not connect input to input, or output to output
                if (startPort.direction == port.direction)
                    continue;

                // Ensure the data types match (e.g., int to int, flow to flow)
                if (startPort.portType != port.portType)
                    continue;

                compatiblePorts.Add(port);
            }

            return compatiblePorts;
        }

    }
}