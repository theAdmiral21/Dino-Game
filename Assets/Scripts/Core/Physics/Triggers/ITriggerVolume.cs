using Physics.Core.Abstractions;

namespace Core.Physics.Triggers
{
    public interface ITriggerVolume
    {
        int Id { get; }
        IBoundsProvider BoundsProvider { get; }
    }
}