using System;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class AddBudgetPage : ContentPage
    {
        private readonly Services _services;
        public AddBudgetPage(Services services)
        {
            _services = services;
            InitializeComponent();
        }
        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void AddBudget_Clicked(object sender, System.EventArgs e)
        {
            var error = _services.VerificationService.VerifyData(amount: budgetAmount.Text);
            if (error == "")
            {
                //DisplayAlert("Success!", "Budget added succesfully", "OK");
                Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Ooops...", error, "OK");
            }
        }
    }
}
