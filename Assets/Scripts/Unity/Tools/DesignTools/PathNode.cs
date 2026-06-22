using Primitives.Physics;
using UnityEngine;

namespace Tools.DesignTools
{
    public class PathNode : DesignToolsBase
    {
        public ActionType Action;
        public Vector2 Position => transform.position;
        public PathNode PrevNode { get; private set; }
        public PathNode NextNode { get; private set; }
        public KinematicResult KinematicState;
        public void InitNode(PathNode prevNode = null)
        {
            PrevNode = prevNode;
            SetNextNode();
        }

        public void SetNextNode()
        {
            if (PrevNode != null)
            {
                PrevNode.SetNextNode();
            }
        }
    }
}
