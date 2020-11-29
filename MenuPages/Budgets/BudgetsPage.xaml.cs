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
    public partial class BudgetsPage : ContentPage
    {
        private readonly MenuPage _menuPage;
        private readonly Services _services;
        public BudgetsPage(MenuPage menuPage, Services services)
        {
            _menuPage = menuPage;
            _services = services;
            InitializeComponent();
        }

        private void AddBudget_Clicked(object sender, EventArgs e)
        {
            var addBudgetPage = new AddBudgetPage(_services);
            NavigationPage.SetHasNavigationBar(addBudgetPage, false);
            Navigation.PushAsync(addBudgetPage);
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
    }
}