namespace Core.Light
{
    public struct LightData
    {
        public readonly float AmbientLight;
        public readonly ILightEmitter LightSource;
        public bool IsValid => LightSource != null;
        public LightData(
                        float ambientLight,
                        ILightEmitter lightSource
        )
        {
            AmbientLight = ambientLight;
            LightSource = lightSource;
        }
    }
}