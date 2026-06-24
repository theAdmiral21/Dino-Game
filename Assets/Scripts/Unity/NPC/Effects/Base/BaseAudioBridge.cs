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

        public int Priority => 0;

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