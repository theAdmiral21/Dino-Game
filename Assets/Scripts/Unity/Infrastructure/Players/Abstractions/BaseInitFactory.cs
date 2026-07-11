using System.Linq;
using Game.Core.Execution;
using Infrastructure.Unity.DataStructures;
using Infrastructure.Unity.Registries;
using Unity.Game.GameLoop;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public abstract class BaseInitFactory : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        [SerializeField] private int _priority = 0;
        public int Priority => _priority;
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
            var initObjects = gObject.GetComponentsInChildren<IInitializable<IGameContext>>().ToList();

            InitFactory.InitializeObject(_context, initObjects);

            // Order initialization order by priority.
            // List<IInitializable<IGameContext>> _systemList = initObjects.OrderBy(s => s.Priority).ToList();
            // foreach (var obj in _systemList)
            // {
            //     Debug.Log($"Initializing: {obj}");
            //     obj.Initialize(_context);

            //     Debug.Log($"Post initializing: {obj}");
            //     obj.PostInitialize(_context);
            // }
            return gObject;
        }
    }
}