using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class EditScheduledPaymentPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;
        private readonly ScheduledPayment _payment;
        private readonly string _type;
        public EditScheduledPaymentPage(ScheduledPayment payment, string type, PlutusApiClient plutusApi)
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
            newPaymentDay.Date = _payment.Date.ConvertToDate();
            newPaymentFrequency.SelectedItem = _payment.Frequency;

            if (_type == "MonthlyIncome")
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
            var verificationService = new VerificationService();
            var error = verificationService.VerifyData(name: newPaymentName.Text, amount: newPaymentAmount.Text);
            if (error == "")
            {
                await _plutusApiClient.ChangeScheduledPaymentAsync(new ScheduledPayment(newPaymentDay.Date, newPaymentName.Text, double.Parse(newPaymentAmount.Text),
                newPaymentCategory.SelectedItem.ToString(), newPaymentFrequency.SelectedItem.ToString(), _payment.Active), _payment.Id, _type);
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
            await _plutusApiClient.DeleteScheduledPaymentAsync(_payment.Id, _type);
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            await Application.Current.MainPage.Navigation.PopAsync();

        }

    }


}
