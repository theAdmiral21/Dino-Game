using Game.Core.Execution;
using Infrastructure.Unity.DataStructures;
using Infrastructure.Unity.Registries;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public abstract class BaseInitFactory : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        public int Priority => 0;
        private IGameContext _context;

        public void Initialize(IGameContext context)
        {
            _context = context;
        }

        public abstract SpawnData InstantiateObject(ref SpawnData data);

        public void PostInitialize(IGameContext context) { }

        protected GameObject InitializeNewObject(GameObject gObject)
        {
            Debug.Log($"Context is: {_context} - Frame: {Time.frameCount}");
            var initObjects = gObject.GetComponentsInChildren<IInitializable<IGameContext>>();

            foreach (var obj in initObjects)
            {
                Debug.Log($"Initializing: {obj}");
                obj.Initialize(_context);

                Debug.Log($"Post initializing: {obj}");
                obj.PostInitialize(_context);
            }
            return gObject;
        }
    }
}