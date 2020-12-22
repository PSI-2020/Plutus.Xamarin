using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InsightsPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;
        public InsightsPage(PlutusApiClient plutusApi)
        {
            _plutusApiClient = plutusApi;
            InitializeComponent();
            Load();

        }
        public async void Load()
        {
            statisticsStack.Children.Add(await LoadExpensePieChart());
            statisticsStack.Children.Add(await LoadExpenseBarChartAsync());
            statisticsStack.Children.Add(await LoadIncomePieChartAsync());
            statisticsStack.Children.Add(await LoadIncomeBarChartAsync());
        }
        private async Task<StackLayout> LoadExpensePieChart()
        {
            var stack = new StackLayout()
            {
                BackgroundColor = Color.FromHex("CEC4B3")
            };
            var expenseLabel = new Label()
            {
                Text = "Expense statistics: ".ToUpper(),
                FontSize = 20,
                Margin = new Thickness(0, 10, 0, 0),
                TextColor = Color.FromHex("726B60"),
                FontFamily = "LilitaOne",
                HorizontalOptions = LayoutOptions.Center
            };
            var chartView = new Microcharts.Forms.ChartView()
            {
                HeightRequest = 200
            };
            chartView.Chart = new PieChart()
            {
                BackgroundColor = SKColor.Parse("CEC4B3"),
                GraphPosition = GraphPosition.AutoFill,
                Entries = await CreateExpenseEntries(),
                LabelTextSize = 25,
                LabelMode = LabelMode.RightOnly
            };
            stack.Children.Add(expenseLabel);
            stack.Children.Add(chartView);
            return stack;

        }
        private async Task<StackLayout> LoadExpenseBarChartAsync()
        {
            var stack = new StackLayout()
            {
                BackgroundColor = Color.FromHex("CEC4B3")
            };
            var expenseLabel = new Label()
            {
                Text = "Total: ".ToUpper() + (await _plutusApiClient.GetAllCategoriesSpent("Expense")).ToString("C2"),
                FontSize = 17,
                Margin = new Thickness(0, 10, 0, 0),
                TextColor = Color.FromHex("726B60"),
                FontFamily = "LilitaOne",
                HorizontalOptions = LayoutOptions.Center
            };
            var chartView = new Microcharts.Forms.ChartView()
            {
                HeightRequest = 200
            };
            chartView.Chart = new BarChart()
            {
                BackgroundColor = SKColor.Parse("CEC4B3"),
                Entries = await CreateExpenseEntries(),
                LabelTextSize = 25
            };
            stack.Children.Add(expenseLabel);
            stack.Children.Add(chartView);
            return stack;

        }
        private async Task<StackLayout> LoadIncomePieChartAsync()
        {
            var stack = new StackLayout()
            {
                BackgroundColor = Color.FromHex("CEC4B3")
            };
            var incomeLabel = new Label()
            {
                Text = "Income statistics: ".ToUpper(),
                FontSize = 20,
                Margin = new Thickness(0, 10, 0, 0),
                TextColor = Color.FromHex("726B60"),
                FontFamily = "LilitaOne",
                HorizontalOptions = LayoutOptions.Center
            };

            var chartView = new Microcharts.Forms.ChartView()
            {
                HeightRequest = 200
            };
            chartView.Chart = new PieChart()
            {
                BackgroundColor = SKColor.Parse("CEC4B3"),
                GraphPosition = GraphPosition.AutoFill,
                Entries = await CreateIncomeEntries(),
                LabelTextSize = 25,
                LabelMode = LabelMode.RightOnly
            };
            stack.Children.Add(incomeLabel);
            stack.Children.Add(chartView);
            return stack;

        }
        private async Task<StackLayout> LoadIncomeBarChartAsync()
        {
            var stack = new StackLayout()
            {
                BackgroundColor = Color.FromHex("CEC4B3")
            };
            var incomeLabel = new Label()
            {
                Text = "Total: ".ToUpper() + (await _plutusApiClient.GetAllCategoriesSpent("Income")).ToString("C2"),
                FontSize = 17,
                Margin = new Thickness(0, 10, 0, 0),
                TextColor = Color.FromHex("726B60"),
                FontFamily = "LilitaOne",
                HorizontalOptions = LayoutOptions.Center
            };

            var chartView = new Microcharts.Forms.ChartView()
            {
                HeightRequest = 200
            };
            chartView.Chart = new BarChart()
            {
                BackgroundColor = SKColor.Parse("CEC4B3"),
                Entries = await CreateIncomeEntries(),
                LabelTextSize = 25,
            };
            stack.Children.Add(incomeLabel);
            stack.Children.Add(chartView);
            return stack;

        }
        private async Task<List<ChartEntry>> CreateExpenseEntries()
        {
            var groceries = await _plutusApiClient.GetCategorySpent("Expense", "Groceries");
            var restaurant = await _plutusApiClient.GetCategorySpent("Expense", "Restaurant");
            var clothes = await _plutusApiClient.GetCategorySpent("Expense", "Clothes");
            var transport = await _plutusApiClient.GetCategorySpent("Expense", "Transport");
            var health = await _plutusApiClient.GetCategorySpent("Expense", "Health");
            var school = await _plutusApiClient.GetCategorySpent("Expense", "School");
            var bills = await _plutusApiClient.GetCategorySpent("Expense", "Bills");
            var entertainment = await _plutusApiClient.GetCategorySpent("Expense", "Entertainment");
            var other = await _plutusApiClient.GetCategorySpent("Expense", "Other");

            var list = new List<ChartEntry>
                {
                  new ChartEntry(Convert.ToInt32(groceries))
                  {
                    Color = SKColor.Parse("864F48"),
                    Label = "Groceries",
                    ValueLabel = groceries.ToString(),
                    ValueLabelColor = SKColor.Parse("864F48"),
                   },
                  new ChartEntry(Convert.ToInt32(restaurant))
                  {
                    Color = SKColor.Parse("413E37"),
                    Label = "Restaurant",
                    ValueLabel = restaurant.ToString(),
                    ValueLabelColor = SKColor.Parse("413E37")
                  },
                  new ChartEntry(Convert.ToInt32(clothes))
                  {
                    Color = SKColor.Parse("756161"),
                    Label = "Clothes",
                    ValueLabel = clothes.ToString(),
                    ValueLabelColor = SKColor.Parse("756161")
                   },
                  new ChartEntry(Convert.ToInt32(transport))
                  {
                   Color = SKColor.Parse("7F3B8F"),
                   Label = "Transport",
                    ValueLabel = transport.ToString(),
                     ValueLabelColor = SKColor.Parse("7F3B8F")
                  },
                  new ChartEntry(Convert.ToInt32(health))
                  {
                    Color = SKColor.Parse("BA8BA1"),
                    Label = "Health",
                    ValueLabel = health.ToString(),
                     ValueLabelColor = SKColor.Parse("BA8BA1")
                   },
                  new ChartEntry(Convert.ToInt32(school))
                  {
                   Color = SKColor.Parse("8F5941"),
                   Label = "School",
                    ValueLabel = school.ToString(),
                     ValueLabelColor = SKColor.Parse("8F5941")
                  },
                  new ChartEntry(Convert.ToInt32(bills))
                  {
                    Color = SKColor.Parse("AC6953"),
                    Label = "Bills",
                    ValueLabel = bills.ToString(),
                     ValueLabelColor = SKColor.Parse("AC6953")
                   },
                  new ChartEntry(Convert.ToInt32(entertainment))
                  {
                   Color = SKColor.Parse("5F2540"),
                   Label = "Entertainment",
                    ValueLabel = entertainment.ToString(),
                     ValueLabelColor = SKColor.Parse("5F2540")
                  },
                  new ChartEntry(Convert.ToInt32(other))
                  {
                   Color = SKColor.Parse("8F4154"),
                   Label = "Other",
                    ValueLabel = other.ToString(),
                     ValueLabelColor = SKColor.Parse("8F4154")
                  }

                };
            return list;

        }
        private async Task<List<ChartEntry>> CreateIncomeEntries()
        {
            var salary = await _plutusApiClient.GetCategorySpent("Income", "Salary");
            var gift = await _plutusApiClient.GetCategorySpent("Income", "Gift");
            var investment = await _plutusApiClient.GetCategorySpent("Income", "Investment");
            var sale = await _plutusApiClient.GetCategorySpent("Income", "Sale");
            var rent = await _plutusApiClient.GetCategorySpent("Income", "Rent");
            var other = await _plutusApiClient.GetCategorySpent("Income", "Other");

            var list = new List<ChartEntry>
                {
                  new ChartEntry(Convert.ToInt32(salary))
                  {
                    Color = SKColor.Parse("165520"),
                    Label = "Salary",
                    ValueLabel = salary.ToString(),
                     ValueLabelColor = SKColor.Parse("165520")
                   },
                  new ChartEntry(Convert.ToInt32(gift))
                  {
                   Color = SKColor.Parse("2A9763"),
                   Label = "Gift",
                    ValueLabel = gift.ToString(),
                     ValueLabelColor = SKColor.Parse("2A9763")
                  },
                  new ChartEntry(Convert.ToInt32(investment))
                  {
                    Color = SKColor.Parse("8BACBA"),
                    Label = "Investment",
                    ValueLabel = investment.ToString(),
                     ValueLabelColor = SKColor.Parse("8BACBA")
                   },
                  new ChartEntry(Convert.ToInt32(sale))
                  {
                   Color = SKColor.Parse("496E7A"),
                   Label = "Sale",
                    ValueLabel = sale.ToString(),
                     ValueLabelColor = SKColor.Parse("496E7A")
                  },
                  new ChartEntry(Convert.ToInt32(rent))
                  {
                    Color = SKColor.Parse("296064"),
                    Label = "Rent",
                    ValueLabel = rent.ToString(),
                     ValueLabelColor = SKColor.Parse("296064")
                   },
                  new ChartEntry(Convert.ToInt32(other))
                  {
                   Color = SKColor.Parse("599F8A"),
                   Label = "Other",
                    ValueLabel = other.ToString(),
                     ValueLabelColor = SKColor.Parse("599F8A")
                  }
                };
            return list;
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