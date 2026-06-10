namespace Physics.Core.PhysicsActors
{
    public interface IActorEventBusProvider
    {
        public IActorEventBus ActorEventBus { get; }
    }
}