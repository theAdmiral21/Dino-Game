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
        [SerializeField] private TextMeshProUGUI _magQuantity;
        [SerializeField] private TextMeshProUGUI _equipmentQuantity;
        [SerializeField] private Image _flashlightImage;
        [SerializeField] private Image _flashlightBattery;
        [SerializeField] private Image _healthBar;
        [SerializeField] private TextMeshProUGUI _medkitQuantity;
        [SerializeField] private EquipmentAssets _equipmentAssets;

        private IEventBus _eventBus;
        private ItemType _currentlyEquipped = ItemType.None;
        private Dictionary<ItemType, Sprite> _itemMap = new();

        private void Awake()
        {
            SetInventoryImage(_currentlyEquipped);
        }

        private void OnDestroy()
        {
            UnSubToEvents();
        }

        public void UpdateEquipped(CurrentEquipmentChanged evt)
        {
            _currentlyEquipped = evt.NewItem.Item;
            SetInventoryImage(_currentlyEquipped);
            // Maybe play a sound? 

            // Update the storage
            _equipmentQuantity.text = $"{evt.NewItem.Quantity}";
        }

        public void UpdateFlashlight(FlashlightToggled evt)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateHealth(OnHealthChanged evt)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateMedkitQuantity(MedkitQuantityChanged evt)
        {
            _medkitQuantity.text = $"x{evt.CurrentQuantity}";
        }

        public void UpdateEquippedQuantity(int quantity)
        {
            // _equipmentQuantity.text = $"{quantity}";
        }

        public void UpdateEquippedQuantity(EquipmentQuantityChanged evt)
        {
            // Debug.Log($"Got {evt.CurrentQuantity} items");
            _equipmentQuantity.text = $"{evt.CurrentQuantity}";
        }
        public void UpdateMagazineQuantity(int quantity)
        {
            _equipmentQuantity.text = $"{quantity}";
        }

        public void UpdateMagazineQuantity(MagazineQuantityChanged evt)
        {
            _magQuantity.text = $"{evt.CurrentQuantity}";
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
            _eventBus.Subscribe<MagazineQuantityChanged>(HandleMagazineQuantityChanged);
            _eventBus.Subscribe<MedkitQuantityChanged>(HandleMedkitQuantityChanged);
            _eventBus.Subscribe<FlashlightToggled>(UpdateFlashlight);
            _eventBus.Subscribe<OnHealthChanged>(UpdateHealth);
            // Debug.Log($"Inventory presenter subbed to events");
        }

        private void UnSubToEvents()
        {
            _eventBus.Unsubscribe<CurrentEquipmentChanged>(UpdateEquipped);
            _eventBus.Unsubscribe<EquipmentQuantityChanged>(HandleQuantityChanged);
            _eventBus.Unsubscribe<MagazineQuantityChanged>(HandleMagazineQuantityChanged);
            _eventBus.Unsubscribe<MedkitQuantityChanged>(HandleMedkitQuantityChanged);
            _eventBus.Unsubscribe<FlashlightToggled>(UpdateFlashlight);
            _eventBus.Unsubscribe<OnHealthChanged>(UpdateHealth);
        }
        private void HandleMagazineQuantityChanged(MagazineQuantityChanged evt)
        {
            UpdateMagazineQuantity(evt);
        }
        private void HandleQuantityChanged(EquipmentQuantityChanged evt)
        {
            // Debug.Log($"Got quantity changed event with {evt.CurrentQuantity} items");

            // if the quantity that changed is our currently equipped item
            UpdateEquippedQuantity(evt);
        }

        private void HandleMedkitQuantityChanged(MedkitQuantityChanged evt)
        {
            UpdateMedkitQuantity(evt);
        }

        private void SetInventoryImage(ItemType item)
        {
            if (item == ItemType.None)
            {
                _equippedImage.enabled = false;
                _equipmentQuantity.enabled = false;
                _magQuantity.enabled = false;
            }
            else
            {
                _equippedImage.enabled = true;
                _equipmentQuantity.enabled = true;
                _magQuantity.enabled = true;
                _equippedImage.sprite = _equipmentAssets.GetSprite(item);
            }

        }
    }
}