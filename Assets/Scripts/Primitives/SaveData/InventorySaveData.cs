using System.Collections.Generic;
using Primitives.Items;

namespace Primitives.SaveData
{
    [System.Serializable]
    public struct InventorySaveData
    {
        public ItemType CurrentItem;
        public List<ItemStock> Items;
    }
    [System.Serializable]
    public struct ItemStock
    {
        public ItemType Item;
        public int Amount;
    }
}