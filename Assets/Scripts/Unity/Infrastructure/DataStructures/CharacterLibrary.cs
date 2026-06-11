using System;
using System.Collections.Generic;
using Primitives.Characters;
using UnityEngine;
namespace Infrastructure.Unity.DataStructures
{
    // Create a ScriptableObject to hold the PlayerStats
    [CreateAssetMenu(fileName = "NewCharacterLibrary", menuName = "Configs/Character Library")]
    public class CharacterLibrary : ScriptableObject
    {
        public List<CharacterConfig> Characters = new List<CharacterConfig>();
        public CharacterID[] CharacterIds => (CharacterID[])Enum.GetValues(typeof(CharacterID));

        // #if UNITY_EDITOR
        //     public void AddNewCharacter(CharacterData data)
        //     {
        //         // Add the character data to the list
        //         Characters.Add(data);
        //         // Update the enums
        //         CharacterEnumGenerator.RegenerateEnum(this);
        //     }
        // #endif
        public CharacterConfig? GetCharacter(string name)
        {
            foreach (CharacterConfig data in Characters)
            {
                if (data.CharacterName == name)
                {
                    return data;
                }
            }
            Debug.LogError($"Could not find character: {name}");
            return null;
        }

        public CharacterConfig? GetCharacter(CharacterID? character)
        {
            if (character == null) return null;
            foreach (CharacterConfig data in Characters)
            {
                if (data.CharacterId == character)
                {
                    return data;
                }
            }
            Debug.LogError($"Could not find character: {character}");
            return null;
        }

        public CharacterConfig? GetCharacter(int index)
        {
            return GetCharacter(CharacterIds[index]);
        }

    }
}