using UnityEngine;

namespace Tools.DesignTools
{
    public abstract class DesignToolsBase : MonoBehaviour
    {
        protected void Awake()
        {
            // Design tools should only exist in the editor space.
#if !UNITY_EDITOR
Destroy();
#endif
        }
    }
}