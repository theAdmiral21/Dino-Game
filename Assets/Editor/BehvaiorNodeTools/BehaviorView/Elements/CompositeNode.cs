using Unity.AI.BehaviorTree;
using UnityEditor.Experimental.GraphView;

namespace Editor.BehaviorNodeTools.BehaviorView.Elements
{
    public class CompositeNode : BaseNode
    {


        public CompositeNode(BehaviorNodeSO nodeData, BaseNode parent) : base(nodeData, parent)
        {
            // add an input port on the left for composites to connect to
            InputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            InputPort.portName = "Parent Connections";
            inputContainer.Add(InputPort);

            OutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
            OutputPort.portName = "Child Connections";

            inputContainer.Add(OutputPort);
        }

        public void Initialize()
        {
            Draw();
        }

        public override void Draw()
        {
            title = NodeName;


        }
    }
}