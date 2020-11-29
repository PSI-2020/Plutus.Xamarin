using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulerPage : ContentPage
    {
        MenuPage _menuPage;
        Services _services;
        public SchedulerPage(MenuPage menuPage, Services services)
        {
            _menuPage = menuPage;
            _services = services;
            InitializeComponent();
        }

        private void AddScheduledPayment_Clicked(object sender, EventArgs e)
        {
            var addScheduledPaymentPage = new SchedulerAddPage(_menuPage, _services);
            NavigationPage.SetHasNavigationBar(addScheduledPaymentPage, false);
            Navigation.PushAsync(addScheduledPaymentPage);
        }
        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            var page = new MainPage(_services);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);

        }
    }
}