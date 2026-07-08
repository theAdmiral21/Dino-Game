using Core.Equipment;
using Core.Inventory;
using Movement.Core.Movement.DataStructures;
using Primitives.EventBus.Abstractions;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Equipment
{
    public class EquipmentManager : MonoBehaviour, IEquipmentManager
    {
        [SerializeField] private Transform _facingTransform;
        [SerializeField] private Transform _equipmentAnchor;

        private Vector3 _anchorScale;

        [SerializeField] private SerializedInterface<IEquipmentBridge> _equipmentBridgeMono;
        private IEquipmentBridge _equipmentBridge => _equipmentBridgeMono.Interface;


        public IEquipment ActiveEquipment { get; private set; }
        public IInventoryItem ActiveItem { get; private set; }

        [SerializeField] private EquipmentFactory _equipmentFactory;
        private IEventBus _inventoryEventBus;
        private IInventorySystem _inventorySystem;

        [Header("Debug")]
        [SerializeField] private string _currentEquipment;
        [SerializeField] private string _currentItem;

        private void OnDestroy()
        {
            UnsubToEvents();
        }

        public void HandleEquipmentChanged(CurrentEquipmentChanged evt)
        {
            Debug.Log($"Got switch equipment event");
            TearDownActiveEquipment();

            SetUpActiveEquipment(evt);
        }

        public void HandleEquipmentChangeResult(SwitchEquipmentResult switchEquipment)
        {
            Debug.Log($"Got switch equipment result");
            _inventorySystem.SwitchEquipment(switchEquipment);
        }

        private void SetUpActiveEquipment(CurrentEquipmentChanged evt)
        {
            Debug.Log($"Setting up new equipment: {evt.NewItem.Item}");
            // Instantiate the new equipment
            IEquipment equipment = _equipmentFactory.BuildEquipment(evt.NewItem.Item, _equipmentAnchor);

            // Assign the equipment and item
            ActiveEquipment = equipment;
            ActiveItem = evt.NewItem;

            // Connect events
            ActiveEquipment.OnFire += ActiveItem.HandleFire;
            ActiveEquipment.OnReload += ActiveItem.HandleReload;

            // Notify the presenter how much ammo is in the magazine
            _inventoryEventBus.Publish(new MagazineQuantityChanged
            {
                CurrentQuantity = ActiveEquipment.RoundCount
            });
        }
        private void TearDownActiveEquipment()
        {
            if (ActiveEquipment == null) return;

            // Disconnect events
            ActiveEquipment.OnFire -= ActiveItem.HandleFire;
            ActiveEquipment.OnReload -= ActiveItem.HandleReload;
            // Destroy the equipment game object
            GameObject equipmentObject = ActiveEquipment.GetComponent<Transform>().gameObject;
            ActiveEquipment = null;
            Destroy(equipmentObject);

        }

        public void Init(IEventBus eventBus, IInventorySystem inventorySystem)
        {
            Debug.Log($"Initializing equipment manager");
            _inventoryEventBus = eventBus;
            Debug.Assert(_inventoryEventBus != null, "Inventory event bus is null.");
            SubToEvents();

            _inventorySystem = inventorySystem;
        }

        private void SubToEvents()
        {
            _inventoryEventBus.Subscribe<CurrentEquipmentChanged>(HandleEquipmentChanged);
        }
        private void UnsubToEvents()
        {
            _inventoryEventBus.Unsubscribe<CurrentEquipmentChanged>(HandleEquipmentChanged);
        }

        private void Update()
        {
            _anchorScale = _equipmentAnchor.localScale;
            _anchorScale.x = Mathf.Sign(_facingTransform.localScale.x);
            _equipmentAnchor.localScale = _anchorScale;
        }

        private void LateUpdate()
        {
            if (ActiveEquipment != null)
            {
                _currentEquipment = ActiveEquipment.ToString();
            }

            if (ActiveItem != null)
            {
                _currentItem = ActiveItem.ToString();
            }
        }
    }
}
