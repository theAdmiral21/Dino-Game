using UnityEngine;
using Infrastructure.Core.Inputs;
using Primitives.Characters;

namespace Game.Core.Interactions
{
    public interface IInteractContext
    {
        public Vector2 PlayerLocation { get; }
        public CharacterID PlayerCharacter { get; }
        public IPlayerActionMapManager PlayerActionMapManager { get; }
        public IConversationInputReader ConversationInputReader { get; }
    }
}