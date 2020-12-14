using System;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class AddBudgetPage : ContentPage
    {
        private readonly Services _services;
        private readonly PlutusApiClient _plutusApiClient = new PlutusApiClient();
        public AddBudgetPage(Services services)
        {
            _services = services;
            InitializeComponent();
            budgetCategory.SelectedIndex = 0;
        }
        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void AddBudget_Clicked(object sender, EventArgs e)
        {
            var error = _services.VerificationService.VerifyData(amount: budgetAmount.Text);
            if (error == "")
            {
                var list = await _plutusApiClient.GetBudgetsListAsync();
                await _plutusApiClient.PostBudgetAsync(new Budget("budget" + list.Count, budgetCategory.SelectedItem.ToString(), double.Parse(budgetAmount.Text), budgetFrom.Date, budgetTo.Date));
                await DisplayAlert("Success!", "Budget added succesfully", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Ooops...", error, "OK");
            }
        }
    }
}
