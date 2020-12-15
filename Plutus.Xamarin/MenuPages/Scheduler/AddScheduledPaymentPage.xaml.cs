using Xamarin.Forms;
using System;

namespace Plutus.Xamarin
{
    public partial class AddScheduledPaymentPage : ContentPage
    {
        public AddScheduledPaymentPage()
        {
            InitializeComponent();
        }
        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        private void AddScheduledPayment_Clicked(object sender, EventArgs e)
        {
            var verificationService = new VerificationService(); //pakeisti
            var error = verificationService.VerifyData(name: paymentName.Text,amount: paymentAmount.Text);
            if (error == "")
            {
                //DisplayAlert("Success!", "Goal changed succesfully", "OK");
                Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Ooops...", error, "OK");
            }
            
        }
       
    }
}

