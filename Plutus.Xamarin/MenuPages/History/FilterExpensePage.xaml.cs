using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plutus.WebService;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class FilterExpensePage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;
        private readonly int _index;
        private List<History> _list;
        private List<History> _filteredList = new List<History>();
        private readonly HistoryPage _historyPage;
        public FilterExpensePage(PlutusApiClient plutusApi, int index, HistoryPage historyPage)
        {
            _plutusApiClient = plutusApi;
            _index = index;
            _historyPage = historyPage;
            InitializeComponent();
            GetList();

        }
        private async void GetList()
        {
            _list = await _plutusApiClient.GetHistoryAsync(_index);
        }

        private List<History> FilterByName(List<History> list)
        {
            if (name.Text == null)
                return list;

            return list.Where(x => x.Name.ToLower() == name.Text.ToLower()).ToList();
        }

        private List<History> FilterByCategory(List<History> list)
        {
            var selectedCategories = new List<string>();
            var filteredList = new List<History>();

            if (groceries.IsChecked)
                selectedCategories.Add("Groceries");
            if (bills.IsChecked)
                selectedCategories.Add("Bills");
            if (restaurant.IsChecked)
                selectedCategories.Add("Restaurant");
            if (clothes.IsChecked)
                selectedCategories.Add("Clothes");
            if (health.IsChecked)
                selectedCategories.Add("Health");
            if (school.IsChecked)
                selectedCategories.Add("School");
            if (entertainment.IsChecked)
                selectedCategories.Add("Entertainment");
            if (other.IsChecked)
                selectedCategories.Add("Other");
            if (transport.IsChecked)
                selectedCategories.Add("Transport");

            if (!selectedCategories.Any())
                return list;

            foreach (var payment in list)
            {
                foreach (var category in selectedCategories)
                {
                    if (payment.Category == category)
                    {
                        filteredList.Add(payment);
                    }
                }
            }

            return filteredList;

        }

        private List<History> FilterByAmount(List<History> list)
        {

            if (amountFrom.Text == null && amountTo.Text == null)
            {
                return list;
            }
            if (amountFrom.Text == null && amountTo.Text != null)
            {
                return list.Where(x => x.Amount <= double.Parse(amountTo.Text)).ToList();
            }
            if (amountFrom.Text != null && amountTo.Text == null)
            {
                return list.Where(x => x.Amount >= double.Parse(amountFrom.Text)).ToList();
            }
            if (amountFrom.Text != null && amountTo.Text != null)
            {
                return list.Where(x => x.Amount <= double.Parse(amountTo.Text)).Where(x => x.Amount >= double.Parse(amountFrom.Text)).ToList();
            }

            return null;


        }
        private List<History> FilterByDate(List<History> list)
        {
            var filteredList = new List<History>();

            if (!dateCheckBox.IsChecked)
                return list;

            filteredList = list.Where(x => new DateTime(x.Date.Year, x.Date.Month, x.Date.Day, 0, 0, 0) <= dateTo.Date)
                               .Where(x => new DateTime(x.Date.Year, x.Date.Month, x.Date.Day, 0, 0, 0) >= dateFrom.Date)
                               .ToList();


            return filteredList;
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        private void ShowButton_Clicked(object sender, EventArgs e)
        {
            var filteredList = _list;

            filteredList = FilterByName(filteredList);

            filteredList = FilterByCategory(filteredList);

            filteredList = FilterByAmount(filteredList);

            filteredList = FilterByDate(filteredList);

            _historyPage.FilteredList = filteredList;

            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
