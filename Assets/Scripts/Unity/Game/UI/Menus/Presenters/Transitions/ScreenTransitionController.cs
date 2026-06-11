using Infrastructure.Unity.Registries;
using Unity.Common.Unity;
using UnityEngine;

namespace Game.Unity.UI.Menus.Presenters.Transitions
{
    public class ScreenTransitionController : MonoBehaviour
    {
        public IScreenTransitionRegistry TransitionRegistry => _transitionRegistry;
        [SerializeField] private SerializedInterface<IScreenTransitionRegistry> _registryMono;
        private IScreenTransitionRegistry _transitionRegistry;

        private void Awake()
        {
            _transitionRegistry = _registryMono.Interface;
        }
    }
}
