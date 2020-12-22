using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class EditSchedulerPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;

        public EditSchedulerPage(PlutusApiClient plutusApi)
        {
            _plutusApiClient = plutusApi;
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            LoadScheduledPaymentsAsync();
            base.OnAppearing();
        }
        private async void LoadScheduledPaymentsAsync()
        {

            scheduledIncome.Children.Clear();
            scheduledExpenses.Children.Clear();
            var income = await _plutusApiClient.GetAllScheduledPaymentsAsync("MonthlyIncome");
            var expense = await _plutusApiClient.GetAllScheduledPaymentsAsync("MonthlyExpenses");

            if (income.Any())
            {
                var allIncome = LoadPayments(income, "MonthlyIncome");
                foreach (var item in allIncome)
                    scheduledIncome.Children.Add(item);
            }

            if (expense.Any())
            {
                var allExpenses = LoadPayments(expense, "MonthlyExpenses");
                foreach (var item in allExpenses)
                    scheduledExpenses.Children.Add(item);
            }

        }
        private List<Grid> LoadPayments(List<ScheduledPayment> payments, string type)
        {
            var allPayments = new List<Grid>();
            foreach (var item in payments)
            {
                var grid = new Grid
                {
                    Padding = new Thickness(2, 2, 15, 2),
                    BackgroundColor = Color.FromHex("#726B60"),
                    ColumnDefinitions =
                    {
                    new ColumnDefinition() { Width = new GridLength(30)},
                    new ColumnDefinition(),
                    new ColumnDefinition() { Width = new GridLength(30)}
                    }
                };

                var scheduledPayment = new StackLayout
                {
                    Padding = new Thickness(10, 0, 0, 0),
                };

                var nameLabel = new Label
                {
                    FontFamily = "LilitaOne",
                    TextColor = Color.White,
                    HorizontalTextAlignment = TextAlignment.Start,
                    HorizontalOptions = LayoutOptions.Fill,
                    FontSize = 25,
                    FontAttributes = FontAttributes.Bold,
                    Text = item.Name
                };
                var amountLabel = new Label
                {
                    FontFamily = "LilitaOne",
                    TextColor = Color.White,
                    HorizontalTextAlignment = TextAlignment.Start,
                    HorizontalOptions = LayoutOptions.Fill,
                    FontSize = 15,
                    FontAttributes = FontAttributes.Bold,
                    Text = item.Amount.ToString("C2") + " " + item.Frequency
                };
                var editButton = new Button
                {
                    Text = "edit",
                    TextTransform = TextTransform.Lowercase,
                    FontAttributes = FontAttributes.Bold,
                    BackgroundColor = Color.Transparent,
                    TextColor = Color.FromHex("#CEC4B4"),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.End,
                    Padding = 0,
                    WidthRequest = 60,
                    HeightRequest = 20

                };
                editButton.Clicked += (s, e) =>
                {
                    var index = payments.IndexOf(item);
                    var editScheduledPaymentPage = new EditScheduledPaymentPage(index,type,payments,_plutusApiClient);
                    NavigationPage.SetHasNavigationBar(editScheduledPaymentPage, false);
                    Navigation.PushAsync(editScheduledPaymentPage);
                };

                var deleteButton = new Button
                {
                    Text = "-",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.White,
                    BackgroundColor = Color.FromHex("#864F48"),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    Padding = 0,
                    WidthRequest = 20,
                    HeightRequest = 20,
                    CornerRadius = 50,
                    Margin = new Thickness(5, 0, 0, 0)

                };
                deleteButton.Clicked += async (s, e) =>
                {
                   var index = payments.IndexOf(item);
                   await _plutusApiClient.DeleteScheduledPaymentAsync(index, type);
                   LoadScheduledPaymentsAsync();
                };

                scheduledPayment.Children.Add(nameLabel);
                scheduledPayment.Children.Add(amountLabel);
                grid.Children.Add(scheduledPayment, 1, 0);
                grid.Children.Add(editButton, 2, 0);
                grid.Children.Add(deleteButton, 0, 0);
                allPayments.Add(grid);
            }
            
            return allPayments;

        }

        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            var page = new MainPage(_plutusApiClient);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);

        }
        private void DoneButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
