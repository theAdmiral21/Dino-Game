namespace Primitives.Characters
{
    public struct CharacterData //: ICharacterData
    {
        public string CharacterName { get; private set; }

        public CharacterID CharacterID { get; private set; }

        public CharacterData(string name, CharacterID id)
        {
            CharacterName = name;
            CharacterID = id;
        }
    }
}