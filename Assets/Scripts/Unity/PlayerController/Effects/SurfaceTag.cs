using Game.Core.Effects;
using Primitives.Audio;
using UnityEngine;

namespace PlayerController.Unity.Effects
{
    public class SurfaceTag : MonoBehaviour, ISurfaceTag
    {
        [SerializeField] SurfaceType _tag;
        public SurfaceType Tag => _tag;
    }
}