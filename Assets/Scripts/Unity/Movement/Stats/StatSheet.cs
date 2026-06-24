using System;
using System.Collections.Generic;
using Movement.Application.Stats;
using Movement.Core.Abstractions;
using Movement.Core.Stats;
using Movement.Unity.Stats.StatSOs.Abstractions;
using UnityEngine;

namespace Movement.Unity.Stats
{
    public class StatSheet : MonoBehaviour, IStatSheet
    {
        [SerializeField] private List<StatSO> _stats;
        private bool _statsSet = false;
        public IStatCollection StatCollection
        {
            get
            {
                if (!_statsSet)
                {
                    BuildStatCollection();
                }
                return _statCollection;
            }
        }

        private IStatCollection _statCollection;

        // private void Awake()
        // {
        //     // _lookUp = new();

        //     StatCollection = BuildStatCollection();
        // }

        private void BuildStatCollection()
        {
            Dictionary<Type, object> statDict = new();
            foreach (var stat in _stats)
            {
                var runTimeStat = stat.BuildRunTime();
                statDict[runTimeStat.GetType()] = runTimeStat;
            }
            _statCollection = new StatCollection(statDict);
            _statsSet = true;
            // Debug.Log($"Stats set: {_statsSet}");
        }

        public T Get<T>()
        {
            return StatCollection.Get<T>();
        }

        public bool TryGet<T>(out T stat)
        {
            return StatCollection.TryGet(out stat);
        }
    }
}