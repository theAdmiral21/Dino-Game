using Game.Core.Interactions;
using Infrastructure.Core.Inputs;
using PlayerController.Core.Inputs;
using Primitives.Characters;
using UnityEngine;

namespace PlayerController.Unity.Interactions
{
    public class InteractContext : IInteractContext
    {
        public CharacterID PlayerCharacter { get; private set; }
        public IPlayerActionMapManager PlayerActionMapManager { get; private set; }

        public IConversationInputReader ConversationInputReader { get; private set; }

        public Vector2 PlayerLocation { get; private set; }

        public InteractContext(CharacterID id,
                                IPlayerActionMapManager playerActionMapManager,
                                IConversationInputReader conversationInputReader,
                                Vector2 playerLocation)
        {
            PlayerCharacter = id;
            PlayerActionMapManager = playerActionMapManager;
            ConversationInputReader = conversationInputReader;
            PlayerLocation = playerLocation;
        }
    }
}