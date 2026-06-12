# Detection System

In order to make gameplay more engaging and challenging for the player I am implementing a detection system. There will be three ways for the dinosaurs to detect the player:
1. Sight
2. Hearing
3. Smell

## Sight
Sight will be pretty straight forward. There will be a cone of vision facing the direction that the enemy is facing. This cone will detect see-able objects and calculate a score that determines whether or not the enemy actually sees the object. Sight scores are modified by light, concealment, and physical objects.
  
Things that create light ie flashlights, ambient lighting, and flares, will draw a lot of attention negating darkness and concealment (hiding in the bushes).

```c#
public interface IVisualDetector
{
    public float Distance { get; }
    public float Acuity { get; }
    public float NightVision { get; }
    public float ObsAmbientLight { get; }
    public Dictionary<IVisible,VisualScore> Look();
}

public struct VisualScore()
{

}

public interface IVisible
{
    public float Concealment { get; }
    public float Emission { get; }
    public float TarAmbientLight { get; }

    public void UpdateVisibility();
}
```

The visual detector will perform a scan using ray casts or some sort of cone visual. When scanning the detector will collect all of the IVisible elements within the scan area from the observation position to distance. Then a calculation will be done to determine the visual score of the object. Things like Acuity, NightVision, AmbientLight, Emission, and distance to object all contribute to a higher visual score. Larger visual scores mean it is easier for the dinosaur to see you. Concealment is used to lower your visual score. Concealment is hiding in bushes and what not.

### Visual Factors
Distance: The distance the visual detector can see to.
Acuity: The sensitivity of the visual detector.
NightVision: The amount used to negate low ambient light.
ObsAmbientLight: A measure of how bright the area is around the observer
DistanceToTarget: The distance from the observer to the target
Concealment: The amount of cover provided by the local environment.
Emission: How much light the target is giving off.
TarAmbientLight: The ambient light around the target.

### Factor Relationships
All of the visual factors are related to one another and work together to produce a visual score. This visual score ultimately decides whether or not a dinosaur can see you.   
- The distance to the target inversely affects the visual score. The further you are from the observer the harder it is to see you.
- ObsAmbientLight and TarAmbientLight have a rather complex relationship. 
  - If the observer is in the dark and the target is in the light, the target is easier to see
  - If the observer is in the light and the target is in the dark, the target is harder to see
  - If both the observer and the target are in the dark, the target is harder to see
  - If both the observer and the target are in the light, the target is easier to see
- NightVision makes it easier for the observer to see targets in the dark, essentially negating some effects of a low ambient light score.
- Emission makes it easier for the observer to see the target only when there is a large difference between the level of emission and the ambient light. ie: a flashlight is easier to see in the dark than it is in the light
- Concealment is tricky to nail down. objects in the environment as well as the target's movement contribute to this.
  - A lot of movement lowers your concealment score.
  - Hiding behind objects in the scenery increases your concealment score.