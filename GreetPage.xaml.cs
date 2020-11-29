using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GreetPage : ContentPage
    {
        private readonly Services _services;
        public GreetPage(Services services)
        {
            _services = services;
            InitializeComponent();
        }

        private void GetStarted_Clicked(object sender, EventArgs e)
        {
            var mainPage = new MainPage(_services);
            NavigationPage.SetHasNavigationBar(mainPage, false);
            Navigation.PushAsync(mainPage);
        }
    }
}