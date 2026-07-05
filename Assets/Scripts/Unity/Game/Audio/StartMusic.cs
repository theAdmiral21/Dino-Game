using UnityEngine;
using Infrastructure.Unity.Registries;
using Game.Core.Execution;
using Game.Core.Audio;
using Gameplay.Common.Unity;
using Game.Application.Audio.DataStructures;
using Physics.Core.PhysicsActors;

namespace Game.Unity.Audio
{
    public class StartMusic : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        [SerializeField] private TriggerVolume _trigger;
        private IAudioService _audioService;
        [SerializeField] private int _priority = 0;
        public int Priority => _priority;

        private new void Awake()
        {
            base.Awake();
            _trigger.OnVolumeEntered += PlayMusic;
        }
        private new void OnDestroy()
        {
            base.OnDestroy();
            _trigger.OnVolumeEntered -= PlayMusic;
        }

        public void Initialize(IGameContext context)
        {
            _audioService = context.AudioService;
        }
        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_audioService != null, $"Failed to get audio service for {gameObject.name}");
        }

        private void PlayMusic(IPhysicsActor actor)
        {
            _audioService.PlayMusic(new SongSoundRequest(SongSoundKey.Factory));
        }
    }
}