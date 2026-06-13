using System;
using Codice.Client.BaseCommands.Merge.Xml;
using UnityEngine;

namespace Unity.Inventory
{
    [CreateAssetMenu(fileName = "ItemLimits", menuName = "Game/Items/Carry Limits")]

    [Serializable]

    public class ItemSO : ScriptableObject
    {
        public ItemType Item;
        [SerializeField] ItemType _item;
    }
}