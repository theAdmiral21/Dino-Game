using System.Collections.Generic;
using Core.Equipment;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Primitives.Items;
using Unity.Equipment.DataStructures;
using UnityEngine;

namespace Unity.Equipment
{
    public class EquipmentFactory : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        [SerializeField] private EquipmentMapSO _mapSO;
        private Dictionary<ItemType, EquipmentStats> _statMap;
        [SerializeField] private GameObject _rockPrefab;

        public int Priority => 0;

        private IGameContext _gameContext;

        public IEquipment BuildEquipment(ItemType item, Transform anchor)
        {
            if (_statMap == null) _statMap = _mapSO.GetStatMap();

            switch (item)
            {
                case ItemType.Rock:
                    {
                        GameObject rock = Instantiate(_rockPrefab, anchor);
                        rock.transform.position = anchor.position;
                        Debug.Assert(rock != null, "Why is rock null?");
                        IEquipment equipment = rock.GetComponent<IEquipment>();
                        equipment.Init(_statMap[item], _gameContext);
                        Debug.Assert(equipment != null, "Why is equipment null?");
                        return equipment;
                    }
                default:
                    {
                        Debug.LogError($"Item type: {item} is not currently supported.");
                        return null;
                    }
            }
        }

        public void Initialize(IGameContext context)
        {
            _gameContext = context;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_gameContext != null, $"failed to assign _game context");
        }
    }
}