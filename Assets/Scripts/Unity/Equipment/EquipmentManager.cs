using Core.Equipment;
using Core.Inventory;
using Primitives.EventBus.Abstractions;
using Primitives.Items;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Equipment
{
    public class EquipmentManager : MonoBehaviour, IEquipmentManager
    {
        [SerializeField] private SerializedInterface<IEquipmentBridge> _equipmentBridgeMono;
        private IEquipmentBridge _equipmentBridge => _equipmentBridgeMono.Interface;

        public IEquipment ActiveEquipment { get; private set; }

        [SerializeField] private EquipmentFactory _equipmentFactory;
        private IEventBus _inventoryEventBus;

        public void SwitchEquipment(CurrentEquipmentChanged evt)
        {
            Debug.Log($"Equipment got switch  equipment");
            TearDownActiveEquipment();

            // Instantiate the new equipment

            SetUpActiveEquipment();
        }

        private void SetUpActiveEquipment()
        {
        }
        private void TearDownActiveEquipment()
        {
            if (ActiveEquipment == null) return;
        }

        public void SetEventBus(IEventBus eventBus)
        {
            _inventoryEventBus = eventBus;
            SubToEvents();
        }

        private void SubToEvents()
        {
            _inventoryEventBus.Subscribe<CurrentEquipmentChanged>(UpdateEquipment);
        }
        private void UnsubToEvents()
        {
            _inventoryEventBus.Unsubscribe<CurrentEquipmentChanged>(UpdateEquipment);
        }

        private void UpdateEquipment(CurrentEquipmentChanged changed)
        {
            ActiveEquipment = changed.NewItem.Equipment;
        }
    }
}
