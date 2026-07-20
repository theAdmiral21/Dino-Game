using System;
using System.Collections.Generic;
using Core.Environment.Abstractions;

namespace Application.Environment
{
    public class KeypadBrain : IKeypadBrain
    {
        public List<int> Pin => _pin;
        private List<int> _pin = new();
        private int _currentIndex => CurrentEntry.Count - 1;
        public List<int> CurrentEntry { get; private set; } = new();
        public KeypadBrain(int length)
        {
            if (length == 0) throw new ArgumentException("Length can not be zero");
            GeneratePin(length);
        }

        public void ClearEntry()
        {
            CurrentEntry.Clear();
        }

        public bool EnterValue(int val)
        {
            if (CurrentEntry.Count < _pin.Count)
            {
                CurrentEntry.Add(val);
                return true;
            }
            return false;
        }

        public void GeneratePin(int length)
        {
            if (length == 0) throw new ArgumentException("Length can not be zero");

            _pin.Clear();
            ClearEntry();

            for (int i = 0; i < length; i++)
            {
                _pin.Add(UnityEngine.Random.Range(0, 9));
            }
        }

        public bool IsValid()
        {
            if (_pin.Count == CurrentEntry.Count)
            {
                for (int i = 0; i < _pin.Count; i++)
                {
                    if (_pin[i] != CurrentEntry[i]) return false;
                }
                return true;
            }
            return false;
        }

        public void ResetPin()
        {
            GeneratePin(_pin.Count);
        }

        public void RemoveLast()
        {
            if (CurrentEntry.Count > 0)
                CurrentEntry.Remove(CurrentEntry[^1]);
        }
    }
}