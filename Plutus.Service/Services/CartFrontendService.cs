using Plutus.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plutus
{
    public class CartFrontendService //: ICartFrontEndService
    {
        private Cart _currentCart;
        private List<Cart> _carts;
        public bool Loaded { get; set; }

        public CartFrontendService()
        {
            _carts = new List<Cart>();
            Loaded = false;
        }
        public void CleanseCarts()
        {
            _carts = new List<Cart>();
            Loaded = false;
        }

        public void NewCart() => _currentCart = new Cart();
        public void AddExpenseToCart(string name, double amount, string category)
        {
            var expense = new CartExpense(name, amount, category, true);
            _currentCart.AddExpense(expense);
        }
        public void EditExpense(int index, string name, double amount, string category) => _currentCart.EditExpense(index, name, amount, category);

        public int GiveCurrentCartElemCount() => _currentCart.GiveElementC();

        public CartExpense GiveCurrentElemAt(int i) => _currentCart.GiveExpense(i);

        public void RemoveExpenseCurrentAt(int i) => _currentCart.RemoveExpense(i);

        public void SetCurrentName(string name) => _currentCart.ChangeName(name);
        public (int, string, List<CartExpense>) AddCurrentCart()
        {
            _carts.Add(_currentCart);
            return SaveCart();
        }

        public string GiveCurrentName() => _currentCart.GiveName();
        public int GiveCartCount() => _carts.Count;

        public string VerifyName(string name, string prevname) => _carts.Where(x => ((x.GiveName() == name) && (x.GiveName() != prevname))).Any() ? "Cart name already taken" : "";

        public string GiveCartNameAt(int i) => _carts[i].GiveName();

        public bool Changes(int i) => (_currentCart == _carts[i]) ? false : true;

        public void CurrentCartSet(int i) => _currentCart = _carts[i];

        public (int, string, List<CartExpense>) SaveCartChanges(int i)
        {
            _carts[i] = _currentCart;
            return SaveCart();
        }

        public int DeleteCurrent()
        {
            var index = _carts.IndexOf(_currentCart);
            _carts.Remove(_currentCart);
            _currentCart = null;
            return index;
        }
        public void ChangeState(int i) => _currentCart.ChangeState(i);
        public int ChargeCart() => _carts.IndexOf(_currentCart);
        public (int, string, List<CartExpense>) SaveCart()
        {
            if (_currentCart == null) return (-1, null, null);
            var index = _carts.IndexOf(_currentCart);
            var name = _currentCart.GiveName();
            var cartexpenses = new List<CartExpense>();
            for (var i = 0; i < _currentCart.GiveElementC(); i++)
            {
                cartexpenses.Add(_currentCart.GiveExpense(i));
            }
            return (index, name, cartexpenses);
        }

        public void LoadCart(string name, List<CartExpense> expenses)
        {
            var cart = new Cart(name);
            for (var i = 0; i < expenses.Count; i++)
            {
                cart.AddExpense(expenses[i]);
            }
            _carts.Add(cart);
        }

        public Cart StartShopping() => _currentCart;

    }
}
