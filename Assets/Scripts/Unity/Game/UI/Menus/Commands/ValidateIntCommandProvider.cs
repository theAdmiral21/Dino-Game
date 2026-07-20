
using Core.Environment.Abstractions;
using Game.Application.UI.Menus.UICommands;
using Primitives.Common.Scenes;
using Primitives.Menus.Commands;
using Unity.Common.Unity;
using UnityEngine;

namespace Game.UI.Menus.Unity.Commands
{
    public class ValidateIntCommandProvider : MonoBehaviour, ICommandProvider
    {
        [SerializeField] private SerializedInterface<IPinValidator> _keypadMono;
        private IPinValidator _keypad => _keypadMono.Interface;
        public IUICommand CreateCommand()
        {
            _keypad.IsValid();
            return null;
        }
    }
}