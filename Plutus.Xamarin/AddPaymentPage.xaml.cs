using Xamarin.Forms;
using System;

namespace Plutus.Xamarin
{
    public partial class AddPaymentPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;
        private readonly string _type;
        public AddPaymentPage(string type, PlutusApiClient plutusApi)
        {
            _type = type;
            _plutusApiClient = plutusApi;
            InitializeComponent();
            LoadCategoryItems();
            paymentCategory.SelectedIndex = 0;
        }
        private void LoadCategoryItems()
        {
            paymentCategory.Items.Clear();

            if (_type == "Income")
            {
                paymentCategory.Items.Add("Salary");
                paymentCategory.Items.Add("Gift");
                paymentCategory.Items.Add("Investment");
                paymentCategory.Items.Add("Sale");
                paymentCategory.Items.Add("Rent");
                paymentCategory.Items.Add("Other");
            }
            else if (_type == "Expense")
            {
                paymentCategory.Items.Add("Groceries");
                paymentCategory.Items.Add("Restaurant");
                paymentCategory.Items.Add("Clothes");
                paymentCategory.Items.Add("Transport");
                paymentCategory.Items.Add("Health");
                paymentCategory.Items.Add("School");
                paymentCategory.Items.Add("Bills");
                paymentCategory.Items.Add("Entertainment");
                paymentCategory.Items.Add("Other");
            }
        }
        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        private async void AddScheduledPayment_Clicked(object sender, EventArgs e)
        {
            var verificationService = new VerificationService(); //pakeisti
            var error = verificationService.VerifyData(name: paymentName.Text, amount: paymentAmount.Text);
            if (error == "")
            {
                await _plutusApiClient.PostPaymentAsync(new Payment(DateTime.Now.ConvertToInt(), paymentName.Text, double.Parse(paymentAmount.Text),
                paymentCategory.SelectedItem.ToString()), _type);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Ooops...", error, "OK");
            }

        }

    }
}

