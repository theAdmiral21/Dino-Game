using UnityEngine;
using Infrastructure.Core.Inputs;
using Primitives.Characters;
using Game.Core.Inputs;

namespace Game.Core.Interactions
{
    public interface IInteractContext
    {
        public Vector2 PlayerLocation { get; }
        public CharacterID PlayerCharacter { get; }
        public IPlayerActionMapManager PlayerActionMapManager { get; }
        public IConversationInputReader ConversationInputReader { get; }
        public IUIInputProvider MenuInputReader { get; }
    }
}