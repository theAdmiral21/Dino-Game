namespace Game.Core.Events
{
    /// <summary>
    /// Generic interface for playing sounds or animations based off of an event or action.
    /// </summary>
    public interface IEventFeedBack
    {
        public void React();
    }
}