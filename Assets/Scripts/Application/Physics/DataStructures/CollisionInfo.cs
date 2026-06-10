using UnityEngine;

namespace Physics.Application.DataStructures
{
    public struct CollisionInfo
    {
        public bool Left;
        public bool Right;
        public bool Above;
        public bool Below;

        public void Reset()
        {
            Left = false;
            Right = false;
            Above = false;
            Below = false;
        }

        public void PrintInfo()
        {
            Debug.Log($"Left: {Left}; Right: {Right}; Above: {Above}; Below: {Below}");
        }

        public void SetLeft(bool val) => Left = val;
        public void SetRight(bool val) => Right = val;
        public void SetAbove(bool val) => Above = val;
        public void SetBelow(bool val) => Below = val;

    }
}