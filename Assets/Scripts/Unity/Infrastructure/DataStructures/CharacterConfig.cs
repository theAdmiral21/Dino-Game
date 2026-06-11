using System.Collections.Generic;
using Primitives.Characters;
using UnityEngine;

namespace Infrastructure.Unity.DataStructures
{
    // Create a ScriptableObject to hold the PlayerStats
    [CreateAssetMenu(fileName = "NewCharacterData", menuName = "Configs/Character Data")]
    public class CharacterConfig : ScriptableObject
    {
        public string CharacterName;
        public CharacterID CharacterId;
        public CharacterData Data;
        public List<CharacterCostume> Costume = new List<CharacterCostume>();
        public AudioClip Voice;
        public Sprite oneUp;
        public RuntimeAnimatorController characterAnimator;
        public RuntimeAnimatorController portriatAnimator;

    }

}