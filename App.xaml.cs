using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plutus;
using System.IO;
using System.Xml.Linq;

namespace Plutus.Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            var services = new Services(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            InitializeComponent();
            var greetPage = new GreetPage(services);
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
