using System.Collections.Generic;
using AI.Application.BehaviorTreeNodes;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace AI.Unity.BehaviorTree
{
    [CreateAssetMenu(fileName = "SelectorSO", menuName = "AI/Enemy/Composites/Selector SO")]
    public class SelectorSO : BehaviorNodeSO
    {
        [SerializeField] public List<BehaviorNodeSO> Children;
    }
}