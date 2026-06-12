using Core.Inventory;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Application.Inventory
{
    public class AmmoItem : BaseInventoryItem, IAmmoItem
    {
        public AmmoItem(ItemType item, int maxAllowed) : base(item, maxAllowed) { }

        public bool HasCorrespondingWeapon { get; private set; }

        public void AddCorrespondingWeapon(ItemType item)
        {
            switch (item)
            {
                case ItemType.Shotgun:
                    {
                        if (Item == ItemType.Shell)
                        {
                            HasCorrespondingWeapon = true;
                        }
                        break;
                    }
            }
        }

        public override bool CanAdd(IItemProviderRequest provider)
        {
            if (Quantity >= _maxAllowed) return false;

            if (provider.Item != Item) return false;

            if (!HasCorrespondingWeapon) return false;
            // Both of these will need to check if the base item is available when attempting to add/consume ammo.

            return true;
        }

        public override bool CanConsume(IItemConsumerRequest consumer)
        {
            if (Quantity <= 0) return false;

            if (consumer.Item != Item) return false;

            if (!HasCorrespondingWeapon) return false;
            // Both of these will need to check if the base item is available when attempting to add/consume ammo.

            return true;
        }
    }
}