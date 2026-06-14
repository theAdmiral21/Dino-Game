using System;
using Primitives.Items;

namespace Unity.Equipment.DataStructures
{
    [Serializable]
    public class StatEntry
    {
        public ItemType Key;
        public EquipmentSO Data;
    }
}