namespace AI.Core.State
{
    public interface ISearchAreaContext : IDetectPlayerContext
    {
        public void FaceLeft();
        public void FaceRight();
        public void SearchArea();
    }
}