using Core.Game;
using Physics.Core.PhysicsActors;
using Primitives.EventBus.Abstractions;

namespace Core.Inventory
{
    public interface IInventoryPresenter
    {
        public void SetEventBus(IEventBus eventBusProvider);
        public void UpdateEquipped(CurrentEquipmentChanged evt);
        public void UpdateEquippedQuantity(int quantity);
        public void UpdateEquippedQuantity(EquipmentQuantityChanged evt);
        public void UpdateHealth(OnHealthChanged evt);
        public void UpdateMedkitQuantity(EquipmentQuantityChanged evt);
        public void UpdateFlashlight(FlashlightToggled evt);
    }
}