namespace Core.Detection.Services
{
    public interface IDetectionServices
    {
        public IScentDepositService ScentDepositService { get; }
        public IScentSampleService ScentSampleService { get; }
    }
}