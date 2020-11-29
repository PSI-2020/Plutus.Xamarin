using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class EditGoalPage : ContentPage
    {
        MenuPage _menuPage;
        Services _services;
        public EditGoalPage(MenuPage menuPage,Services services)
        {
            _menuPage = menuPage;
            _services = services;
            InitializeComponent();
        }

        void ChangeGoal_Clicked(object sender, System.EventArgs e)
        {
            var error = _services.VerificationService.VerifyData(name: newGoalName.Text, amount: newGoalAmount.Text);
            if (error == "")
            {
                //DisplayAlert("Success!", "Goal changed succesfully", "OK");
                var page = new GoalsPage(_menuPage, _services);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
            }
            else
            {
                DisplayAlert("Ooops...", error, "OK");
            }
        }
        void DeleteGoal_Clicked(object sender, System.EventArgs e)
        {
        }
        void ExitButton_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
