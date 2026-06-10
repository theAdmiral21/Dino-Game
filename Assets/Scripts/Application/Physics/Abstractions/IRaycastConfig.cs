namespace Physics.Application.Abstractions
{
    public interface IRaycastConfig
    {
        /// <summary>
        /// How many rays to cast in the vertical direction along the horizontal
        /// </summary>
        public int RaycastCountVertical { get; set; }
        /// <summary>
        /// How many rays to cast in the horizontal direction along the vertical
        /// </summary>
        public int RaycastCountHorizontal { get; set; }
        /// <summary>
        ///  Method for keeping the RaycastOrigins struct up to date
        /// </summary>
        public void UpdateRaycastOrigins();
        /// <summary>
        /// Method for spacing raycasts an equidistance apart.
        /// </summary>
        public void CalculateSpacing();
    }
}