using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class EditPaymentPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;
        private readonly Payment _payment;
        private readonly DataType _type;
        public EditPaymentPage(Payment payment,DataType type, PlutusApiClient plutusApi)
        {
            _payment = payment;
            _type = type;
            _plutusApiClient = plutusApi;
            InitializeComponent();
            LoadContent();

        }
        private void LoadContent()
        {
            newPaymentCategory.Items.Clear();
            newPaymentName.Text = _payment.Name;
            newPaymentAmount.Text = _payment.Amount.ToString();

            if (_type == DataType.Income)
            {
                newPaymentCategory.Items.Add("Salary");
                newPaymentCategory.Items.Add("Gift");
                newPaymentCategory.Items.Add("Investment");
                newPaymentCategory.Items.Add("Sale");
                newPaymentCategory.Items.Add("Rent");
                newPaymentCategory.Items.Add("Other");
            }
            else
            {
                newPaymentCategory.Items.Add("Groceries");
                newPaymentCategory.Items.Add("Restaurant");
                newPaymentCategory.Items.Add("Clothes");
                newPaymentCategory.Items.Add("Transport");
                newPaymentCategory.Items.Add("Health");
                newPaymentCategory.Items.Add("School");
                newPaymentCategory.Items.Add("Bills");
                newPaymentCategory.Items.Add("Entertainment");
                newPaymentCategory.Items.Add("Other");
            }

            newPaymentCategory.SelectedItem = _payment.Category;
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            Application.Current.MainPage.Navigation.PopAsync();
        }
        private async void ChangeButton_Clicked(object sender, EventArgs e)
        {
            var verificationService = new VerificationService(); //pakeisti
            var error = verificationService.VerifyData(name: newPaymentName.Text, amount: newPaymentAmount.Text);
            if (error == "")
            {
                var newPayment = new Payment
                {
                    Date = DateTime.UtcNow.ConvertToInt(),
                    Name = newPaymentName.Text,
                    Amount = double.Parse(newPaymentAmount.Text),
                    Category = newPaymentCategory.SelectedItem.ToString()
                };
                await _plutusApiClient.EditPaymentAsync(newPayment, _payment.PaymentID);
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Ooops...", error, "OK");
            }

        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            await _plutusApiClient.DeletePaymentAsync(_payment.PaymentID);
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            await Application.Current.MainPage.Navigation.PopAsync();

        }

    
}
}
