using Core.Detection.Olfactory;
using Core.Detection.Services;

namespace Application.Detection.Services
{
    public class DetectionServices : IDetectionServices
    {
        public IScentDepositService ScentDepositService { get; private set; }
        public IScentSampleService ScentSampleService { get; private set; }
        private IScentMap _scentMap;
        public DetectionServices(IScentMap scentMap)
        {
            _scentMap = scentMap;
            ScentDepositService = _scentMap;
            ScentSampleService = _scentMap;
        }
    }
}