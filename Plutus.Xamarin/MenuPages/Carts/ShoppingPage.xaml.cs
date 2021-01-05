using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plutus;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;
        private readonly CartFrontendService _cartService;
        private readonly int _index;
        private readonly ShoppingFrontEndService _shoppingService;
        public ShoppingPage(CartFrontendService cs, PlutusApiClient ps, int i)
        {
            _plutusApiClient = ps;
            _cartService = cs;
            _index = i;
            _shoppingService = new ShoppingFrontEndService(_cartService.StartShopping());
            
            InitializeComponent();
            
        }
        protected override void OnAppearing()
        {
            cartName.Text = _cartService.GiveCurrentName();
            base.OnAppearing();
            LoadExpenses();
        }

        private void Back_Clicked(object sender, EventArgs e) => Application.Current.MainPage.Navigation.PopAsync();

        private async void ChargeShopping_ClickedAsync(object sender, EventArgs e)
        {
            await _plutusApiClient.PostChargeShoppingAsync(_shoppingService.ChargeShopping());
            await DisplayAlert("Shopping Charged", "Success", "OK");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void LoadExpenses()
        {
            shoppingExpenses.Children.Clear();
            _ = scroll.ScrollToAsync(shoppingExpenses, ScrollToPosition.Start, false);
            var toPList = _shoppingService.GiveExpenses(0);
            var pList = _shoppingService.GiveExpenses(1);
            if (_cartService.GiveCurrentCartElemCount() != 0)
            {
                var toPLabel = GetStateStack("To Pick");
                var pLabel = GetStateStack("Already Picked");
                shoppingExpenses.Children.Add(toPLabel);
                foreach(var expense in toPList)
                {
                    var expBut = GetExpenseBut(expense.Item1, expense.Item2, 0);
                    expBut.Clicked += (s, e) =>
                    {
                        var but = (SExpBut)s;
                        var elemIndex = but.elemIndex;
                        _shoppingService.ElementClicked(elemIndex);
                        LoadExpenses();
                    };
                    shoppingExpenses.Children.Add(expBut);
                }
                shoppingExpenses.Children.Add(pLabel);
                foreach (var expense in pList)
                {
                    var expBut = GetExpenseBut(expense.Item1, expense.Item2, 1);
                    expBut.Clicked += (s, e) =>
                    {
                        var but = (SExpBut)s;
                        var elemIndex = but.elemIndex;
                        _shoppingService.ElementClicked(elemIndex);
                        LoadExpenses();
                    };
                    shoppingExpenses.Children.Add(expBut);
                }
            }
        }

        private Button GetExpenseBut(string name, int index, short state)
        {
            var c = (state == 0) ? Color.FromHex("#726B60") : Color.FromHex("#cccccc");
            var but = new SExpBut(index, name, c);
            return but;
        }
        private StackLayout GetStateStack(string text)
        {
            var stack = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent
            };
            var tLabel = new Label()
            {
                FontSize = 12,
                Text = text,
                TextColor = Color.FromHex("8E897E"),
                FontFamily = "LilitaOne",
                BackgroundColor = Color.Transparent,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center

            };
            var line = new Label()
            {
                Text = "",
                BackgroundColor = Color.FromHex("#726B60"),
                HeightRequest = 3
            };
            stack.Children.Add(tLabel);
            stack.Children.Add(line);
            return stack;
        }

    }
}