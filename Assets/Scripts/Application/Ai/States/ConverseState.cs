using AI.Core.State;
using AI.Core.State.Abstractions;
using AI.Core.State.Enums;

namespace AI.Application.States
{
    /// <summary>
    /// A state used to control the flow of conversation
    /// </summary>
    public class ConverseState<T> : IState<T> where T : IConversationContext
    {
        public StateType State => StateType.Converse;

        public void Enter(T context)
        {
            // Signal that the npc is ready to talk
        }

        public void Update(T context)
        {

        }

        public void Exit(T context)
        {

        }


    }
}