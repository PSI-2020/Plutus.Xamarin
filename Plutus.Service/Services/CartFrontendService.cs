using System.Collections.Generic;
using System.Linq;

namespace Plutus
{
    public class CartFrontendService 
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

        public void NewCart()
        {
            _currentCart = new Cart();
            _currentCart.CartId = -1;
        }
        public CartExpense AddExpenseToCart(string name, double amount, string category)
        {
            var expense = new CartExpense(name, amount, category, true);
            expense.ExpenseId = 0;
            _currentCart.AddExpense(expense);
            return expense;
        }
        public CartExpense EditExpense(int index, string name, double amount, string category) 
        {
            _currentCart.EditExpense(index, name, amount, category);
            return _currentCart.GiveExpense(index);
        }

        public int GiveCurrentCartElemCount() => _currentCart.GiveElementC();

        public CartExpense GiveCurrentElemAt(int i) => _currentCart.GiveExpense(i);

        public int RemoveExpenseCurrentAt(int i)
        {
            var ide = _currentCart.GiveExpense(i);
            _currentCart.RemoveExpense(i);
            return ide.ExpenseId;
        }
        public int GiveCurrentId() => _currentCart.CartId;

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
            var id = _currentCart.CartId;
            _carts.Remove(_currentCart);
            _currentCart = null;
            return id;
        }
        public CartExpense ChangeState(int i)
        {
            _currentCart.ChangeState(i);
            return _currentCart.GiveExpense(i);
        }
        public int ChargeCart() => _currentCart.CartId;
        public (int, string, List<CartExpense>) SaveCart()
        {
            if (_currentCart == null) return (-1, null, null);
            var id = _currentCart.CartId;
            var name = _currentCart.GiveName();
            var cartexpenses = new List<CartExpense>();
            for (var i = 0; i < _currentCart.GiveElementC(); i++)
            {
                cartexpenses.Add(_currentCart.GiveExpense(i));
            }
            return (id, name, cartexpenses);
        }

        public void LoadCart(string name, int id, List<CartExpense> expenses)
        {
            var cart = new Cart(name);
            cart.CartId = id;
            for (var i = 0; i < expenses.Count; i++)
            {
                cart.AddExpense(expenses[i]);
            }
            _carts.Add(cart);
        }

        public Cart StartShopping() => _currentCart;

    }
}
