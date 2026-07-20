
using Core.Environment.Abstractions;
using Primitives.Menus.Commands;
using Unity.Common.Unity;
using UnityEngine;

namespace Game.UI.Menus.Unity.Commands
{
    public class InputIntCommandProvider : MonoBehaviour, ICommandProvider
    {
        public int IntValue;
        [SerializeField] private SerializedInterface<IKeyEnterable> _keypadMono;
        private IKeyEnterable _keypad => _keypadMono.Interface;
        public IUICommand CreateCommand()
        {
            _keypad.EnterValue(IntValue);
            return null;
        }
    }
}