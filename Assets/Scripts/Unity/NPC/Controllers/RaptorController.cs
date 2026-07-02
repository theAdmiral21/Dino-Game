using System.Linq;
using AI.Application.BehaviorTree;
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using Core.Ai.Behavior.Visualization;
using Core.Ai.BlackBoard;
using Core.Detection;
using Core.Game.HealthSystem.Health;
using Core.Movement.Inputs;
using Movement.Core.Abstractions;
using Movement.Unity.Stats;
using NPC.Application.BehaviorContexts;
using Unity.AI.BehaviorTree;
using Unity.Common.Unity;
using Unity.Detection.Detectors.DataStructures;
using Unity.Tools.DrawingTools;
using UnityEngine;

namespace Unity.NPC.Controllers
{
    public class RaptorController : MonoBehaviour, IRaptorController
    {
        [SerializeField] private Transform _parentTransform;

        [SerializeField] private BehaviorNodeSO _rootSO;
        [SerializeField] private DetectorStatsSO _statsSO;

        [SerializeField] private SerializedInterface<IStatSheet> _statSheetMono;
        private IStatSheet _statSheet => _statSheetMono.Interface;

        [SerializeField] private SerializedInterface<IRaptorInput> _raptorInputMono;
        private IRaptorInput _raptorInput => _raptorInputMono.Interface;

        [SerializeField] private SerializedInterface<IDetectorOrchestrator> _detectorOrchestratorMono;
        private IDetectorOrchestrator _detectorOrchestrator => _detectorOrchestratorMono.Interface;

        [SerializeField] private SerializedInterface<IPackDataProvider> _packDataProviderMono;
        private IPackDataProvider _packDataProvider => _packDataProviderMono.Interface;

        public IInspectableNode RootNode => _behaviorTree.Root;
        private IBehaviorTree<RaptorContext> _behaviorTree;

        public RaptorContext Context => _context;
        private RaptorContext _context;

        [Header("Debug")]
        [SerializeField] private string _currentNode;
        [SerializeField] private string _currentStatus;

        private void Awake()
        {

            // build the context
            _context = new RaptorContext(_raptorInput, _packDataProvider, _statsSO.BuildRunTime(), _statSheet.StatCollection);

            // Build the nodes
            IBehaviorNode<RaptorContext> root = BuildNode(_rootSO);

            // build the tree
            _behaviorTree = new BehaviorTree<RaptorContext>(root);

            // Init the detector brain
            _detectorOrchestrator.InitBrain(_context);
        }

        public void TickBehaviorTree(float dt)
        {
            // Update your context
            _context.Dt = Time.deltaTime;
            _context.CurrentPosition = _parentTransform.position;

            // Update your tree
            _behaviorTree.Tick(_context);
        }

        private IBehaviorNode<RaptorContext> BuildNode(BehaviorNodeSO node)
        {
            switch (node)
            {
                case SequenceSO sequence:
                    {
                        return new SequenceNode<RaptorContext>(
                            sequence.Children.Select(C => BuildNode(C)).ToList()
                            );
                    }
                case SelectorSO selector:
                    {
                        return new SelectorNode<RaptorContext>(
                            selector.Children.Select(C => BuildNode(C)).ToList()
                            );
                    }
                default:
                    {
                        return ((BehaviorNodeSO<RaptorContext>)node).BuildRunTime();
                    }
            }
        }

        private void LateUpdate()
        {
            try
            {
                var node = _behaviorTree.Root as SelectorNode<RaptorContext>;
                string[] temp = node.CurrentNode.Split("`");
                string nodeName = temp[0].Split(".")[^1];
                _currentNode = nodeName;
            }
            catch
            {
                _currentNode = "None";
            }

            _currentStatus = $"{Context.CurrentStatus}";

            // Draw where the raptor is trying to go
            DrawUtil.DrawDebugCircle(_context.Destination, 2, Color.yellow);

            Debug.Log($"[Run] input dir: {_context.AiInput.Move.x}");
        }
    }
}