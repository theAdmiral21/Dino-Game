using Core.Detection.Olfactory.DataStructures;

namespace Core.Detection.Services
{
    public interface IScentDepositService
    {
        public void Deposit(OlfactoryData data);
    }
}