using System.Collections.Generic;
using System.IO;
using Primitives.Checkpoints;
using Primitives.Items;
using Primitives.SaveData;
using UnityEngine;

namespace Unity.Game.SaveData
{
    [CreateAssetMenu(menuName = "Game/Save/New Game Defaults")]
    public class DefaultPlayerDataSO : ScriptableObject
    {
        [Header("Health Data")]
        public int StartingHealth;
        [Header("Kinematic Data")]
        public Vector2 ReloadPosition;
        [Header("Inventory Data")]
        public ItemType EquippedItem;
        public List<ItemStock> FoundItems;
        [Header("Inventory Data")]
        public int RoundsInMagazine;
        [Header("Checkpoint Data")]
        public CheckpointId Id;

        public PlayerSaveData GetPlayerDefaults()
        {
            HealthSaveData HealthData;
            KinematicSaveData FrameData;
            InventorySaveData InventoryData;
            EquipmentSaveData EquipmentData;
            CheckpointSaveData CheckpointData;

            // make the structs and make sure things are copasetic
            bool healthIsValid = false;
            if (StartingHealth > 0 && StartingHealth <= 100)
            {
                healthIsValid = true;
            }
            if (!healthIsValid) throw new InvalidDataException($"Starting health must be greater than zero and less than 100.");
            HealthData = new HealthSaveData { CurrentHealth = StartingHealth };

            // Hmm the level tells you where to start.. does this make sense?
            FrameData = new KinematicSaveData { ReloadPosition = ReloadPosition };

            bool equippedItemIsValid = false;
            foreach (var item in FoundItems)
            {
                if (item.Item == EquippedItem)
                {

                    equippedItemIsValid = true;
                    break;
                }
            }
            if (!equippedItemIsValid)
            {
                throw new InvalidDataException($"Equipped item is not in the FoundItems list.");
            }
            InventoryData = new InventorySaveData
            {
                CurrentItem = EquippedItem,
                Items = FoundItems
            };

            // Checking the magazine is hard. Just don't fuck it up
            EquipmentData = new EquipmentSaveData { RoundsInMagazine = RoundsInMagazine };

            // Checkpoints are also weird because their id changes every time the game runs...
            CheckpointData = new CheckpointSaveData { Id = Id };


            return new PlayerSaveData
            {
                HealthData = HealthData,
                FrameData = FrameData,
                InventoryData = InventoryData,
                EquipmentData = EquipmentData,
                CheckpointData = CheckpointData,
            };
        }
    }
}