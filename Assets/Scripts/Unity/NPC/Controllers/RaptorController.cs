using System.Linq;
using AI.Application.BehaviorTree;
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using Core.Movement.Inputs;
using NPC.Application.BehaviorContexts;
using Unity.AI.BehaviorTree;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.NPC.Controllers
{
    public class RaptorController : MonoBehaviour
    {
        [SerializeField] private Transform _parentTransform;

        [SerializeField] private BehaviorNodeSO _rootSO;

        [SerializeField] private SerializedInterface<IAiInput> _aiInputMono;
        private IAiInput _aiInput => _aiInputMono.Interface;

        // [SerializeField] private SerializedInterface<IPlayerDetector> _detectorMono;
        // private IPlayerDetector _detector => _detectorMono.Interface;

        // [SerializeField] private SerializedInterface<IPathAwayFrom> _pathAwayMono;
        // private IPathAwayFrom _pathAway => _pathAwayMono.Interface;

        private IBehaviorTree<RaptorContext> _behaviorTree;
        private RaptorContext _context;
        private void Awake()
        {
            // build the context
            _context = new RaptorContext(_aiInput);
            // _context = new RaptorContext(_aiInput, _detector, _pathAway);

            // Build the nodes
            IBehaviorNode<RaptorContext> ballRoot = BuildNode(_rootSO);

            // build the tree
            _behaviorTree = new BehaviorTree<RaptorContext>(ballRoot);
        }

        private void Update()
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
    }
}