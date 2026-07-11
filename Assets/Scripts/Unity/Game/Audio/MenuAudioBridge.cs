// using UnityEngine;
// using Infrastructure.Unity.Registries;
// using Game.Core.Execution;
// using Game.Core.Audio;
// using Primitives.Audio.SoundKeys;
// namespace Game.Unity.Audio
// {
//     //NOTE Make this into a base class with shared update, start, stop, stop all, and PlaySFX methods
//     public class MenuAudioBridge : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>, IMenuAudio
//     {
//         private IAudioService _audioService;
//         [SerializeField] private int _priority = 0;
//         public int Priority => _priority;        // Instead of playing sounds directly, use the audio services
//         // private void Awake()
//         // {
//         //     // Register with the scene boot strapper in order initialize in the correct order
//         //     SceneInitializationRegistry.Register(this);
//         // }
//         public void Initialize(IGameContext context)
//         {
//             Debug.Log($"Audio services is not null: {context.AudioService != null}");
//             _audioService = context.AudioService;
//         }
//         public void PostInitialize(IGameContext context) { }
//         public void PlayBackSound() => _audioService.PlaySFX(new MenuSoundRequest(MenuSoundKey.Back));
//         public void PlayCancelSound() => _audioService.PlaySFX(new MenuSoundRequest(MenuSoundKey.Cancel));

//         public void PlaySelectSound() => _audioService.PlaySFX(new MenuSoundRequest(MenuSoundKey.Select));


//         public void PlaySubmitSound() => _audioService.PlaySFX(new MenuSoundRequest(MenuSoundKey.Submit));

//         public void PlayStartSound() => _audioService.PlaySFX(new MenuSoundRequest(MenuSoundKey.Start));
//     }
// }