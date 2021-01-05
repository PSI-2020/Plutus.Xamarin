using System.Collections.Generic;

namespace Plutus
{
    public class ShoppingFrontEndService
    {
        private readonly List<ShoppingExpense> _shoppingBag;

        public ShoppingFrontEndService(Cart cart)
        {
            _shoppingBag = new List<ShoppingExpense>();
            for (var i = 0; i < cart.GiveElementC(); i++)
            {
                var expense = new ShoppingExpense(cart.GiveExpense(i), 0);
                _shoppingBag.Add(expense);
            }
        }

        public void ElementClicked(int index) => _shoppingBag[index].State = _shoppingBag[index].State == 0 ? 1 : 0;
        public List<ShoppingExpense> ChargeShopping() => _shoppingBag;

        public List<(string, int)> GiveExpenses(int state)
        {
            var list = new List<(string, int)>();
            for(var i = 0; i < _shoppingBag.Count; i++)
            {
                if (_shoppingBag[i].State == state) list.Add((_shoppingBag[i].Name, i));
            }
            return list;
        }

    }
}
