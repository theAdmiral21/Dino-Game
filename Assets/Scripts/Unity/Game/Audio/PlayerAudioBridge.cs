// using UnityEngine;
// using Infrastructure.Unity.Registries;
// using Game.Core.Execution;
// using Primitives.Audio;
// using Game.Core.Audio;
// using Primitives.Audio.SoundKeys;

// namespace Game.Unity.Audio
// {
//     public class PlayerAudioBridge : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>, IPlayerAudioPlayer
//     {
//         private IAudioService _audioService;

//         private AudioSource _wallSlideSource;
//         private AudioSource _scentSource;
//         private AudioSource _twinkleSource;

//         [SerializeField] private int _priority = 0;
//         public int Priority => _priority;
//         public void Initialize(IGameContext context)
//         {
//             _audioService = context.AudioService;
//         }

//         public void PostInitialize(IGameContext context)
//         {
//             Debug.Assert(_audioService != null, $"Audio service is null");
//         }

//         public void PlayWalk(SurfaceType surface, float volume = 1f, bool loop = false) => _audioService.PlaySFX(new SurfaceSoundRequest(ActionSoundKey.Walk, surface));

//         public void PlayRun(SurfaceType surface, float volume = 1f, bool loop = false) => _audioService.PlaySFX(new SurfaceSoundRequest(ActionSoundKey.Run, surface));


//         public void PlayJump(SurfaceType surface, float volume = 1f, bool loop = false) => _audioService.PlaySFX(new SurfaceSoundRequest(ActionSoundKey.Jump, surface));


//         public void PlayLanding(SurfaceType surface, float volume = 1f, bool loop = false)
//         {
//             _audioService.PlaySFX(new SurfaceSoundRequest(ActionSoundKey.Land, surface));
//         }

//         public void PlayCrouch(float volume = 1, bool loop = false)
//         {
//             Debug.LogError($"Don't forget to add a crouch sound!");
//         }

//         public void PlayDeath(float volume = 1, bool loop = false)
//         {
//             // _audioService.PlaySFX(new Death)
//         }

//         public void PlayHurt(float volume = 1, bool loop = false)
//         {
//             // throw new System.NotImplementedException();
//         }

//         public void PlayDodge(float volume = 1, bool loop = false)
//         {
//             // throw new System.NotImplementedException();
//         }
//     }
// }