using Game.Application.Audio.DataStructures;
using Game.Core.Audio;
using Game.Core.Events;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Unity.Events
{
    /// <summary>
    /// Simple component that plays a sound. Is meant to be used with other components that require a sound is played when an action happens. eg BounceEntity
    /// </summary>
    public class AudioFeedBack : SelfRegister<IInitializable<IGameContext>>, IEventFeedBack, IInitializable<IGameContext>
    {
        [SerializeField] private LevelObjectEntityKey _entityKey;
        [SerializeField] private ActionSoundKey _actionKey;
        [SerializeField] private bool _allowPolyphony = false;
        private IAudioService _audioService;
        private bool _isPlaying = false;
        public int Priority => 0;

        private void Awake()
        {
            base.Awake();
            // Add a check here to make sure the given entity can perform the given action
        }
        public void React()
        {
            if (_allowPolyphony)
            {
                // Debug.Log($"Playing with polyphony.");
                _audioService.PlaySFX(new LevelObjectSoundRequest(_entityKey, _actionKey));
            }
            else
            {
                if (!_isPlaying)
                {
                    _isPlaying = true;
                    // Debug.Log($"playing without polyphony");
                    _audioService.PlaySFX(new LevelObjectSoundRequest(_entityKey, _actionKey));
                    _isPlaying = false;
                }
            }
        }

        public void Initialize(IGameContext context)
        {
            _audioService = context.AudioService;
            // Debug.Log($"audio service is null in init: {_audioService == null}");
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_audioService != null, "Audio service is null!");
            // Debug.Log($"audio service is null in post init: {_audioService == null}");
        }
    }
}