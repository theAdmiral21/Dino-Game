using Infrastructure.Unity.DataStructures;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class BuildPlayer : MonoBehaviour
    {
        public void Build(SpawnData data)
        {
            // Override the animator
            var animator = data.PlayerObject.GetComponentInChildren<Animator>();
            animator.runtimeAnimatorController = data.CharacterInfo.characterAnimator;

            data.PlayerObject.SetActive(true);
            data.CameraObject.SetActive(true);
        }
    }
}