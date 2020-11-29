using System;
using Xamarin.Forms;

[assembly: ExportFont("LilitaOne.ttf", Alias = "LilitaOne")]
[assembly: ExportFont("Macondo.ttf", Alias = "Macondo")]
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
