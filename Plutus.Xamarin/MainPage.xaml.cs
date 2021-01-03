using System;
using System.Linq;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class MainPage : ContentPage
    {
        private readonly MenuPage _menuPage;
        private readonly PlutusApiClient _plutusApiClient;

        public MainPage(PlutusApiClient plutusApi)
        {
            InitializeComponent();
            _plutusApiClient = plutusApi;
            _menuPage = new MenuPage(_menuPage, plutusApi);
            NavigationPage.SetHasNavigationBar(_menuPage, false);
       }
        protected override void OnAppearing()
        {
            LoadContentAsync();
            base.OnAppearing();
        }
        public async void LoadContentAsync()
        {
        var list = await _plutusApiClient.GetGoalsAsync();

        if(!list.Any())
        {
           goalName.Text = "No goal";
           return;

        }
        goalName.Text = "GOAL: " + list[0].Name;
        goalAmountAndDueDate.Text = "save " + list[0].Amount.ToString("C2") + " until " + list[0].DueDate.ToString("yyyy-MM-dd");
        spendTodayLabel.Text = await _plutusApiClient.GetGoalInsightsAsync(0, "daily");
    }

    private void MenuButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(_menuPage);
        }
        private void AddExpense_Clicked(object sender, EventArgs e)
        {
            var page = new AddPaymentPage("Expense", _plutusApiClient);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);

        }
        private void AddIncome_Clicked(object sender, EventArgs e)
        {
            var page = new AddPaymentPage("Income", _plutusApiClient);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);

        }
    }
}
