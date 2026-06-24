# Light Implementation

Light implementation is going to be done in two parts, rendering and game play. The rendering is what the player sees, the game play is what the detectors see.

Gameplay lighting will be handled by a trigger volume that exposes a lighting value. This lighting value is set via the inspector. When the player or animal enters a light zone, their ambient lighting score changes. There will also be some nuance depending on the source of the light. Which will be determined by a raycast.

I'm going to need a couple interfaces. 
```c#
public interface ILightContext
{
    public float GetAmbientLight();
}

public interface ILightVolume
{
    public Vector2 SourcePosition {get;}
    public Vector2 Intensity {get;}
}
```