// using Game.Core.Execution;
// using Infrastructure.Unity.DataStructures;
// using Infrastructure.Unity.Registries;
// using UnityEngine;

// namespace Infrastructure.Unity.Players
// {
//     public class InitializeObject : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
//     {
//         [SerializeField] private int _priority = 0;
//         public int Priority => _priority;
//         private IGameContext _context;

//         public void Initialize(IGameContext context)
//         {
//             _context = context;
//         }

//         public void PostInitialize(IGameContext context) { }

//         public SpawnData InitializePlayer(ref SpawnData spawnData)
//         {
//             spawnData.PlayerObject = InitializeNewObject(spawnData.PlayerObject);
//             spawnData.CameraObject = InitializeNewObject(spawnData.CameraObject);
//             return spawnData;
//         }

//         // public GameObject InitializeObject(GameObject gObject)
//         // {

//         // }

//         private GameObject InitializeNewObject(GameObject gObject)
//         {
//             var initObjects = gObject.GetComponentsInChildren<IInitializable<IGameContext>>();

//             foreach (var obj in initObjects)
//             {
//                 obj.Initialize(_context);

//                 obj.PostInitialize(_context);
//             }
//             return gObject;
//         }
//     }
// }