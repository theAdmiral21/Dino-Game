using System;
using System.Collections.Generic;
using Movement.Core.Stats;

namespace Movement.Application.Stats
{
    public class StatCollection : IStatCollection
    {
        private Dictionary<Type, object> _stats;

        public StatCollection(Dictionary<Type, object> stats)
        {
            _stats = stats;
        }

        public T Get<T>() => (T)_stats[typeof(T)];

        public bool TryGet<T>(out T stat)
        {
            if (_stats.TryGetValue(typeof(T), out var value))
            {
                stat = (T)value;
                return true;
            }
            stat = default;
            return false;
        }
    }
}