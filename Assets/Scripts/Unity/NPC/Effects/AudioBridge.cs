using Application.Game.Audio.DataStructures;
using Game.Core.Audio;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Primitives.Audio;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace NPC.Unity.Effects
{
    public class AudioBridge : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        protected IAudioService _audioService;

        [SerializeField] private int _priority = 0;
        public int Priority => _priority;

        public void Initialize(IGameContext context)
        {
            _audioService = context.AudioService;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_audioService != null, $"Unable to assign audio service");
        }

        public void PlaySound(EntityKey entity, ActionSoundKey actionKey)
        {
            _audioService.PlaySFX(new SoundRequest(entity, actionKey));
        }

        public void PlaySound(EntityKey entity, ActionSoundKey actionKey, SurfaceType surface)
        {
            _audioService.PlaySFX
            (
                new SoundRequest(entity, actionKey, surface)
            );
        }
        public void PlaySound(EntityKey entity, ActionSoundKey actionKey, SurfaceType surface, AudioBehavior behavior)
        {
            _audioService.PlaySFX
            (
                new SoundRequest(entity, actionKey, surface, behavior)
            );
        }

        public void PlaySound(EntityKey entity,
                                ActionSoundKey actionKey,
                                SurfaceType surface,
                                AudioBehavior behavior,
                                Vector2 volumeRange
                                )
        {
            _audioService.PlaySFX
            (
                new SoundRequest(entity, actionKey, surface, behavior, volumeRange)
            );
        }

        public void PlaySound(EntityKey entity,
                        ActionSoundKey actionKey,
                        SurfaceType surface,
                        AudioBehavior behavior,
                        Vector2 volumeRange,
                        Vector2 pitchRange
                        )
        {
            _audioService.PlaySFX
            (
                new SoundRequest(entity, actionKey, surface, behavior, volumeRange, pitchRange)
            );
        }
    }
}