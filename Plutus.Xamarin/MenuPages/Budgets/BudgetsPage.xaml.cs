using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetsPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient = new PlutusApiClient();
        public BudgetsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            LoadAllBudgets();
            base.OnAppearing();
        }
        private async void LoadAllBudgets()
        {
            var list = await _plutusApiClient.GetBudgetsListAsync();
            budgetStack.Children.Clear();
            foreach (var budget in list)
            {
                var stack = await LoadBudget(budget);
                budgetStack.Children.Add(stack);
            }
        }

        private async Task<StackLayout> LoadBudget(Budget budget)
        {

            var budgetSum = Convert.ToInt32(budget.Sum);
            var from = budget.From.ConvertToDate();
            var to = budget.To.ConvertToDate();
            var index = int.Parse(budget.Name.Substring(6));
            var spent = await _plutusApiClient.GetBudgetSpentAsync(index);
            var left = await _plutusApiClient.GetBudgetLeftToSpendAsync(index);
            var alreadySpent = Convert.ToInt32(spent);
            var leftToSpend = Convert.ToInt32(left);

            if (alreadySpent > budgetSum)
            {
                leftToSpend = 0;
            }

            var stack = new StackLayout()
            {
                BackgroundColor = Color.FromHex("CEC4B3")
            };
            var budgetCategory = new Label()
            {
                Text = "BUDGET FOR " + budget.Category.ToUpper(),
                FontSize = 20,
                Margin = new Thickness(0, 10, 0, 0),
                TextColor = Color.FromHex("726B60"),
                FontFamily = "LilitaOne",
                HorizontalOptions = LayoutOptions.Center
            };
            var labelFrom = new Label()
            {
                Text = "From: " + from.ToString("yyyy/MM/dd"),
                TextColor = Color.White,
                FontFamily = "LilitaOne",
                HorizontalOptions = LayoutOptions.Center
            };
            var labelTo = new Label()
            {
                Text = "To: " + to.ToString("yyyy/MM/dd"),
                TextColor = Color.White,
                FontFamily = "LilitaOne",
                HorizontalOptions = LayoutOptions.Center
            };
            var chartView = new Microcharts.Forms.ChartView()
            {
                HeightRequest = 150
            };
            chartView.Chart = new DonutChart()
            {
                BackgroundColor = SKColor.Parse("CEC4B3"),
                Entries = new List<ChartEntry>
                {
                  new ChartEntry(alreadySpent)
                  {
                    Color = SKColor.Parse("864F48"),
                   },
                  new ChartEntry(leftToSpend)
                  {
                   Color = SKColor.Parse("8E897E"),
                  }
                }
            };

            var grid = new Grid()
            {
                Margin = new Thickness(40, 10),
                ColumnDefinitions =
                    {
                    new ColumnDefinition(),
                    new ColumnDefinition()
                    }
            };

            var deleteButton = new Button()
            {
                Margin = new Thickness(0, 0, 5, 0),
                Padding = new Thickness(0),
                Text = "Delete",
                FontSize = 10,
                TextColor = Color.White,
                FontFamily = "LilitaOne",
                BackgroundColor = Color.FromHex("979185"),
                HeightRequest = 30
            };

            deleteButton.Clicked += async (s, e) =>
            {
                await _plutusApiClient.DeleteBudgetAsync(index);
                LoadAllBudgets();
            };

            var showDetailsButton = new Button()
            {
                Margin = new Thickness(5, 0, 0, 0),
                Padding = new Thickness(0),
                Text = "Show details",
                FontSize = 10,
                TextColor = Color.White,
                FontFamily = "LilitaOne",
                BackgroundColor = Color.FromHex("979185"),
                HeightRequest = 30
            };
            grid.Children.Add(deleteButton, 0, 0);
            grid.Children.Add(showDetailsButton, 1, 0);

            stack.Children.Add(budgetCategory);
            stack.Children.Add(labelFrom);
            stack.Children.Add(labelTo);
            stack.Children.Add(chartView);
            stack.Children.Add(grid);

            return stack;

        }

        private void AddBudget_Clicked(object sender, EventArgs e)
        {
            var addBudgetPage = new AddBudgetPage();
            NavigationPage.SetHasNavigationBar(addBudgetPage, false);
            Navigation.PushAsync(addBudgetPage);
        }
        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            var page = new MainPage();
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);

        }
    }
}