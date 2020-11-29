using System;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class MainPage : ContentPage
    {
        private readonly MenuPage _menuPage;
        private readonly Services _services;

        public MainPage(Services services)
        {
            InitializeComponent();
            _menuPage = new MenuPage(_menuPage, services);
            _services = services;
            NavigationPage.SetHasNavigationBar(_menuPage, false);
            
        }

        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(_menuPage);
        }
    }
}
