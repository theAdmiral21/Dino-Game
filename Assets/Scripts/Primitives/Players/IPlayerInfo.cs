/*
Okay I know how this looks. I've put an interface in primitives which is against the rules, but hear me out. Primitives is used for data types and structures. IPlayerInfo defines the shape of a data type. So while not being a pure data type it isn't a pure service contract either. Therefore, in order to avoid a big refactor (removing game from player controller), I have put this interface here. - Thomas
*/

using System;
using Primitives.Characters;
using Primitives.SaveData;

namespace Primitives.Players
{
    public interface IPlayerInfo
    {
        public Guid PlayerId { get; }
        public CharacterID CharacterId { get; }
        public PlayerSaveData SaveData { get; }
    }
}