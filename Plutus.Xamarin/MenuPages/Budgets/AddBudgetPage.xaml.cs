using System;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class AddBudgetPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient = new PlutusApiClient();
        public AddBudgetPage()
        {
            InitializeComponent();
            budgetCategory.SelectedIndex = 0;
        }
        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void AddBudget_Clicked(object sender, EventArgs e)
        {
            var verificationService = new VerificationService();
            var error = verificationService.VerifyData(amount: budgetAmount.Text);
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
