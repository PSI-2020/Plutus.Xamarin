using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulerPage : ContentPage
    {
        private readonly MenuPage _menuPage;
        private readonly Services _services;
        private readonly PlutusApiClient _plutusApiClient = new PlutusApiClient();
        public SchedulerPage(MenuPage menuPage, Services services)
        {
            _menuPage = menuPage;
            _services = services;
            InitializeComponent();
            LoadScheduledPaymentsAsync();

        }
        private async void LoadScheduledPaymentsAsync()
        {
            var income = await _plutusApiClient.GetAllScheduledPaymentsAsync("MonthlyIncome");
            var expense = await _plutusApiClient.GetAllScheduledPaymentsAsync("MonthlyExpenses");

            if (income.Any())
            {
                var allIncome = LoadPayments(income);
                foreach (var item in allIncome)
                    scheduledIncome.Children.Add(item);
            }

            if (expense.Any())
            {
                var allExpenses = LoadPayments(expense);
                foreach (var item in allExpenses)
                    scheduledExpenses.Children.Add(item);
            }

        }
        private List<Grid> LoadPayments(List<ScheduledPayment> payments)
        {
            var allGrids = new List<Grid>();
            foreach (var item in payments)
            {
                var grid = new Grid
                {
                    Padding = new Thickness(2, 2, 15, 2),
                    BackgroundColor = Color.FromHex("#726B60"),
                    ColumnDefinitions =
                    {
                    new ColumnDefinition(),
                    new ColumnDefinition()
                    }
                };

                var stack = new StackLayout
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
                var switchButton = new Switch
                {
                    OnColor = Color.FromHex("#4C8C5E"),
                    ThumbColor = Color.FromHex("#939291"),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.End,
                    IsToggled = item.Active
                };

                stack.Children.Add(nameLabel);
                stack.Children.Add(amountLabel);
                grid.Children.Add(stack, 0, 0);
                grid.Children.Add(switchButton, 1, 0);

                allGrids.Add(grid);
            }
            return allGrids;

        }

        private void AddScheduledPayment_Clicked(object sender, EventArgs e)
        {
            var schedulerAddPage = new SchedulerAddPage(_menuPage, _services);
            NavigationPage.SetHasNavigationBar(schedulerAddPage, false);
            Navigation.PushAsync(schedulerAddPage);
        }
        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            var page = new MainPage(_services);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);

        }
        private void EditScheduledPayment_Clicked(object sender, EventArgs e)
        {
            var editScheduledPaymentPage = new EditScheduledPaymentPage(_services);
            NavigationPage.SetHasNavigationBar(editScheduledPaymentPage, false);
            Navigation.PushAsync(editScheduledPaymentPage);
        }
    }
}