using Movement.Core.Abstractions;

namespace Game.Core.Effects
{
    public interface IEffectPlayer
    {
        public void Play(IEffectResult effectResult);
    }
}