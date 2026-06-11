namespace Game.Core.Characters.Abstractions
{
    public interface ICharacterDataProvider
    {
        public ICharacterData Data { get; }
        public void SetCharacterData(ICharacterData data);
    }
}