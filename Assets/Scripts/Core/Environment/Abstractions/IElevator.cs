using System.Collections.Generic;
using UnityEngine;

namespace Environment.Core.Abstractions
{
    public interface IElevator
    {
        public List<Vector2> Destinations { get; }
    }
}