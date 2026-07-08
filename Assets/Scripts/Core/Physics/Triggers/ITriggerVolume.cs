using Physics.Core.Abstractions;
using UnityEngine;

namespace Core.Physics.Triggers
{
    public interface ITriggerVolume
    {
        EntityId Id { get; }
        IBoundsProvider BoundsProvider { get; }
    }
}