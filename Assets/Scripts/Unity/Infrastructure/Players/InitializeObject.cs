using Game.Core.Execution;
using Infrastructure.Unity.DataStructures;
using Infrastructure.Unity.Registries;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class InitializeObject : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        public int Priority => 0;
        private IGameContext _context;

        public void Initialize(IGameContext context)
        {
            _context = context;
        }

        public void PostInitialize(IGameContext context) { }

        public SpawnData InitializePlayer(ref SpawnData spawnData)
        {
            spawnData.PlayerObject = InitializeNewObject(spawnData.PlayerObject);
            spawnData.CameraObject = InitializeNewObject(spawnData.CameraObject);
            return spawnData;
        }

        private GameObject InitializeNewObject(GameObject gObject)
        {
            var initObjects = gObject.GetComponentsInChildren<IInitializable<IGameContext>>();

            foreach (var obj in initObjects)
            {
                obj.Initialize(_context);

                obj.PostInitialize(_context);
            }
            return gObject;
        }
    }
}