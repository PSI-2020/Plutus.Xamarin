using Xamarin.Forms;
using System;

namespace Plutus.Xamarin
{
    public partial class AddCartPage : ContentPage
    {
        private readonly CartFrontendService _cartService;
        private readonly PlutusApiClient _plutusApiClient;
        public AddCartPage(CartFrontendService cs, PlutusApiClient pc)
        {
            _cartService = cs;
            _plutusApiClient = pc;
            InitializeComponent();
        }
        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        private async void AddCart_Clicked(object sender, EventArgs e)
        {
            var verificationService = new VerificationService(); //pakeisti
            var error = verificationService.VerifyData(name: newCartName.Text);
            if (error == "")
            {
                _cartService.SetCurrentName(newCartName.Text);
                var cartinfo = _cartService.AddCurrentCart();
                await _plutusApiClient.PostCartAsync(cartinfo.Item1, cartinfo.Item2, cartinfo.Item3);
                await DisplayAlert("Success!", "Cart added succesfully", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Ooops...", error, "OK");
            }
        }
    }
}