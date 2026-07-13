using System;

namespace Primitives.SaveData
{
    [Serializable]
    public struct PlayerSaveData
    {
        public HealthSaveData HealthData;
        public KinematicSaveData FrameData;
        public InventorySaveData InventoryData;
        public EquipmentSaveData EquipmentData;
        public CheckpointSaveData CheckpointData;
        // public IDamageOverlay DamageOverlay;

    }
}