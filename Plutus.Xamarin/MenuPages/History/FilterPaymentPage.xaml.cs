﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plutus.WebService;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class FilterPaymentPage : ContentPage
    {
        private readonly HistoryPage _historyPage;
        public FilterPaymentPage(PlutusApiClient plutusApi, int index, HistoryPage historyPage)
        {
            _historyPage = historyPage;
            InitializeComponent();
        }

        private int FilterByName()
        {
            if (name.Text == null)
            {
                _historyPage.HistoryFilters.NameFiter = false;
                _historyPage.HistoryFilters.NameFiterString = "Empty";
                return 0;
            }
            _historyPage.HistoryFilters.NameFiter = true;
            _historyPage.HistoryFilters.NameFiterString = name.Text;
            return 1;
        }

        private int FilterByCategory()
        {
            var selectedECategories = new List<int>();
            var selectedICategories = new List<int>();
            int change;
            if (groceries.IsChecked)
                selectedECategories.Add(1);
            if (bills.IsChecked)
                selectedECategories.Add(2);
            if (restaurant.IsChecked)
                selectedECategories.Add(4);
            if (clothes.IsChecked)
                selectedECategories.Add(8);
            if (health.IsChecked)
                selectedECategories.Add(16);
            if (school.IsChecked)
                selectedECategories.Add(32);
            if (entertainment.IsChecked)
                selectedECategories.Add(64);
            if (other.IsChecked)
                selectedECategories.Add(128);
            if (transport.IsChecked)
                selectedECategories.Add(256);
            if (salary.IsChecked)
                selectedICategories.Add(1);
            if (gift.IsChecked)
                selectedICategories.Add(2);
            if (investment.IsChecked)
                selectedICategories.Add(4);
            if (sale.IsChecked)
                selectedICategories.Add(8);
            if (rent.IsChecked)
                selectedICategories.Add(16);

            if (!selectedECategories.Any())
            {
                change = 0;
                _historyPage.HistoryFilters.ExpFlag = 0;
            }
            else
            {
                var count = selectedECategories.Sum();
                _historyPage.HistoryFilters.ExpFlag = count;
                change = 1;
            }
            if (!selectedICategories.Any())
            {
                change = (change == 1) ? 1 : 0;
                _historyPage.HistoryFilters.IncFlag = 0;
            }
            else
            {
                var count = selectedICategories.Sum();
                _historyPage.HistoryFilters.IncFlag = count;
                change = 1;
            }

            return change;

        }

        private int FilterByAmount()
        {
            
            if (amountFrom.Text == null && amountTo.Text == null)
            {
                _historyPage.HistoryFilters.AmountFilter = 0;
                _historyPage.HistoryFilters.AmountFrom = 0;
                _historyPage.HistoryFilters.AmountTo = 0;
                return 0;
            }
            if(amountFrom.Text==null && amountTo.Text!=null)
            {
                _historyPage.HistoryFilters.AmountFilter = 1;
                _historyPage.HistoryFilters.AmountTo = double.Parse(amountTo.Text);
                _historyPage.HistoryFilters.AmountTo = 0;
                return 1;
            }
            if (amountFrom.Text != null && amountTo.Text == null)
            {
                _historyPage.HistoryFilters.AmountFilter = 2;
                _historyPage.HistoryFilters.AmountFrom = double.Parse(amountFrom.Text);
                _historyPage.HistoryFilters.AmountTo = 0;
                return 1;
            }
            if (amountFrom.Text != null && amountTo.Text != null)
            {
                _historyPage.HistoryFilters.AmountFilter = 3;
                _historyPage.HistoryFilters.AmountFrom = double.Parse(amountFrom.Text);
                _historyPage.HistoryFilters.AmountTo = double.Parse(amountTo.Text);
                return 1;
            }
            return 0;

        }
        private int FilterByDate()
        {
            if (!dateCheckBox.IsChecked)
            {
                _historyPage.HistoryFilters.DateFilter = false;
                _historyPage.HistoryFilters.DateFrom = 0;
                _historyPage.HistoryFilters.DateTo = 0;
                return 0;
            }
            var dateto = dateTo.Date.ConvertToInt();
            var datefrom = dateFrom.Date.ConvertToInt();
            _historyPage.HistoryFilters.DateFilter = true;
            _historyPage.HistoryFilters.DateFrom = datefrom;
            _historyPage.HistoryFilters.DateTo = dateto;
            return 1;
        }


        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        private void ShowButton_Clicked(object sender, EventArgs e)
        {
            var change = 0;
            change += FilterByName();

            change += FilterByCategory();

            change += FilterByAmount();

            change += FilterByDate();

            _historyPage.HistoryFilters.Used = (change == 0) ? false : true;
            _historyPage.CurrentPage = 1;

            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
