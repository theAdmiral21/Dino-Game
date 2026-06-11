using Movement.Unity.Stats;
using UnityEngine;

namespace PlayerController.Unity.Stats
{
    [CreateAssetMenu(menuName = "Stats/Config/Player Stats Config")]
    public class PlayerStatsConfig : ScriptableObject
    {
        [Header("Movement")]
        public RunStatSO Run;
        public SprintStatSO Sprint;
        public AerialStatSO Aerial;
        public JumpStatSO Jump;
        public DoubleJumpStatSO DoubleJump;
        public WallStatSO Wall;

        [Header("Physics Properties")]
        public GravityStatSO Gravity;

        [Header("Abilities")]
        public LongJumpStatSO LongJump;
        public QuickStepStatSO QuickStep;
        public TeleportStatSO Teleport;

        [Header("Combat")]
        public InvincibilityStatSO Invincibility;

    }
}