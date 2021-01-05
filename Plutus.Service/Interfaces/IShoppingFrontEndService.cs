using System.Collections.Generic;

namespace Plutus.Interfaces
{
    public interface IShoppingFrontEndService
    {
        public void InitializeShoppingService(Cart cart);
        public void ElementClicked(int index);
        public void ChargeShoppingAsync();
        public List<string> GiveExpenses(int state);
    }
}
