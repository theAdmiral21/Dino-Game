using UnityEngine;

namespace Game.Unity.Scenes.DataStructures
{
    [CreateAssetMenu(fileName = "Scene-Name", menuName = "Game/Scenes/SceneContext")]
    public class SceneContext : BaseSceneContext
    {

        // public string LevelName => _levelName;
        // [SerializeField] private string _levelName;
        // public SceneId Id => _id;
        // [SerializeField] private SceneId _id;
        // public ISceneTag Tag => _sceneTag;
        // [SerializeField] private SceneTag _sceneTag;
        // public GameState StartState => _sceneState;
        // [SerializeField] private GameState _sceneState;
        // // public ISceneTag NextScene => _nextSceneTag;
        // // [SerializeField] private SceneTag _nextSceneTag;
        // public SceneType LevelType => _levelType;
        // [SerializeField] private SceneType _levelType;

        // [Header("Audio Setup")]
        // public LevelTrackSet SongList;
        // public LevelTrackSet AmbientTracks;

        // [Header("Dog Toy")]
        // public Sprite DogToy;
        // public Sprite DogToySilhouette;

        // [Header("CutScenes")]
        // public Dictionary<CinematicId, PlayableDirector> Cinematics = new();

        public void Awake()
        {
            Debug.Assert(Tag != null, $"{this} does not have an assigned SceneTag.");
        }
    }
}