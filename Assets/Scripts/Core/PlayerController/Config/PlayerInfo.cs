using System;
using Primitives.Characters;
using Primitives.Players;
using Primitives.SaveData;

namespace PlayerController.Core.Config
{
    [System.Serializable]
    public class PlayerInfo : IPlayerInfo
    {
        public Guid PlayerId { get; private set; }
        public CharacterID CharacterId { get; private set; }
        public PlayerSaveData SaveData { get; private set; }

        public PlayerInfo(Guid playerId, CharacterID characterID)
        {
            PlayerId = playerId;
            CharacterId = characterID;
        }

        public override bool Equals(object obj)
        {
            if (obj is IPlayerInfo other)
                return PlayerId == other.PlayerId;
            return false;
        }

        public override int GetHashCode()
        {
            return PlayerId.GetHashCode();
        }
    }
}