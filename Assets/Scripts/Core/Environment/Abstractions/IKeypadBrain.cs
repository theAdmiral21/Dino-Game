using System.Collections.Generic;
using Core.Environment.Abstractions;

namespace Core.Environment.Abstractions
{
    public interface IKeypadBrain : IPinValidator, IKeyEnterable
    {
        public List<int> Pin { get; }
        public List<int> CurrentEntry { get; }
        public void ResetPin();
        public void ClearEntry();
    }
}