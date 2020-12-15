using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        readonly MenuPage _menuPage;
        readonly HistoryPage _historyPage;
        readonly InsightsPage _insightsPage;
        readonly SchedulerPage _schedulerPage;
        readonly GoalsPage _goalsPage;
        readonly BudgetsPage _budgetsPage;
        readonly CartsPage _cartsPage;
        readonly SchedulerAddPage _schedulerAddPage;
        public MenuPage(MenuPage menuPage)
        {
            _menuPage = menuPage;
            InitializeComponent();

            _historyPage = new HistoryPage(_menuPage);
            NavigationPage.SetHasNavigationBar(_historyPage, false);
           
            _insightsPage = new InsightsPage(_menuPage);
            NavigationPage.SetHasNavigationBar(_insightsPage, false);

            _schedulerPage = new SchedulerPage(_menuPage);
            NavigationPage.SetHasNavigationBar(_schedulerPage, false);

            _schedulerAddPage = new SchedulerAddPage(_menuPage);
            NavigationPage.SetHasNavigationBar(_schedulerAddPage, false);

            _goalsPage = new GoalsPage();
            NavigationPage.SetHasNavigationBar(_goalsPage, false);

            _budgetsPage = new BudgetsPage();
            NavigationPage.SetHasNavigationBar(_budgetsPage, false);

            _cartsPage = new CartsPage(_menuPage);
            NavigationPage.SetHasNavigationBar(_cartsPage, false);
        }

        private void HistoryPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(_historyPage);
        }
        private void InsightsPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(_insightsPage);
        }
        private void SchedulerPage_Clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(_schedulerPage);
        }
        private void GoalsPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(_goalsPage);
        }
        private void BudgetsPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(_budgetsPage);
        }
        private void CartsPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(_cartsPage);
        }
    }
}