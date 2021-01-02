using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plutus.WebService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;

        public List<History> FilteredList { get; set; }

        public HistoryPage(PlutusApiClient plutusApi)
        {
            _plutusApiClient = plutusApi;
            InitializeComponent();
            dataPicker.SelectedItem = "All";
            FilteredList = null;
        }
        protected override void OnAppearing()
        {
            LoadDetailsAsync(dataPicker.SelectedIndex);
            base.OnAppearing();
        }

        private void SelectedItemChanged(object sender, EventArgs e)
        {
            FilteredList = null;
            var index = dataPicker.SelectedIndex;
            LoadDetailsAsync(index);
        }

        private async void LoadDetailsAsync(int index)
        {
            data.Children.Clear();
            data.RowDefinitions.Clear();
            _ = scroll.ScrollToAsync(data, ScrollToPosition.Start, false);

            var list = new List<History>();
            if (FilteredList == null)
            {
                list = await _plutusApiClient.GetHistoryAsync(index);
            }
            else
            {
                list = FilteredList;
            }
            if (list != null)
            {
                var i = 0;

                foreach (var payment in list)
                {
                    data.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
                    data.Children.Add(PaymentLabel(payment.Date.ToString("yyyy-MM-dd"),i), 0, i);
                    data.Children.Add(PaymentLabel(payment.Name, i), 1, i);
                    data.Children.Add(PaymentLabel(payment.Amount.ToString("C2"), i), 2, i);
                    data.Children.Add(PaymentLabel(payment.Category, i), 3, i);
                    data.Children.Add(PaymentLabel(payment.Type, i), 4, i);

                    i++;
                }

                data.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1) });
                data.Children.Add(new BoxView() { BackgroundColor = Color.FromHex("8D8B86") }, 0, i);
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
        private void EditButton_Clicked(object sender, EventArgs e)
        {
            var page = new EditHistoryPage(_plutusApiClient, dataPicker.SelectedIndex);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);

        }
        private void FilterButton_Clicked(object sender, EventArgs e)
        {
            if (dataPicker.SelectedIndex == 0)
            {
                var page = new FilterPaymentPage(_plutusApiClient, dataPicker.SelectedIndex, this);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
            }
            if (dataPicker.SelectedIndex == 1)
            {
                var page = new FilterExpensePage(_plutusApiClient, dataPicker.SelectedIndex, this);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
            }
            if (dataPicker.SelectedIndex == 2)
            {
                var page = new FilterIncomePage(_plutusApiClient, dataPicker.SelectedIndex, this);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
            }
        }


    }
}