# Sound Detection
For sound detection there will be two classes: a SoundDetector and a SoundEmitter. The emitter will emit sound based on actions. For example when throwing the rock. Detectors will detect sounds broadcast by emitters. Emitters will dictate how loud and far reach a sound is where detectors will define how sensitive they are.

Something like:
```c#
public interface ISoundEmitter
{
    public Vector2 Origin { get; }
    public float MinRadius { get; }
    public SurfaceTag GetSurface();
    public float GetSpeed();
    public void EmitSound(float soundRadius);
}

public interface ISoundDetector
{
    public float Sensitivity { get; }
    public void Detect(EmittedSound sound)
}

public struct EmittedSound
{
    public Vector2 Origin; // uhh what else?
    public float Radius;
    public SoundType Type;
}
```

Honestly I think I need some sort of sound score struct to really make this work. The emitter will construct the struct, find the detectors, and then call detect on the detectors with the struct. The detectors will then do *something* with that information. Another thing I might want to consider is attenuation. Sounds are emitted as spheres but they only have so much energy (or whatever the measurement is). That means if the door is shut and you make a sound it will be quieter to things on the other side of the door. This could be a raycast from the sound origin to the detector to get everything that might attenuate the sound and then use distance and surfaces to make sound quieter or louder thus effecting if the detector actually hear's something. 

I am using the radius of the emitted sound as a measure of the intensity and fall off. Louder sounds have a larger radius quieter sounds have a small radius. This is also apparent in a weapon's stats. 

```c#
public void Detect(EmittedSound sound)
{
    // Calc fall off 
    float fallOff = CalcFallOff(sound);
    // Calc attenuation
    float attenuation = CalcAttenuation(distance);
    // Calc perceived intensity
    float perceivedIntensity = fallOff * attenuation * sensitivity;

    if (perceivedIntensity > threshold)
    {
        React();
    }
}

private float CalcFallOff(EmittedSound sound)
{
    float distance = Vector2.Distance(sound.Origin, transform.position);
    float fallOff = 1 - (distance/sound.radius);
    return fallOff;
}

private float CalcAttenuation(float distance)
{
    // some sort of occlusion magic
}
```