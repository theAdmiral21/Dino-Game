using System.Linq;
using AI.Application.BehaviorTree;
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using Core.Ai.BlackBoard;
using Core.Detection;
using Core.Game.HealthSystem.Health;
using Core.Movement.Inputs;
using NPC.Application.BehaviorContexts;
using Unity.AI.BehaviorTree;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.NPC.Controllers
{
    public class RaptorController : MonoBehaviour, IRaptorController
    {
        [SerializeField] private Transform _parentTransform;

        [SerializeField] private BehaviorNodeSO _rootSO;

        [SerializeField] private SerializedInterface<IAiInput> _aiInputMono;
        private IAiInput _aiInput => _aiInputMono.Interface;

        [SerializeField] private SerializedInterface<IDetectorOrchestrator> _detectorOrchestratorMono;
        private IDetectorOrchestrator _detectorOrchestrator => _detectorOrchestratorMono.Interface;

        // [SerializeField] private SerializedInterface<IHealthComponentProvider> _healthComponentMono;
        // private IHealthComponentProvider _healthComponent => _healthComponentMono.Interface;



        private IBehaviorTree<RaptorContext> _behaviorTree;

        public RaptorContext Context => _context;
        private RaptorContext _context;

        [Header("Debug")]
        [SerializeField] private string _currentNode;

        private void Awake()
        {

            // build the context
            _context = new RaptorContext(_aiInput);

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
            var node = _behaviorTree.Root as SelectorNode<RaptorContext>;
            string[] temp = node.CurrentNode.Split("`");
            string nodeName = temp[0].Split(".")[^1];
            _currentNode = nodeName;
        }
    }
}