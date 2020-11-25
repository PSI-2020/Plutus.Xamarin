using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        HistoryPage _historyPage;
        public MenuPage()
        {
            InitializeComponent();
            _historyPage = new HistoryPage();
            NavigationPage.SetHasNavigationBar(_historyPage, false);
        }

        private void HistoryPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(_historyPage);
        }
    }
}