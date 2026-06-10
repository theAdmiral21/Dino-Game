using Physics.Core.Abstractions;

namespace Gameplay.Common.Application.Abstractions
{
    public interface ITriggerVolume
    {
        int Id { get; }
        IBoundsProvider BoundsProvider { get; }
    }
}