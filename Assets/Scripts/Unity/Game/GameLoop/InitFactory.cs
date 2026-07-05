using System.Collections.Generic;
using System.Linq;
using Game.Core.Execution;
using Infrastructure.Unity;
using Primitives.Stats;
using UnityEngine;

namespace Unity.Game.GameLoop
{
    public static class InitFactory
    {
        /// <summary>
        /// Helper method for initializing a list of IInitializable<IGameContext> objects. This method can handle both sorted and unsorted lists.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="systems"></param>
        /// <returns></returns>
        public static List<IInitializable<IGameContext>> InitializeObject(IGameContext context, List<IInitializable<IGameContext>> systems)
        {
            // In case they objects aren't already sorted
            List<IInitializable<IGameContext>> _systemList = systems.OrderBy(s => s.Priority).ToList();

            return InitializeList(context, _systemList);
        }

        /// <summary>
        /// Helper method for intializing all of the underlying IInitializable<IGameContext> systems in a given GameObject.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="gObject"></param>
        /// <returns></returns>
        public static List<IInitializable<IGameContext>> InitializeObject(IGameContext context, GameObject gObject)
        {
            List<IInitializable<IGameContext>> inits = gObject.GetComponentsInChildren<IInitializable<IGameContext>>().ToList();

            return InitializeObject(context, inits);
        }

        private static List<IInitializable<IGameContext>> InitializeList(IGameContext context, List<IInitializable<IGameContext>> systems)
        {
            // Do I call this here or some where else? 
            foreach (var system in systems)
            {
                // if (_printDebug) Debug.Log($"Initializing: {system}");
                system.Initialize(context);
            }


            foreach (var system in systems)
            {
                // if (_printDebug) Debug.Log($"Initializing: {system}");
                system.PostInitialize(context);
            }

            return systems;
        }
    }
}