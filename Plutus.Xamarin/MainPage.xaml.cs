using System;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class MainPage : ContentPage
    {
        private readonly MenuPage _menuPage;
        private readonly PlutusApiClient _plutusApiClient;

        public MainPage(PlutusApiClient plutusApi)
        {
            InitializeComponent();
            _plutusApiClient = plutusApi;
            _menuPage = new MenuPage(_menuPage, plutusApi);
            NavigationPage.SetHasNavigationBar(_menuPage, false);

        }

        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(_menuPage);
        }
    }
}
