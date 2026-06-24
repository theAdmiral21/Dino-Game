using Physics.Core.PhysicsActors;
using UnityEngine;

namespace Physics.Unity.PhysicsQueries
{
    public class ActorProvider : MonoBehaviour, IActorProvider
    {
        public IPhysicsActor Actor { get; private set; }

        private void Awake()
        {
            Actor = GetComponent<IPhysicsActor>();
        }
    }
}