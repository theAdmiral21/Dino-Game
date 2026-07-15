using System;
using System.Collections.Generic;
using Core.Inventory.Requests;
using Movement.Core.Movement.DataStructures;
using Primitives.Items;
using Primitives.SaveData;

namespace Core.Inventory
{
    public interface IInventorySystem : IDisposable
    {
        public Dictionary<ItemType, IInventoryItem> Items { get; }
        public IInventoryItem CurrentlyEquipped { get; }
        public int RestockItem(IItemProviderRequest provider);
        public void SwitchEquipment(SwitchEquipmentResult switchEquipment);
        public void IndexEquipment(IndexEquipmentResult indexExquipment);
        public void UpdateInventoryContents(InventorySaveData saveData);
    }
}