
using Core.Environment.Abstractions;
using Game.Application.UI.Menus.UICommands;
using Primitives.Common.Scenes;
using Primitives.Menus.Commands;
using Unity.Common.Unity;
using UnityEngine;

namespace Game.UI.Menus.Unity.Commands
{
    public class RemoveLastCommandProvider : MonoBehaviour, ICommandProvider
    {
        [SerializeField] private SerializedInterface<IKeyEnterable> _keyEnterableMono;
        private IKeyEnterable _keyEnterable => _keyEnterableMono.Interface;
        public IUICommand CreateCommand()
        {
            _keyEnterable.RemoveLast();
            return null;
        }
    }
}