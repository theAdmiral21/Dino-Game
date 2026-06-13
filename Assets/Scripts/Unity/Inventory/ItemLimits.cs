using System;
using UnityEngine;

namespace Unity.Inventory
{
    [CreateAssetMenu(fileName = "ItemLimits", menuName = "Game/Items/Carry Limits")]

    [Serializable]

    public class ItemLimits : ScriptableObject
    {
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