using System.Collections.Generic;
using Core.Inventory.Requests;
using Movement.Core.Movement.DataStructures;
using Primitives.Items;

namespace Core.Inventory
{
    public interface IInventorySystem
    {
        public Dictionary<ItemType, IInventoryItem> Items { get; }
        public IInventoryItem CurrentlyEquipped { get; }
        public int RestockItem(IItemProviderRequest provider);
        public void SwitchEquipment(SwitchEquipmentResult switchEquipment);
        public void IndexEquipment(IndexEquipmentResult indexExquipment);
    }
}