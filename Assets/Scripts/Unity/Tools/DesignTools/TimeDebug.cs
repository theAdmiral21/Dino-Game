using UnityEngine;

namespace Tools.DesignTools
{
    public class TimeDebug : DesignToolsBase
    {
        public bool Active = false;
        public float TimeScale = 1;

        // Update is called once per frame
        void Update()
        {
            if (Active)
            {
                if (Time.timeScale != TimeScale)
                {
                    Time.timeScale = TimeScale;
                }
            }
            else
            {
                if (Time.timeScale != 1f)
                {
                    Time.timeScale = 1f;
                }
            }
        }
    }
}
