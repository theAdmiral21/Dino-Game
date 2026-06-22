using Core.Detection.Audio;
using Core.Detection.Audio.DataStructures;
using UnityEngine;

namespace Unity.Detection
{
    public class SoundDetector : MonoBehaviour, ISoundDetector
    {
        public float Sensitivity => throw new System.NotImplementedException();

        public void Detect(EmittedSound sound)
        {
            throw new System.NotImplementedException();
        }
    }
}