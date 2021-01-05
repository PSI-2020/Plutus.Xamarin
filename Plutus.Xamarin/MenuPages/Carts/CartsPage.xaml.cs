using Plutus.Interfaces;
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
    public partial class CartsPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;
        private readonly CartFrontendService _cartService; 
        //private readonly CartService _cartService;

        public CartsPage(PlutusApiClient plutusApi)
        {
            _plutusApiClient = plutusApi;
            _cartService = new CartFrontendService();
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            CreateCartButtonsAsync();
            base.OnAppearing();
        }
        public async void CreateCartButtonsAsync()
        {
            var list = await _plutusApiClient.GetCartNamesAsync();
            var i = 0;
            _cartService.CleanseCarts();
            if (!_cartService.Loaded)
            {
                foreach (var name in list)
                {
                    var listelem = await _plutusApiClient.GetCartExpensesAsync(i);
                    i++;
                    _cartService.LoadCart(name, listelem);
                }
                _cartService.Loaded = true;
            }

            cartsStack.Children.Clear();

            for (i = 0; i < _cartService.GiveCartCount(); i++)
            {
                var item = _cartService.GiveCartNameAt(i);
                var button = new CartButton(item, i);
                button.Clicked += new EventHandler(CartButton_Clicked);
                cartsStack.Children.Add(button);
            }

        }

        private void AddCart_Clicked(object sender, EventArgs e)
        {
            _cartService.NewCart();
            var addCartPage = new AddCartPage(_cartService, _plutusApiClient);
            NavigationPage.SetHasNavigationBar(addCartPage, false);
            Navigation.PushAsync(addCartPage);
        }

        private void MenuButton_Clicked(object sender, EventArgs e) => Application.Current.MainPage.Navigation.PopAsync();

        private void ExitButton_Clicked(object sender, EventArgs e)
        {      
            var page = new MainPage(_plutusApiClient);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
        private void CartButton_Clicked(object sender, EventArgs e)
        {
            var button = (CartButton)sender;
            _cartService.CurrentCartSet(button.cartIndex);
            var page = new SingleCartPage(_cartService, _plutusApiClient, button.cartIndex);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

    }
}