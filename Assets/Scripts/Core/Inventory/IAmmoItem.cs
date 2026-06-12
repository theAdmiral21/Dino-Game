using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory
{
    public interface IAmmoItem : IInventoryItem
    {
        public bool HasCorrespondingWeapon { get; }

        public void AddCorrespondingWeapon(ItemType item);
    }
}