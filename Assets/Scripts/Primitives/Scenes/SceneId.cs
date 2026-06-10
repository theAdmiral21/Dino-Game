namespace Primitives.Common.Scenes
{
    public enum SceneId
    {
        Boot,
        Intro,
        MainMenu,
        CharacterSelect,
        LevelSelect,
        Records,
        MultiplayerScoreScreen,
        Credits,
        None,

        // Level 1
        CityIntroCutScene,
        CityMain,
        CityDolphin,
        CityAlt,
        CityBoss,

        // Level 2
        FarmMain,
        FarmDolphin,
        FarmBoss,

        // Level 3
        RiverMain,
        RiverDolphin,
        RiverBoss,

        // Level 4
        FactoryMain,
        FactoryDolphin,
        FactoryBoss,

        // Level 5
        TornadoMain,
        TornadoDolphin,
        TornadoBoss,

        // Level 6
        MountainMain,
        MountainDolphin,
        MountainBoss,

        // Level 7
        MinesMain,
        MinesDolphin,
        MinesBoss,

        // Level 8
        CanyonMain,
        CanyonDolphin,
        CanyonBoss,

        // Level 9
        DesertMain,
        DesertDolphin,
        DesertBoss,

        // Level 10
        BalloonMain,
        BalloonDolphin,
        BalloonBoss,

        // Level 11
        EtherealMain,
        EtherealDolphin,
        EtherealBoss,

        // Multiplayer Levels
        CityMulti,
        FarmMulti,
        RiverMulti,
        FactoryMulti,
        TornadoMulti,
        MountainMulti,
        MinesMulti,
        CanyonMulti,
        DesertMulti,
        BalloonMulti,
        EtherealMulti,
        SpaceMulti,

#if UNITY_EDITOR
        TestMenu,
        TestDolphin,
        DevLevel,
#endif
    }
}