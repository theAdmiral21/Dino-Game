using Primitives.Items;

namespace Core.Inventory
{
    public record EquipmentQuantityChanged
    {
        // public IInventoryItem InventoryItem;
        public int CurrentQuantity;
    }
}