# Detector Brain

The detector brain is the orchestrator for the three detectors:
- Visual
- Audio
- Olfactory  

Each detector should return a struct with the data it perceived. For example if the the auditory sensor detects a sound event but does not *perceive* the event, nothing happens. Each of the structs returned by the detectors returns different types of data.  

**Visual detectors** return the last known location of the detected object and which way it is facing. From there the brain is able to determine if the object can see the brain, how far away it is (for pounce calculations) etc.  

**Audio detectors** act as long range alerts. They can indicate vaguely what happened or is currently happening AND in what direction it came from. Sequential successful audio detections will also return the direction of travel of the object. IE if the sounds are similar and increasing in volume, the brain can determine that the object is coming closer and vice versa.

**Olfactory detectors** are used to determine if an object has been at a certain location, what kind of object it is, possibly how wounded the object is?, and which direction the object went. So if the player were wounded and bleeding, when they moved they would create a scent trail. The trail would degrade over time until it became undetectable. However if a raptor came across the scent trail it could potentially follow the trail to the player. Also if the player were to heal, they would cease leaving a scent trail.  
  
With all of this in mind, what kind of data does each detector return?

```c#
public struct VisualData
{
    public Vector2 Position;
    public Vector2 Facing;
    public Vector2 Velocity;
    public HealthState Health; // If you can be seen, someone can determine how you feel
}

public struct AudioData
{
    public float DetectionTime;
    public float SoundIntensity;
    public SoundType Type; // I don't know if I actually want this or not. It could be interesting or it could be a lot of work for nothing
    public Vector2 SoundDirection;
}

public struct OlfactoryData
{
    public float ScentIntensity;
    public HealthState Health;
    public EntityType Entity; // does this even matter? Am I going to have the dinos chasing one another?
    public float ScentDirection; // this is a float because you have to follow the trail left or right. If the trail goes cold, you have to find it.
}
```

With this wealth of data, the detector brain will determine what the behavior tree should do next. Some actions could include:
- Searching the immediate area
- Tracking the player (doesn't know where the player is)
- Stalking the player (can actively see the player)
- Attacking the player
- Cutting off escape routes
- Probably other behaviors I'm not thinking of...

I guess the goal of each dino is essentially to eat the player. So they will have to use their sensor data to first locate the player.

## Execution
The brain will constantly ping the visual sensor for data. The audio and olfactory sensors will alert the brain when they detect something. You can't really turn off your vision but you are constantly smelling and hearing things. It just that only certain scents or sounds get your attention. 

## Confidence Calculation
What makes for high confidence in detections? 
- multiple detections with close values; 3 visual detections with similar results
- different detection avenues returning similar results; visual and auditory returning the same direction
- what else? When the data returns expected results that's a good sign but how do you code that?