using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class EditScheduledPaymentPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;
        private readonly int _index;
        private readonly string _type;
        private readonly List<ScheduledPayment> _list;
        public EditScheduledPaymentPage(int index, string type, List<ScheduledPayment> list, PlutusApiClient plutusApi)
        {
            _index = index;
            _type = type;
            _list = list;
            _plutusApiClient = plutusApi;
            InitializeComponent();
            LoadContent();

        }
        private void LoadContent()
        {
            newPaymentCategory.Items.Clear();
            newPaymentName.Text = _list[_index].Name;
            newPaymentAmount.Text = _list[_index].Amount.ToString();
            newPaymentDay.Date = _list[_index].Date.ConvertToDate();
            newPaymentFrequency.SelectedItem = _list[_index].Frequency;

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

            newPaymentCategory.SelectedItem = _list[_index].Category;
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
                var list = await _plutusApiClient.GetAllScheduledPaymentsAsync(_type);
                await _plutusApiClient.ChangeScheduledPaymentAsync(new ScheduledPayment(newPaymentDay.Date, newPaymentName.Text, double.Parse(newPaymentAmount.Text),
                newPaymentCategory.SelectedItem.ToString(), _type + list.Count, newPaymentFrequency.SelectedItem.ToString(), _list[_index].Active), _index, _type);
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
            await _plutusApiClient.DeleteScheduledPaymentAsync(_index, _type);
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            await Application.Current.MainPage.Navigation.PopAsync();

        }

    }


}
