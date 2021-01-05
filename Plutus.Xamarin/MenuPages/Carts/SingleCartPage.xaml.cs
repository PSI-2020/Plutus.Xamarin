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
    public partial class SingleCartPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;
        private readonly CartFrontendService _cartService;
        private readonly int _index;
        public SingleCartPage(CartFrontendService cs, PlutusApiClient ps, int i)
        {
            _plutusApiClient = ps;
            _cartService = cs;
            _index = i;
            
            InitializeComponent();
            
        }
        protected override void OnAppearing()
        {
            cartName.Text = _cartService.GiveCurrentName();
            base.OnAppearing();
            LoadExpenses();
        }
        protected override bool OnBackButtonPressed()
        {
            var answer = DisplayAlert("Changes Not save", "Please use Back button to save changes", "Ok");
            base.OnBackButtonPressed();
            return false;
        }

        private async void Back_ClickedAsync(object sender, EventArgs e)
        {
            _ = await SaveCartAsync();
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        private async void DeleteCart_ClickedAsync(object sender, EventArgs e)
        {
            var info = _cartService.DeleteCurrent();
            await _plutusApiClient.DeleteCartAsync(info);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        private async void ChargeCart_ClickedAsync(object sender, EventArgs e)
        {
            _ = await SaveCartAsync();
            await _plutusApiClient.PostCartChargeAsync(_cartService.ChargeCart());
            await DisplayAlert("Success!", "Cart charged succesfully", "OK");
        }
        private void NewExpense_Clicked(object sender, EventArgs e)
        {
            var page = new AddCartExpensePage(_cartService, _plutusApiClient);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
        private void Rename_Clicked(object sender, EventArgs e)
        {
            var page = new RenameCartPage(_cartService, _plutusApiClient, _index);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
        private void GoShopping_Clicked(object sender, EventArgs e)
        {
            var page = new ShoppingPage(_cartService, _plutusApiClient, _index);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
        private async Task<int> SaveCartAsync()
        {
            var cartInfo = _cartService.SaveCart();
            if (cartInfo.Item2 == null) return 1;
            await _plutusApiClient.PostCartAsync(cartInfo.Item1, cartInfo.Item2, cartInfo.Item3);
            return 0;
        }
        private void LoadExpenses()
        {
            cartExpenses.Children.Clear();
            cartExpenses.RowDefinitions.Clear();
            var expCount = _cartService.GiveCurrentCartElemCount();
            _ = scroll.ScrollToAsync(cartExpenses, ScrollToPosition.Start, false);
            if (expCount != 0)
            {
                for (var i = 0; i < expCount; i++)
                {
                    var expense = _cartService.GiveCurrentElemAt(i);
                    var deleteButton = new DelCEBut(i);
                    var editButton = new EditCEBut(i);
                    var stateSlider = new StateCESwitch(i, expense.State);
                    deleteButton.Clicked += (s, e) =>
                    {
                        var but = (DelCEBut)s;
                        var elemIndex = but.elemIndex;
                        _cartService.RemoveExpenseCurrentAt(elemIndex);
                        LoadExpenses();
                    };
                    editButton.Clicked += (s, e) =>
                    {
                        var but = (EditCEBut)s;
                        var elemIndex = but.elemIndex;
                        var page = new EditCartExpensePage(_cartService, _plutusApiClient, elemIndex);
                        NavigationPage.SetHasNavigationBar(page, false);
                        Navigation.PushAsync(page);
                    };
                    stateSlider.Toggled += (s, e) =>
                    {
                        var but = (StateCESwitch)s;
                        var elemIndex = but.elemIndex;
                        _cartService.ChangeState(elemIndex);
                    };

                    var delete = GetStack(i);
                    delete.Children.Add(deleteButton);
                    var edit = GetStack(i);
                    edit.Children.Add(editButton);
                    var state = GetStack(i);
                    state.Children.Add(stateSlider);
                    cartExpenses.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
                    cartExpenses.Children.Add(delete, 0, i);
                    cartExpenses.Children.Add(cartExpenseLabel(expense.Name,i), 1, i);
                    cartExpenses.Children.Add(cartExpenseLabel(expense.Price.ToString("C2"), i), 2, i);
                    cartExpenses.Children.Add(cartExpenseLabel(expense.Category.ToString(), i), 3, i);
                    cartExpenses.Children.Add(state, 4, i);
                    cartExpenses.Children.Add(edit, 5, i);
                }
                cartExpenses.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1) });
                cartExpenses.Children.Add(new BoxView() { BackgroundColor = Color.FromHex("8D8B86") }, 0, expCount);
            }
        }
        private StackLayout GetStack(int index)
        {
            var stack = new StackLayout
            {
                BackgroundColor = index % 2 == 0 ? Color.FromHex("DCD5C9") : Color.FromHex("CDC7BC"),
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            return stack;
        }
        private Label cartExpenseLabel(string info, int index)
        {
            var label = new Label()
            {
                FontSize = 12,
                Text = info,
                TextColor = Color.FromHex("8E897E"),
                FontFamily = "LilitaOne",
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center

            };
            label.BackgroundColor = index % 2 == 0 ? Color.FromHex("DCD5C9") : Color.FromHex("CDC7BC");
            return label;
        }
    }
}