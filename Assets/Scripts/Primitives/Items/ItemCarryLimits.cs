using System;

namespace Primitives.Items
{
    [Serializable]
    public class ItemCarryLimits
    {
        // You can only ever hold one weapon or flashlight
        public int Taser = 1;
        public int Shotgun = 1;
        public int RocketLauncher = 1;
        public int NerveGas = 1;
        public int Flashlight = 1;

        // Ammo
        public int Shell;
        public int Rocket;
        public int Canister;

        // Utility Items
        public int Rocks;
        public int Medkit;
        public int Flares;
        public int SmokeGrenade;
    }
}
