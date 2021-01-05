using System.Threading.Tasks;
using Xamarin.Forms;
using System;

namespace Plutus.Xamarin
{
    public partial class EditGoalPage : ContentPage
    {
        private readonly Goal _goal;
        private readonly PlutusApiClient _plutusApiClient;
        public EditGoalPage(Goal goal, PlutusApiClient plutusApi)
        {
            _goal = goal;
            _plutusApiClient = plutusApi;
            InitializeComponent();
            LoadContent();
        }

        private void LoadContent()
        {
            newGoalName.Text = _goal.Name;
            newGoalAmount.Text = _goal.Amount.ToString();
            newGoalDueDate.Date = _goal.DueDate;
        }


        private async void ChangeGoal_Clicked(object sender, EventArgs e)
        {
            var verificationService = new VerificationService(); //pakeisti
            var error = verificationService.VerifyData(name: newGoalName.Text, amount: newGoalAmount.Text);
            if (error == "")
            {
                //var id = await GetId(_goal);
                int id = _goal.Id;
                var newGoal = new Goal(newGoalName.Text, double.Parse(newGoalAmount.Text), newGoalDueDate.Date);
                await _plutusApiClient.EditGoalAsync(id, newGoal);
                await DisplayAlert("Success!", "Goal changed succesfully", "OK");
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
                await Application.Current.MainPage.Navigation.PopAsync();
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
        private async void DeleteGoal_Clicked(object sender, EventArgs e)
        {
            //var id = await GetId(_goal);
            int id = _goal.Id;
            await _plutusApiClient.DeleteGoalAsync(id);
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
