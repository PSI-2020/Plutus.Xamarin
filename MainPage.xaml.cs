using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class MainPage : ContentPage
    {
        MenuPage _menuPage;
        Services _services;

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
