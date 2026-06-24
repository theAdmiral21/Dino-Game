using System.Collections.Generic;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "SequenceSO", menuName = "AI/Enemy/Composites/Sequence SO")]
    public class SequenceSO : BehaviorNodeSO
    {
        [SerializeField] public List<BehaviorNodeSO> Children;
    }
}