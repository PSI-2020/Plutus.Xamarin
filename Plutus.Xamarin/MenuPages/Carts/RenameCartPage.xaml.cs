using Xamarin.Forms;
using System;

namespace Plutus.Xamarin
{
    public partial class RenameCartPage : ContentPage
    {
        private readonly CartFrontendService _cartService;
        private readonly PlutusApiClient _plutusApiClient;
        private readonly int _index;
        public RenameCartPage(CartFrontendService cs, PlutusApiClient pc, int index)
        {
            _cartService = cs;
            _plutusApiClient = pc;
            _index = index;
            InitializeComponent();
            newCartName.Text = _cartService.GiveCurrentName();
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
                var cartinfo = _cartService.SaveCartChanges(_index);
                await _plutusApiClient.PostRenameCartAsync(cartinfo.Item1, cartinfo.Item2);
                await DisplayAlert("Success!", "Cart renamed succesfully", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Ooops...", error, "OK");
            }
        }
    }
}