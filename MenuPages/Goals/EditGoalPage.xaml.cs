using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class EditGoalPage : ContentPage
    {
        private readonly MenuPage _menuPage;
        private readonly Services _services;
        private readonly Goal _goal;
        private readonly PlutusApiClient _plutusApiClient = new PlutusApiClient();
        public EditGoalPage(Goal goal, MenuPage menuPage,Services services)
        {
            _menuPage = menuPage;
            _services = services;
            _goal = goal;
            InitializeComponent();
            LoadContent();
        }

        private void LoadContent()
        {
            newGoalName.Text = _goal.Name;
            newGoalAmount.Text = _goal.Amount.ToString();
            newGoalDueDate.Date = _goal.DueDate;
        }

        private async void ChangeGoal_Clicked(object sender, System.EventArgs e)
        {
            var error = _services.VerificationService.VerifyData(name: newGoalName.Text, amount: newGoalAmount.Text);
            if (error == "")
            {
                var id = await GetId(_goal);
                var newGoal = new Goal(newGoalName.Text, double.Parse(newGoalAmount.Text), newGoalDueDate.Date);
                await _plutusApiClient.EditGoalAsync(id, newGoal);
                await DisplayAlert("Success!", "Goal changed succesfully", "OK");
                var page = new GoalsPage(_menuPage, _services);
                NavigationPage.SetHasNavigationBar(page, false);
                await Navigation.PushAsync(page);
            }
            else
            {
                await DisplayAlert("Ooops...", error, "OK");
            }
        }
        private async Task<int> GetId(Goal goal)
        {
            var list = await _plutusApiClient.GetGoalsAsync();
            var id = 0;
            foreach (var i in list)
            {
                if (goal.Name == i.Name && goal.Amount == i.Amount && goal.DueDate == i.DueDate)
                    break;
                id++;
            }
            return id;
        }
        private async void DeleteGoal_Clicked(object sender, System.EventArgs e)
        {
            var id = await GetId(_goal);
            await _plutusApiClient.DeleteGoalAsync(id);
            var page = new GoalsPage(_menuPage, _services);
            NavigationPage.SetHasNavigationBar(page, false);
            await Navigation.PushAsync(page);
        }
        private void ExitButton_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
