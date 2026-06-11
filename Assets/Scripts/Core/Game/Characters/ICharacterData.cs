using Primitives.Characters;

namespace Game.Core.Characters.Abstractions
{
    public interface ICharacterData
    {
        public string CharacterName { get; }
        public CharacterID CharacterID { get; }

    }
}