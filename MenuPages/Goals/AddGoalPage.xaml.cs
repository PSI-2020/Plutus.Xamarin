using Xamarin.Forms;
using System;

namespace Plutus.Xamarin
{
    public partial class AddGoalPage : ContentPage
    {
        private readonly Services _services;
        private readonly PlutusApiClient _plutusApiClient = new PlutusApiClient();
        public AddGoalPage(Services services)
        {
            _services = services;
            InitializeComponent();
        }
        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        private async void AddGoal_Clicked(object sender, System.EventArgs e)
        {
            var error = _services.VerificationService.VerifyData(name: newGoalName.Text,amount: newGoalAmount.Text);
            if (error == "")
            {
                var goal = new Goal(newGoalName.Text.UppercaseFirstLetter(), double.Parse(newGoalAmount.Text), newGoalDueDate.Date);
                await _plutusApiClient.PostGoalAsync(goal);
                await DisplayAlert("Success!", "Goal added succesfully", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
               await DisplayAlert("Ooops...", error, "OK");
            }
        }
    }
}
