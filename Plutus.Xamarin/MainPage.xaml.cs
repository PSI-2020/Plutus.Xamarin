using System;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class MainPage : ContentPage
    {
        private readonly MenuPage _menuPage;

        public MainPage()
        {
            InitializeComponent();
            _menuPage = new MenuPage(_menuPage);
            NavigationPage.SetHasNavigationBar(_menuPage, false);
            
        }

        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(_menuPage);
        }
    }
}
