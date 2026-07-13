using Core.Equipment;
using UnityEngine;

namespace Application.Inventory
{
    public class Magazine : IMagazine
    {

        public int RoundCount => _rounds;

        public int Capacity { get; private set; }

        private int _rounds;
        public Magazine(int capacity)
        {
            Capacity = capacity;
            // Start with a full mag
            _rounds = Capacity;
        }

        public void ReplenishRounds(int bulletCount)
        {
            Debug.Log($"Replenished {bulletCount} rounds");
            IncrementQuantity(bulletCount);
        }

        // Bullets can only be consumed one at a time
        public bool ConsumeRound()
        {
            if (_rounds > 0)
            {
                // Non empty magazine
                DecrementQuantity(1);
                return true;
            }
            // Empty magazine
            return false;
        }

        public void SetRounds(int amount)
        {
            _rounds = amount;
        }

        public void DecrementQuantity(int amount)
        {
            _rounds -= amount;
            if (_rounds < 0)
            {
                _rounds = 0;
                return;
            }
        }

        private void IncrementQuantity(int amount)
        {
            _rounds += amount;
            if (_rounds > Capacity)
            {
                _rounds = Capacity;
                return;
            }
        }
    }
}