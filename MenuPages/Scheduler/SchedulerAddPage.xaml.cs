using System;

using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class SchedulerAddPage : ContentPage
    {
        Services _services;
        MenuPage _menuPage;
        public SchedulerAddPage(MenuPage menuPage, Services services)
        {
            _menuPage = menuPage;
            _services = services;
            InitializeComponent();

        }
        private void MyScheduledPayments_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();

        }
        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            var menuPage = new MenuPage(_menuPage, _services);
            NavigationPage.SetHasNavigationBar(menuPage, false);
            Navigation.PushAsync(menuPage);
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            var mainPage = new MainPage(_services);
            NavigationPage.SetHasNavigationBar(mainPage, false);
            Navigation.PushAsync(mainPage);
        }
        private void ScheduledExpense_Clicked(object sender, EventArgs e)
        {
            var page = new AddScheduledPaymentPage(_services);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
        private void ScheduledIncome_Clicked(object sender, EventArgs e)
        {
            var page = new AddScheduledPaymentPage(_services);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
    }
}

