namespace PlayerController.Application.Inputs.DataStructures
{
    public struct InputInfo
    {
        public bool InputLeft;
        public bool InputRight;
        public bool InputDown;
        public bool InputUp;
        public bool DownAndLeft;
        public bool DownAndRight;
        public bool UpAndLeft;
        public bool UpAndRight;

        public void Reset()
        {
            InputLeft = false;
            InputRight = false;
            InputDown = false;
            InputUp = false;
            DownAndLeft = false;
            DownAndRight = false;
            UpAndLeft = false;
            UpAndRight = false;
        }
    }
}