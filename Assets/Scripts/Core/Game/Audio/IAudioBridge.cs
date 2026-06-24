using Game.Core.Effects;

namespace Game.Core.Audio
{
    public interface IAudioBridge
    {
        public void HandleSound(IEffectResult audioEffect);
    }
}