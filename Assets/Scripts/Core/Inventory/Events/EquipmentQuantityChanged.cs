using Primitives.Items;

namespace Core.Inventory
{
    public record EquipmentQuantityChanged
    {
        public ItemType Item;
        public int CurrentQuantity;
    }
}