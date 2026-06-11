using Primitives.Characters;
using UnityEngine;
using System;

namespace Infrastructure.Unity.DataStructures
{
    /// <summary>
    /// Data class for organizing portrait sprites 
    /// </summary>
    [Serializable]
    public class PortraitDataEntry
    {
        public Sprite MouthClosed;
        public Sprite MouthOpen;
        public PortraitEmotion Key;
        public Sprite[] Clip => new Sprite[] { MouthClosed, MouthOpen };
    }
}