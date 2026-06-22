using System.Collections.Generic;
using System.Linq;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using UnityEngine;

namespace Tools.DesignTools
{
    public class DrawPath : DesignToolsBase
    {
        /// <summary>
        /// Boolean for drawing the path or not.
        /// </summary>
        public bool Draw = false;
        /// <summary>
        /// Boolean for varying the start position of jump
        /// </summary>
        public bool AddVariation = false;

        [SerializeField] private List<PathNode> _path = new();
        public GameObject PathNodePrefab;


        [Header("Sim Settings")]
        [SerializeField] private int _simSteps = 20;
        private float _timeStep => Time.fixedDeltaTime;

        private KinematicResult _currentState;

        private ICalcAction _calcJump;
        private ICalcAction _calcFall;
        private ICalcAction _calcRun;
        private ICalcAction _calcRunStop;
        private ICalcAction _calcLanding;
        private ICalcAction _calcJumpCancel;
        private ICalcAction _calcQuickStep;
        private ICalcAction _calcExternalImpulse;
        private ICalcAction _calcExternalContinuous;
        private ICalcAction _calcKnockBack;
        private ICalcAction _calcTeleport;
        private ICalcAction _calcFly;
        private ICalcAction _calcQuickStepStop;
        private ICalcAction _calcQuickStepUpdate;

        private void OnValidate()
        {

            //Setup calculations
            // _calcJump = new CalcJump(_stats);
            // _calcFall = new CalcFall(_stats);
            // _calcRun = new CalcRun(_stats);
            // _calcRunStop = new CalcRunStop(_stats);
            // _calcLanding = new CalcLanding(_stats);
            // _calcJumpCancel = new CalcJumpCancel(_stats);
            // _calcQuickStep = new CalcQuickStep(_stats);
            // _calcExternalImpulse = new CalcExternalImpulse(_stats);
            // _calcExternalContinuous = new CalcExternalContinuous(_stats);
            // _calcKnockBack = new CalcKnockBack(_stats);
            // _calcTeleport = new CalcTeleport(_stats);
            // _calcFly = new CalcFly(_stats);
            // _calcQuickStepStop = new CalcQuickStepStop(_stats);
            // _calcQuickStepUpdate = new CalcQuickStepUpdate(_stats);

            Debug.Log($"Enabling calc run: {_calcRun}");
        }

        private void CollectNodes()
        {
            _path.Clear();
            PathNode[] nodes = GetComponentsInChildren<PathNode>();

            _path = nodes.ToList();
        }

        /// <summary>
        /// Method for adding a new position node
        /// </summary>
        [ContextMenu("Add Node")]
        private void AddNode()
        {
            Debug.Log("Added node");
            // Add a new node as a child of this object
            var nodeObject = Instantiate(PathNodePrefab, transform);
            // Save a reference to the object's PathNode
            var node = nodeObject.GetComponent<PathNode>();
            // Init the node
            if (_path.Count == 0)
            {
                node.InitNode();
            }
            else
            {
                node.InitNode(_path[^1]);
            }
            // Store the reference in the node list
            _path.Add(node);

            Debug.Log($"Total nodes: {_path.Count}");
        }


        private void OnDrawGizmos()
        {
            // Collect all existing nodes
            CollectNodes();
            _currentState = new KinematicResult();
            // Get the node
            for (int j = 0; j < _path.Count - 1; j++)
            {
                PathNode node = _path[j];

                for (int i = 0; i < _simSteps; i++)
                {
                    // here is where things get difficult, I want to calculate the kinematic state at each node based on the action of the previous node.

                    Debug.Log($"node action: {node.Action}");
                    switch (node.Action)
                    {
                        case ActionType.Run:
                            {
                                // var run = new RunResult(true, Vector2.right, RunType.Run, ActionPhase.Continuous);
                                Debug.Log($"node state: {node.KinematicState}");
                                Debug.Log($"next node: {_path[j + 1]}");
                                Debug.Log($"calc run: {_calcRun}");
                                // _path[j + 1].KinematicState = _calcRun.Calculate(run, ref node.KinematicState);
                                Debug.Log($"state: {_path[j + 1].KinematicState.Velocity}");
                                break;
                            }
                    }
                }
            }
        }


    }
}
