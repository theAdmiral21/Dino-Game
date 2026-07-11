using System.Linq;
using AI.Application.BehaviorTree;
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using Core.Ai.Behavior.Visualization;
using Core.Ai.BlackBoard;
using Core.Detection;
using Core.Movement.Inputs;
using Game.Core.Execution;
using Game.Core.Health;
using Infrastructure.Unity.Registries;
using Movement.Core.Abstractions;
using NPC.Application.BehaviorContexts;
using Unity.AI.BehaviorTree;
using Unity.Common;
using Unity.Detection.Detectors.DataStructures;
using Unity.Infrastructure.Providers;
using Unity.Tools.DrawingTools;
using UnityEngine;

namespace Unity.NPC.Controllers
{
    public class RaptorController : SelfRegister<IInitializable<IGameContext>>, IRaptorController, IInitializable<IGameContext>
    {
        [SerializeField] private Transform _parentTransform;
        [Header("Behavior Tree Root Node")]
        [SerializeField] private BehaviorNodeSO _rootSO;
        [Header("Detector Stats")]
        [SerializeField] private DetectorStatsSO _statsSO;

        private IStatSheet _statSheet;
        private IPackDataProvider _packDataProvider;
        private IRaptorInput _raptorInput;
        private IDetectorOrchestrator _detectorOrchestrator;
        private IHealthComponent _healthComponent;

        private bool IsActive = true;

        public IInspectableNode RootNode => _behaviorTree.Root;
        private IBehaviorTree<RaptorContext> _behaviorTree;

        public RaptorContext Context => _context;

        [SerializeField] private int _priority;
        public int Priority => _priority;

        private RaptorContext _context;

        [Header("Debug")]
        [SerializeField] private string _currentNode;
        [SerializeField] private string _currentStatus;
        public void Initialize(IGameContext context)
        {
            // Go collect everything you need to build the component
            var dataProvider = ProviderLookUp.Require<RaptorDataProvider>(this);
            _statSheet = dataProvider.StatSheet;
            _packDataProvider = dataProvider.PackDataProvider;
            _raptorInput = dataProvider.RaptorInput;
            _detectorOrchestrator = dataProvider.DetectorOrchestrator;
            _healthComponent = dataProvider.HealthComponent;
            Debug.Log($"health component: {_healthComponent}");

            // build the context
            _context = new RaptorContext(_raptorInput,
                                         _packDataProvider,
                                         _statsSO.BuildRunTime(),
                                         _statSheet.StatCollection,
                                         this,
                                         _healthComponent);

            // Build the nodes
            IBehaviorNode<RaptorContext> root = BuildNode(_rootSO);

            // build the tree
            _behaviorTree = new BehaviorTree<RaptorContext>(root);

            // Init the detector brain
            _detectorOrchestrator.InitBrain(_context);
        }

        public void PostInitialize(IGameContext context)
        {

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

        public void TickBehaviorTree(float dt)
        {
            if (!IsActive) return;
            // Update your context
            _context.Dt = Time.deltaTime;
            _context.CurrentPosition = _parentTransform.position;

            // Update your tree
            _behaviorTree.Tick(_context);
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

            // Debug.Log($"[Run] input dir: {_context.AiInput.Move.x}");
        }

        public void StopBehaviorTree()
        {
            IsActive = false;
        }

        public void StartBehaviorTree()
        {
            IsActive = true;
        }


    }
}