using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class ShowDetailsBudget : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;
        private readonly int _index;
        public ShowDetailsBudget(int index, PlutusApiClient plutusApi)
        {
            _index = index;
            _plutusApiClient = plutusApi;
            InitializeComponent();
            LoadDetails();
        }

        private async void LoadDetails()
        {
            budgetData.Children.Clear();

            // var budget = await _plutusApiClient.GetBudgetAsync(_index);
            // budgetName.Text = budget.ToUpper();
            var list = await _plutusApiClient.GetBudgetStatsAsync(_index);
            if (list != null)
            { 
               list.OrderByDescending(x => x.Date.ConvertToDate()).ToList();

            var i = 0;

                foreach (var payment in list)
                {
                    budgetData.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
                    budgetData.Children.Add(PaymentLabel(payment.Date.ConvertToDate().ToString("yyyy-MM-dd"), i), 0, i);
                    budgetData.Children.Add(PaymentLabel(payment.Name, i), 1, i);
                    budgetData.Children.Add(PaymentLabel(payment.Amount.ToString("C2"), i), 2, i);
                    budgetData.Children.Add(PaymentLabel(payment.Category, i), 3, i);

                    i++;
                }

                budgetData.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1) });
                budgetData.Children.Add(new BoxView() { BackgroundColor = Color.FromHex("8D8B86") }, 0, i);
            }
        }

        private Label PaymentLabel(string info, int index)
        {
            var label = new Label()
            {
                FontSize = 12,
                Text = info,
                TextColor = Color.FromHex("8E897E"),
                FontFamily = "LilitaOne",
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center

            };
            if (index % 2 == 0)
            {
                label.BackgroundColor = Color.FromHex("DCD5C9");
            }
            else
            {
                label.BackgroundColor = Color.FromHex("CDC7BC");
            }

            return label;
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }


    }
}
