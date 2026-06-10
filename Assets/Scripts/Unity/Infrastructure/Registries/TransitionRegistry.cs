using System.Collections.Generic;
using Game.Core.UI.Menus.Transitions;
using Infrastructure.Unity.Registries;
using UnityEngine;

namespace Infrastructure.Unity
{
    public class TransitionRegistry : MonoBehaviour, IScreenTransitionRegistry
    {
        [Header("Debug")]
        [SerializeField]
        public IReadOnlyCollection<IScreenTransition> Transitions => _transitions.Entities;
        private Registry<IScreenTransition> _transitions = new();

        private void Awake()
        {
            RegistryGateway.SetRegistry(_transitions);
        }
    }
}
