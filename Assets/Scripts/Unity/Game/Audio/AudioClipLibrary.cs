using Game.Application.Audio.DataStructures;
using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Game.Unity.Audio.DataStructures;
using Unity.Common.Unity;
using UnityEngine;

namespace Game.Unity.Audio
{
    public class AudioClipLibrary : MonoBehaviour
    {
        [SerializeField] private ScriptableObject _playerSoundSO;
        private ISoundSet<IPlayerAudioRequest> _playerSoundSet;

        [SerializeField] private ScriptableObject _enemySoundSO;
        private ISoundSet<IEnemyAudioRequest> _enemySoundLib;

        // [SerializeField] private ScriptableObject _spikeSoundSO;
        // private ISoundSet<IEnemyAudioRequest> _spikeSoundSet;

        [SerializeField] private ScriptableObject _menuSoundSO;
        private ISoundSet<IMenuAudioRequest> _menuSoundSet;

        [SerializeField] private FootstepLibrary _footStepLibrary;

        [SerializeField] private ScriptableObject _slideSoundSO;
        private ISoundSet<ISlideSurfaceAudioRequest> _slideSoundSet;

        [SerializeField] private ScriptableObject _itemSoundSO;
        private ISoundSet<IItemAudioRequest> _itemSoundSet;

        [SerializeField] private ScriptableObject _levelObjectSO;
        private ISoundSet<ILevelObjectAudioRequest> _levelObjectSoundSet;

        [SerializeField] private ScriptableObject _levelTrackSO;
        private ISoundSet<ISongAudioRequest> _levelTrackSoundSet;

        [SerializeField] private ScriptableObject _speakerSetSO;
        private ISoundSet<ISpeakerAudioRequest> _speakerSoundSet;

        private void Awake()
        {
            // Convert the scriptable objects
            _playerSoundSet = _playerSoundSO as ISoundSet<IPlayerAudioRequest>;
            if (_playerSoundSet == null)
            {
                Debug.LogError($"PlayerSoundSet could not be mapped from its scriptable object."); return;
            }

            // Convert the scriptable objects
            _enemySoundLib = _enemySoundSO as ISoundSet<IEnemyAudioRequest>;
            if (_enemySoundLib == null)
            {
                Debug.LogError($"PlayerSoundSet could not be mapped from its scriptable object."); return;
            }

            // Convert the scriptable objects
            _menuSoundSet = _menuSoundSO as ISoundSet<IMenuAudioRequest>;
            if (_menuSoundSet == null)
            {
                Debug.LogError($"MenuSoundSet could not be mapped from its scriptable object."); return;
            }

            // Convert the scriptable objects
            _slideSoundSet = _slideSoundSO as ISoundSet<ISlideSurfaceAudioRequest>;
            if (_slideSoundSet == null)
            {
                Debug.LogError($"SlideSoundSet could not be mapped from its scriptable object."); return;
            }

            // Convert the scriptable objects
            _itemSoundSet = _itemSoundSO as ISoundSet<IItemAudioRequest>;
            if (_itemSoundSet == null)
            {
                Debug.LogError($"ItemSoundSet could not be mapped from its scriptable object."); return;
            }

            // Convert the scriptable objects
            _levelObjectSoundSet = _levelObjectSO as ISoundSet<ILevelObjectAudioRequest>;
            if (_levelObjectSoundSet == null)
            {
                Debug.LogError($"LevelObjectSoundSet could not be mapped from its scriptable object."); return;
            }

            // Convert the scriptable objects
            _levelTrackSoundSet = _levelTrackSO as ISoundSet<ISongAudioRequest>;
            if (_levelTrackSoundSet == null)
            {
                Debug.LogError($"LevelTrackSoundSet could not be mapped from its scriptable object."); return;
            }

            // Convert the scriptable objects
            _speakerSoundSet = _speakerSetSO as ISoundSet<ISpeakerAudioRequest>;
            if (_speakerSoundSet == null)
            {
                Debug.LogError($"SpeakerSoundSet could not be mapped from its scriptable object."); return;
            }
        }

        public AudioClipSettings LookUpClip(IAudioRequest request)
        {
            // Sort by entity, then send the action
            // Debug.Log($"Got audio request: {request}");
            switch (request)
            {
                case MenuSoundRequest menuSound:
                    {
                        return _menuSoundSet.GetClip(menuSound);
                    }
                case PlayerSoundRequest playerSound:
                    {
                        return _playerSoundSet.GetClip(playerSound);
                    }
                case EnemySoundRequest enemySound:
                    {
                        return _enemySoundLib.GetClip(enemySound);
                    }
                case SurfaceSoundRequest surfaceSound:
                    {
                        return _footStepLibrary.GetClip(surfaceSound);
                    }
                case SlideSurfaceSoundRequest slideSound:
                    {
                        return _slideSoundSet.GetClip(slideSound);
                    }
                case ItemSoundRequest itemSound:
                    {
                        return _itemSoundSet.GetClip(itemSound);
                    }
                case LevelObjectSoundRequest levelObjectSound:
                    {
                        return _levelObjectSoundSet.GetClip(levelObjectSound);
                    }
                case SongSoundRequest songRequest:
                    {
                        return _levelTrackSoundSet.GetClip(songRequest);
                    }
                case SpeakerSoundRequest speakerRequest:
                    {
                        return _speakerSoundSet.GetClip(speakerRequest);
                    }
                default:
                    {
                        Debug.LogError($"{request} was unhandled");
                        return null;
                    }
            }
        }
    }
}