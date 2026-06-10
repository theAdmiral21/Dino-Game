using UnityEngine;

namespace Infrastructure.Unity.Registries
{
    public abstract class SelfRegister<T> : MonoBehaviour
    {
        protected virtual void Awake()
        {
            RegistryGateway.Register((T)(object)this);
        }

        protected virtual void OnDestroy()
        {
            RegistryGateway.Deregister((T)(object)this);
        }
    }
}