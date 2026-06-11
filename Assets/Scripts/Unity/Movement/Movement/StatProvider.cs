
using Movement.Core.Abstractions;
using Primitives.Stats;
using UnityEngine;

namespace Movement.Unity.Abstractions
{
    public class StatProvider : MonoBehaviour, IStatProvider
    {
        [SerializeField] private ScriptableObject _statSO;
        public PlayerStats Stats
        {
            get
            {
                if (!_statsSet)
                {
                    SetStats(_statSO);
                }
                return _stats;
            }
        }

        public IStatSheet StatSheet => throw new System.NotImplementedException();

        private PlayerStats _stats;
        private IMovementStats _statInterface;
        private bool _statsSet = false;


        public void SetStats(ScriptableObject statSO)
        {
            _statInterface = _statSO as IMovementStats;
            if (_statInterface == null)
            {
                Debug.LogError($"Unable to convert {_statSO.name} to IMovementStats.");
                return;
            }
            // _stats = new PlayerStats(_statInterface);
            _statsSet = true;
        }
    }
}