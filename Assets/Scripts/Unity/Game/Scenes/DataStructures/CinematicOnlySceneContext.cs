// using System.Collections.Generic;
// using Game.Application.Scenes.Abstractions;
// using Game.Core.Cinematics.Enums;
// using Game.Core.Scenes;
// using UnityEngine;

// namespace Game.Unity.Scenes.DataStructures
// {
//     [CreateAssetMenu(fileName = "NameSceneContext", menuName = "Game/Scenes/CinematicOnlySceneContext")]
//     public class CinematicOnlySceneContext : BaseSceneContext, ICinematicOnlySceneContext
//     {
//         public ISceneTag NextScene => _nextSceneTag;
//         [SerializeField] private SceneTag _nextSceneTag;

//         public List<CinematicId> Cinematics => _id;
//         [SerializeField] private List<CinematicId> _id = new();


//     }
// }