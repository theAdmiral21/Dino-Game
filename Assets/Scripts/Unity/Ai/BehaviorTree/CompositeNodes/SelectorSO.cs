using System.Collections.Generic;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "SelectorSO", menuName = "AI/Enemy/Composites/Selector SO")]
    public class SelectorSO : BehaviorNodeSO
    {
        [SerializeField] public List<BehaviorNodeSO> Children;
    }
}