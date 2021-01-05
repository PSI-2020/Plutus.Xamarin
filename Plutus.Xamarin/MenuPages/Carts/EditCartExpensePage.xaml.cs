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
    public partial class EditCartExpensePage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;
        private readonly CartFrontendService _cartService;
        private readonly int _index;
        public EditCartExpensePage(CartFrontendService cs, PlutusApiClient plutusApi, int index)
        {
            _cartService = cs;
            _plutusApiClient = plutusApi;
            _index = index;
            InitializeComponent();
            LoadCategoryItems();
            var expense = _cartService.GiveCurrentElemAt(_index);
            expenseName.Text = expense.Name;
            expenseAmount.Text = expense.Price.ToString();
            expenseCategory.SelectedItem = expense.Category;
        }
        private void LoadCategoryItems()
        {
            expenseCategory.Items.Clear();
            expenseCategory.Items.Add("Groceries");
            expenseCategory.Items.Add("Restaurant");
            expenseCategory.Items.Add("Clothes");
            expenseCategory.Items.Add("Transport");
            expenseCategory.Items.Add("Health");
            expenseCategory.Items.Add("School");
            expenseCategory.Items.Add("Bills");
            expenseCategory.Items.Add("Entertainment");
            expenseCategory.Items.Add("Other");
        }
        private void ExitButton_Clicked(object sender, EventArgs e) => Application.Current.MainPage.Navigation.PopAsync();

        private async void SaveExpense_Clicked(object sender, EventArgs e)
        {
            var verificationService = new VerificationService(); //pakeisti
            var error = verificationService.VerifyData(name: expenseName.Text, amount: expenseAmount.Text);
            if (error == "")
            {
                _cartService.EditExpense(_index, expenseName.Text, double.Parse(expenseAmount.Text), expenseCategory.SelectedItem.ToString());
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Ooops...", error, "OK");
            }

        }
    }
}