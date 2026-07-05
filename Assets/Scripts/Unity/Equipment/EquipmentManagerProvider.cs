using Core.Equipment;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Equipment
{
    public class EquipmentManagerProvider : MonoBehaviour, IEquipmentManagerProvider
    {
        [SerializeField] private SerializedInterface<IEquipmentManager> _equipmentManagerMono;
        public IEquipmentManager EquipmentManager => _equipmentManagerMono.Interface;
    }
}