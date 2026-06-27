# Pack Behavior
In the game I want the raptors to work together as a single unit sharing information and coordinating attacks. Not knowing how to do this at all Claude suggested that I use a black board to solve this. From what I've read a black board is a shared data source that multiple "knowledge sources" utilize to solve a problem defined within the black board. Data sharing and problem resolution is orchestrated by a single control component that coordinates the knowledge sources.

### Problem
So for my raptors the problem is: **"where is the player and how do we eat him/her?"**
 - Does this mean I have to design attack strategies?

### Knowledge Sources
The knowledge sources are the raptors. They generate data via their senses. The knowledge they can share is:

```c#
public class PackData
{
    public Observation<Vector2> LastKnownLocation;
    public Observation<Vector2> BestGuessLocation;
    public float BestGuessConfidence;
    public Observation<float> TargetFacing; // for creeping up behind the player
    public Observation<HealthState> TargetHealth;
    // It might be interesting to add a field for what the player is currently doing like reloading, healing, solving a puzzle, so the raptors know if the player is distracted. This is kind of what the facing field is for. If the player isn't looking, feel free to creep up.



    // How would I share sound and scent data across all of the members? Do I need a PackMember class that exposes a given animal's perception state? Some way to triangulate a position based on sounds and smells is definitely needed...


}

public class MemberStatus
{
    public HealthState Health;
    public Vector2 Position;
    public Status CurrentStatus;
    public AlertLevel Alertness;
}

public enum Status
{
    Searching, // trying to pick up the trail
    Tracking, // you are following a trail
    Stalking, // you have eyes on the target but you're keeping your distance
    Attacking, // you are actively engaging the target
    Retreating, // you are actively fleeing from an engagement
    Startled, // Something has happened, you don't know what, fight or flight time
    Flushing, // Charging the target with no intent to attack, trying to get them to run
    Flanking, // Moving to cut off escape routes
    // anything else?
}

public enum AlertLevel
{
    Unaware, // minding your own business
    Curious, // You heard or smelled something
    Interested, // You heard/smelled something player like
    Engaged, // You might have a fix on the player
    Engrossed, // You see the player, you are obsessed, a psychiatrist would say its unhealthy

}
```

Maybe I should add a Sighting struct to hold observations an when they were recorded so that you can relate how stale something is quickly?

```c#
public struct Observation<T>
{
    public float TimeOfObservation;
    public T Observation;

    public float Age => Time.time - TimeOfObservation;
    public bool IsStale(float maxAge) => Age > MaxAge;
}
```

Or the observation class might just make this really confusing because I'd have a bunch of the same classes..

### Coordinator
Don't forget, someone has to run this show:

```c#
public interface IPackCoordinator
{
    public PackData Data {get;}
    public HashSet<MemberStatus> PackStatus {get;}
    public void AddPackMember(MemberStatus member)
    public void Triangulate();
    public void UpdateBlackBoard();
    public void EvaluateBlackBoard();
}

public class PackCoordinator
{
    public void Triangulate()
    {
        // iterate over the members
        
        // use last scent and sound data to determine what direction relative to a given member something happened
        // ie: MemberA is following a trail to the left, MemberB heard something to the right, MemberA is to the right of MemberB, if the detections are recent, something is likely between them.
        // calculate the best guess
        // calculate the confidence in that guess
    }
}
```