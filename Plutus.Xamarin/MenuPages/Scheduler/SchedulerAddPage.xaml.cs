using System;

using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class SchedulerAddPage : ContentPage
    {
        private readonly MenuPage _menuPage;
        private readonly PlutusApiClient _plutusApiClient;
        public SchedulerAddPage(MenuPage menuPage, PlutusApiClient plutusApi)
        {
            _menuPage = menuPage;
            _plutusApiClient = plutusApi;
            InitializeComponent();

        }
        private void MyScheduledPayments_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();

        }
        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            var menuPage = new MenuPage(_menuPage, _plutusApiClient);
            NavigationPage.SetHasNavigationBar(menuPage, false);
            Navigation.PushAsync(menuPage);
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            var mainPage = new MainPage(_plutusApiClient);
            NavigationPage.SetHasNavigationBar(mainPage, false);
            Navigation.PushAsync(mainPage);
        }
        private void ScheduledExpense_Clicked(object sender, EventArgs e)
        {
            var page = new AddScheduledPaymentPage("MonthlyExpenses", _plutusApiClient);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
        private void ScheduledIncome_Clicked(object sender, EventArgs e)
        {
            var page = new AddScheduledPaymentPage("MonthlyIncome", _plutusApiClient);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
    }
}

