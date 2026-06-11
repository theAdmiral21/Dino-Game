using Infrastructure.Unity.DataStructures;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class GetCharacterConfig : MonoBehaviour
    {
        // This will need some sort of save data to determine if a check point is active
        [SerializeField] private CharacterLibrary _library;
        public SpawnData GetCharacterData(ref SpawnData data)
        {
            data.CharacterInfo = _library.GetCharacter(data.Id);
            // Override the animator
            var animator = data.PlayerObject.GetComponentInChildren<Animator>();

            return data;
        }
    }
}