using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class CheckGoalPage : ContentPage
    {
        private readonly Goal _goal;
        private readonly PlutusApiClient _plutusApiClient;
        public CheckGoalPage(Goal goal, PlutusApiClient plutusApi)
        {
            InitializeComponent();
            _goal = goal;
            _plutusApiClient = plutusApi;
            LoadContentAsync();
        }
        public async void LoadContentAsync()
        {
            var list = await _plutusApiClient.GetGoalsAsync();
            /* var id = 0;
             foreach (var i in list)
             {
                 if (_goal.Name == i.Name && _goal.Amount == i.Amount && _goal.DueDate == i.DueDate)
                     break;
                 id++;
             }*/
            var id = _goal.Id;
            goalNameLabel.Text = _goal.Name;
            goalAmountLabel.Text = _goal.Amount.ToString("C2");
            goalDueDateLabel.Text = _goal.DueDate.ToShortDateString();
            todaySpendLabel.Text = await _plutusApiClient.GetGoalInsightsAsync(id, "daily");
            monthSpendLabel.Text = await _plutusApiClient.GetGoalInsightsAsync(id, "monthly");
            daysLeftLabel.Text = _goal.CalculateDaysLeft();
        }

        private async void SetMainGoal_Clicked(object sender, EventArgs e)
        {
            await _plutusApiClient.SetAsMainGoalAsync(_goal.Id);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void EditGoal_Clicked(object sender, EventArgs e)
        {
            var page = new EditGoalPage(_goal, _plutusApiClient);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
