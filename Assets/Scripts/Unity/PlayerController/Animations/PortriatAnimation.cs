using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerController.Animations
{
    public class PortriatAnimation : MonoBehaviour
    {
        public Image portriat;
        public Sprite[] spriteArray;
        public float frameRate = 0.1f;
        private int _currentFrame = 0;
        private float _timer = 0.0f;

        private Dictionary<string, int> _mood = new Dictionary<string, int>();

        // Start

        private void SetupDictionary()
        {
            // Setup normal
            _mood.Add("normal", 0);
            // Setup scared
            _mood.Add("scared", 2);
            // Setup confused
            _mood.Add("confused", 4);
            // Setup annoyed
            _mood.Add("annoyed", 6);
            // Setup angry
            _mood.Add("angry", 8);
        }

        public void GetPortriatData()
        {
            if (spriteArray.Length > 0)
            {
                portriat.sprite = spriteArray[0];
            }
        }
        // Stop
        // Update is called once per frame
        void Update()
        {
            drawAnimation("normal");
        }

        void drawAnimation(string mood)
        {
            if (spriteArray.Length == 0)
            {
                return;
            }
            // Get the start ndx
            int start = _mood[mood];
            _timer += Time.deltaTime;

            if (_timer >= frameRate)
            {
                _timer -= frameRate;

                //Theres only two frames so..
                if (_currentFrame == start)
                {
                    _currentFrame += 1;
                }
                else
                {
                    _currentFrame = start;
                }
                portriat.sprite = spriteArray[_currentFrame];
            }
        }
    }
}