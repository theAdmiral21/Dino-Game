using UnityEngine;
using Game.Application.Scenes.Abstractions;
using Game.Core.Execution;
using Game.Core.Scenes;
using Game.Core.Audio;

namespace Game.Application.Scenes
{
    public class MenuSceneController : BaseSceneController
    {
        private IMenuSceneContext _sceneContext;
        private IAudioService _audioService;

        public MenuSceneController(ISceneDefinition context, IGameContext gameContext) : base(context, gameContext)
        {
            _sceneContext = context as IMenuSceneContext;
            if (_sceneContext == null)
            {
                Debug.LogError($"Unable to convert {context} to IMenuSceneContext");
                return;
            }

            // Get the audio service to play music with
            _audioService = gameContext.AudioService;
        }

        public override void OnSceneLoaded()
        {
            Debug.Log($"Scene menu controller loaded");
            Debug.LogError($"HEY this doesn't actually play music anymore. FIX IT");
            // _audioService.PlayMusic(new SongSoundRequest(SongSoundKey.MainMenu, AudioBehavior.SingleShot));
        }

        public override void OnSceneUnloaded()
        {
            Debug.LogError($"Scene menu controller unloaded");
        }

        public override void Tick()
        {
            // throw new System.NotImplementedException();
        }


    }
}