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
        private readonly PlutusApiClient _plutusApiClient;
        public BudgetsPage(PlutusApiClient plutusApi)
        {
            _plutusApiClient = plutusApi;
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

            var from = budget.From.ConvertToDate();
            var to = budget.To.ConvertToDate();
            var index = int.Parse(budget.Name.Substring(6));
            var spent = await _plutusApiClient.GetBudgetSpentAsync(index);
            var left = await _plutusApiClient.GetBudgetLeftToSpendAsync(index);

            if (spent > budget.Sum)
            {
                left = 0;
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
            var chartGrid = new Grid();
            var chartView = new Microcharts.Forms.ChartView()
            {
                HeightRequest = 150
            };
            chartView.Chart = new DonutChart()
            {
                BackgroundColor = SKColor.Parse("CEC4B3"),
                Entries = new List<ChartEntry>
                {
                  new ChartEntry(Convert.ToInt32(spent))
                  {
                    Color = SKColor.Parse("864F48"),
                   },
                  new ChartEntry(Convert.ToInt32(left))
                  {
                   Color = SKColor.Parse("8E897E"),
                  }
                }
            };
            var chartLabel = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = (spent / budget.Sum * 100).ToString("F0") + "%",
                TextColor = Color.White,
                FontFamily = "LilitaOne",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.Transparent,
                Padding = 0

            };
            chartGrid.Children.Add(chartView, 0, 0);
            chartGrid.Children.Add(chartLabel, 0, 0);
            var spentGrid = new Grid()
            {
                HeightRequest = 20,
                ColumnDefinitions =
                    {
                    new ColumnDefinition(),
                    new ColumnDefinition() { Width = 1 },
                    new ColumnDefinition()
                    }
            };
            var spentLabel = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                Text = "Spent: " + spent.ToString("C2") + " ",
                TextColor = Color.White,
                FontFamily = "LilitaOne",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.Transparent,
                Padding = 0

            };
            var line = new BoxView()
            {
                BackgroundColor = Color.White,
                WidthRequest = 1,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Center
            };
            var leftLabel = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Text = " Left: " + left.ToString("C2"),
                TextColor = Color.White,
                FontFamily = "LilitaOne",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.Transparent,
                Padding = 0

            };
            spentGrid.Children.Add(spentLabel, 0, 0);
            spentGrid.Children.Add(line, 1, 0);
            spentGrid.Children.Add(leftLabel, 2, 0);

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
            showDetailsButton.Clicked += (s, e) =>
            {
                var page = new ShowDetailsBudget(index, _plutusApiClient);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);

            };
            grid.Children.Add(deleteButton, 0, 0);
            grid.Children.Add(showDetailsButton, 1, 0);

            stack.Children.Add(budgetCategory);
            stack.Children.Add(labelFrom);
            stack.Children.Add(labelTo);
            stack.Children.Add(chartGrid);
            stack.Children.Add(spentGrid);
            stack.Children.Add(grid);

            return stack;

        }

        private void AddBudget_Clicked(object sender, EventArgs e)
        {
            var addBudgetPage = new AddBudgetPage(_plutusApiClient);
            NavigationPage.SetHasNavigationBar(addBudgetPage, false);
            Navigation.PushAsync(addBudgetPage);
        }
        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            var page = new MainPage(_plutusApiClient);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);

        }
    }
}