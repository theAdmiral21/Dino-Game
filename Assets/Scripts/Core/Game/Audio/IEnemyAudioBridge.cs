namespace Game.Core.Audio
{
    public interface IEnemyAudioBridge
    {
        public void PlayAttackSound(float volume = 1f, bool loop = false);
        public void PlayDeathSound(float volume = 1f, bool loop = false);
        public void PlayWarCry(float volume = 1f, bool loop = false);
    }
}