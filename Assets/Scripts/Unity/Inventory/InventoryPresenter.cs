using Primitives.Items;
using Core.Game;
using Core.Inventory;
using Primitives.EventBus.Abstractions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Unity.Inventory
{
    public class InventoryPresenter : MonoBehaviour, IInventoryPresenter
    {
        [SerializeField] private Image _equippedImage;
        [SerializeField] private TextMeshProUGUI _equipmentQuantity;
        [SerializeField] private Image _flashlightImage;
        [SerializeField] private Image _flashlightBattery;
        [SerializeField] private Image _healthBar;
        [SerializeField] private TextMeshProUGUI _medkitQuantity;
        [SerializeField] private EquipmentAssets _equipmentAssets;

        private IEventBus _eventBus;
        private ItemType _currentlyEquipped = ItemType.None;
        private Dictionary<ItemType, Sprite> _itemMap = new();

        private void OnDestroy()
        {
            UnSubToEvents();
        }

        public void UpdateEquipped(CurrentEquipmentChanged evt)
        {
            _currentlyEquipped = evt.NewItem.Item;
            SetInventoryImage(_currentlyEquipped);
            // Maybe play a sound? 
            UpdateEquippedQuantity(evt.NewItem.Quantity);
        }

        public void UpdateFlashlight(FlashlightToggled evt)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateHealth(OnHealthChanged evt)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateMedkitQuantity(EquipmentQuantityChanged evt)
        {
            _medkitQuantity.text = $"x{evt.CurrentQuantity}";
        }

        public void UpdateEquippedQuantity(int quantity)
        {
            _equipmentQuantity.text = $"x{quantity}";
        }

        public void UpdateEquippedQuantity(EquipmentQuantityChanged evt)
        {
            _equipmentQuantity.text = $"x{evt.CurrentQuantity}";
        }

        public void SetEventBus(IEventBus eventBus)
        {
            if (_eventBus == null)
            {
                _eventBus = eventBus;
                SubToEvents();
            }
        }

        private void SubToEvents()
        {
            _eventBus.Subscribe<CurrentEquipmentChanged>(UpdateEquipped);
            _eventBus.Subscribe<EquipmentQuantityChanged>(HandleQuantityChanged);
            _eventBus.Subscribe<FlashlightToggled>(UpdateFlashlight);
            _eventBus.Subscribe<OnHealthChanged>(UpdateHealth);
            Debug.Log($"Inventory presenter subbed to events");
        }

        private void UnSubToEvents()
        {
            _eventBus.Unsubscribe<CurrentEquipmentChanged>(UpdateEquipped);
            _eventBus.Unsubscribe<EquipmentQuantityChanged>(HandleQuantityChanged);
            _eventBus.Unsubscribe<FlashlightToggled>(UpdateFlashlight);
            _eventBus.Subscribe<OnHealthChanged>(UpdateHealth);
        }

        private void HandleQuantityChanged(EquipmentQuantityChanged evt)
        {
            Debug.Log($"Got quantity changed event");

            // if the quantity that changed is our currently equipped item OR a medkit, update the ui

            if (evt.Item == _currentlyEquipped)
            {
                UpdateEquippedQuantity(evt);
            }
            else if (evt.Item == ItemType.Medkit)
            {
                UpdateMedkitQuantity(evt);
            }

        }

        private void SetInventoryImage(ItemType item)
        {
            _equippedImage.sprite = _equipmentAssets.GetSprite(item);
        }
    }
}