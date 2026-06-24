using System.Linq;
using AI.Application.BehaviorTree;
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using AI.Core.PathFinding;
using AI.Unity.BehaviorTree;
using Codice.Client.Common.TreeGrouper;
using Enemy.Core.Detectors.Abstractions;
using Movement.Core.Inputs;
using NPC.Application.BehaviorContexts;
using Unity.Common.Unity;
using UnityEngine;

namespace NPC.Unity.Controllers
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private Transform _parentTransform;

        [SerializeField] private BehaviorNodeSO _rootSO;

        [SerializeField] private SerializedInterface<IAiInput> _aiInputMono;
        private IAiInput _aiInput => _aiInputMono.Interface;

        [SerializeField] private SerializedInterface<IPlayerDetector> _detectorMono;
        private IPlayerDetector _detector => _detectorMono.Interface;

        [SerializeField] private SerializedInterface<IPathAwayFrom> _pathAwayMono;
        private IPathAwayFrom _pathAway => _pathAwayMono.Interface;

        private IBehaviorTree<BallContext> _behaviorTree;
        private BallContext _context;

        private void Awake()
        {
            // build the context
            _context = new BallContext(_aiInput, _detector, _pathAway);

            // Build the nodes
            IBehaviorNode<BallContext> ballRoot = BuildNode(_rootSO);

            // build the tree
            _behaviorTree = new BehaviorTree<BallContext>(ballRoot);
        }

        private void Update()
        {
            // Update your context
            _context.Dt = Time.deltaTime;
            _context.CurrentPosition = _parentTransform.position;

            // Update your tree
            _behaviorTree.Tick(_context);
        }

        private IBehaviorNode<BallContext> BuildNode(BehaviorNodeSO node)
        {
            switch (node)
            {
                case SequenceSO sequence:
                    {
                        return new SequenceNode<BallContext>(
                            sequence.Children.Select(C => BuildNode(C)).ToList()
                            );
                    }
                case SelectorSO selector:
                    {
                        return new SelectorNode<BallContext>(
                            selector.Children.Select(C => BuildNode(C)).ToList()
                            );
                    }
                default:
                    {
                        return ((BehaviorNodeSO<BallContext>)node).BuildRunTime();
                    }
            }
        }
    }
}