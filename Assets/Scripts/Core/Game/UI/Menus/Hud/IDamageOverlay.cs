using Primitives.Health;

namespace Core.Game.UI.Menus.Hud
{
    public interface IDamageOverlay
    {
        public void UpdateOverlay(HealthState state);
    }
}