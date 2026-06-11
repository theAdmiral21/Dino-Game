using Primitives.Characters;
using UnityEngine;
namespace Infrastructure.Unity.DataStructures
{
    // Create a ScriptableObject to hold the PlayerStats
    [CreateAssetMenu(fileName = "NewCostume", menuName = "Configs/Character Costume")]
    public class CharacterCostume : ScriptableObject
    {
        public CostumeEnum Costume;
        public Sprite Portrait;
        public AnimatorOverrideController CostumeOverride;
        public PortraitData PortraitDict;
        public Sprite Avatar;
        public bool unlocked;
    }
}