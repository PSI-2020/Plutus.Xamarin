using Plutus.Interfaces;
using System;
using System.Threading.Tasks;

namespace Plutus
{
    public class PaymentFrontendService : IPaymentFrontEndService
    {
        private readonly PlutusApiClient _plutusApiClient;

        public PaymentFrontendService(PlutusApiClient plutusApi) => _plutusApiClient = plutusApi;


        public async Task AddPaymentAsync(IInfoHolder chi)
        {
            var date = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var payment = new Payment
            {
                Date = date,
                Name = chi.CurrentName,
                Amount = double.Parse(chi.CurrentAmout),
                Category = chi.CurrentCategory
            };

            await _plutusApiClient.PostPaymentAsync(payment, chi.CurrentType);
        }

        public async void AddCartPaymentAsync(string name, double amount, string category)
        {
            var date = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var payment = new Payment
            {
                Date = date,
                Name = name,
                Amount = amount,
                Category = category
            };
            await _plutusApiClient.PostPaymentAsync(payment, "Expense"); ;
        }
    }
}
