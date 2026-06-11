namespace Primitives.Common.Scenes
{
    public enum SceneId
    {
        Boot,
        Intro,
        MainMenu,
        Jungle,
        PowerStation,
        ControlCenter,
        Credits,
        None,


#if UNITY_EDITOR
        DevLevel,
#endif
    }
}