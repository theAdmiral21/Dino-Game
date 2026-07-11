using Core.Detection.Services;

namespace Core.Detection.Olfactory
{
    public interface IScentMap : IScentDepositService, IScentSampleService
    {
        // public void Deposit(OlfactoryData data);

        // public List<OlfactoryData> Sample(Vector2 position, float scentRadius);

        public void Decay(float dt);
    }
}