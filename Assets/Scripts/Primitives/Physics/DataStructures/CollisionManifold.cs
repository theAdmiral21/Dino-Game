using System.Collections.Generic;
using UnityEngine;

namespace Primitives.Physics.DataStructures
{
    public struct CollisionManifold
    {
        public Vector2 Normal;
        public List<Contact> Contacts;
        public int ContactCount;
    }
}