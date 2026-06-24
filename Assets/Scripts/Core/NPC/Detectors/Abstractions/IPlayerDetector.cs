using Primitives.Detectors;

namespace Enemy.Core.Detectors.Abstractions
{
    public interface IPlayerDetector
    {
        public IDetectionData DetectPlayer();

        public void ClearData();
    }

    public interface IDirectionalPlayerDetector : IPlayerDetector
    {
        public IDetectionData DetectPlayer(ISearchData searchData);
    }
}