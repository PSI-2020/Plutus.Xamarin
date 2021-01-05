using System.Threading.Tasks;

namespace Plutus.Interfaces
{
    public interface IPaymentFrontEndService
    {
        public Task AddPaymentAsync(IInfoHolder chi);
        public void AddCartPaymentAsync(string name, double amount, string category);
    }
}
