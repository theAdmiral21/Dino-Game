using Game.Core.Audio;
using Game.Core.Effects;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using UnityEngine;

namespace NPC.Unity.Effects
{
    public abstract class BaseAudioBridge : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>, IAudioBridge
    {
        protected IAudioService _audioService;

        [SerializeField] private int _priority = 0;
        public int Priority => _priority;

        public abstract void HandleSound(IEffectResult audioEffect);

        public void Initialize(IGameContext context)
        {
            _audioService = context.AudioService;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_audioService != null, $"Unable to assign audio service");
        }
    }
}