using Core.Game.UI.Menus.Hud;
using Core.Inventory;
using Physics.Core.DataStructures;

namespace Core.Game.Lifecycle.DataStructures
{
    public struct PlayerSavedData
    {
        public int HealthAmount;
        public ActorFrameData FrameData;
        public IInventorySystem InventorySystem;
        public IDamageOverlay DamageOverlay;

    }
}