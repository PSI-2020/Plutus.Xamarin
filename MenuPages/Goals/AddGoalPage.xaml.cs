using Xamarin.Forms;
using System;

namespace Plutus.Xamarin
{
    public partial class AddGoalPage : ContentPage
    {
        Services _services;
        public AddGoalPage(Services services)
        {
            _services = services;
            InitializeComponent();
        }
        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        private void AddGoal_Clicked(object sender, System.EventArgs e)
        {
            var error = _services.VerificationService.VerifyData(name: newGoalName.Text,amount: newGoalAmount.Text);
            if (error == "")
            {
                //DisplayAlert("Success!", "Goal added succesfully", "OK");
                Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Ooops...", error, "OK");
            }
        }
    }
}
