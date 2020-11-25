using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plutus;

namespace Plutus.Xamarin
{
    public partial class App : Application
    {
        public App()
        {
           // PaymentService paymentService = new PaymentService();
            InitializeComponent();
            var greetPage = new GreetPage();
            NavigationPage.SetHasNavigationBar(greetPage, false);
            var navPage = new NavigationPage(greetPage);
            MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
