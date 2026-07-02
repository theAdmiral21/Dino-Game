using System;
using System.Collections.Generic;
using Unity.AI.BehaviorTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Editor.BehaviorNodeTools.BehaviorView.Elements
{
    public abstract class BaseNode : Node
    {
        public string NodeName { get; set; }
        public Port OutputPort;
        public Port InputPort;
        protected BehaviorNodeSO _nodeData;
        public readonly BaseNode Parent;
        public List<BaseNode> ChildNodes = new();
        public BaseNode(BehaviorNodeSO nodeData, BaseNode parent)
        {
            _nodeData = nodeData;

            NodeName = _nodeData.name;

            Parent = parent;

            parent?.ChildNodes.Add(this);
        }

        public abstract void Draw();

        internal void SetResultColor(Color color)
        {
            titleContainer.style.borderTopWidth = 3;
            titleContainer.style.borderBottomWidth = 3;
            titleContainer.style.borderLeftWidth = 3;
            titleContainer.style.borderRightWidth = 3;

            titleContainer.style.borderTopColor = color;
            titleContainer.style.borderBottomColor = color;
            titleContainer.style.borderLeftColor = color;
            titleContainer.style.borderRightColor = color;
        }
    }
}