using System;
using System.Collections.Generic;
using Primitives.Items;
using UnityEngine;

namespace Unity.Inventory
{
    [CreateAssetMenu(fileName = "EquipmentAssets", menuName = "Game/Items/Equipment Assets")]
    public class EquipmentAssets : ScriptableObject
    {
        [SerializeField] private List<ItemAssetEntry> _assets = new();
        private Dictionary<ItemType, Sprite> _assetDict = new();

        private void OnEnable()
        {
            BuildRunTime();
        }
        public void AddNewEntry(ItemAssetEntry entry)
        {
            _assets.Add(entry);
        }
        public Sprite GetSprite(ItemType item)
        {
            return _assetDict[item];
        }
        private void BuildRunTime()
        {
            _assetDict = new();
            foreach (var asset in _assets)
            {
                if (!_assetDict.ContainsKey(asset.Item))
                {
                    _assetDict[asset.Item] = asset.SpriteImage;
                }
                else
                {
                    Debug.LogError($"ItemType {asset.Item} is already associated with another sprite.");
                }
            }
        }
    }
}