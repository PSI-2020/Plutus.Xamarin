using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class CheckGoalPage : ContentPage
    {
        private readonly Goal _goal;
        private readonly PlutusApiClient _plutusApiClient = new PlutusApiClient();
        public CheckGoalPage(Goal goal)
        {
            InitializeComponent();
            _goal = goal;
            LoadContentAsync();
        }
        public async void LoadContentAsync()
        {
            var list = await _plutusApiClient.GetGoalsAsync();
            var id = 0;
            foreach (var i in list)
            {
                if (_goal.Name == i.Name && _goal.Amount == i.Amount && _goal.DueDate == i.DueDate)
                    break;
                id++;
            }
            goalNameLabel.Text = _goal.Name;
            goalAmountLabel.Text = _goal.Amount.ToString("C2");
            goalDueDateLabel.Text = _goal.DueDate.ToShortDateString();
            todaySpendLabel.Text = await _plutusApiClient.GetGoalInsightsAsync(id,"daily");
            monthSpendLabel.Text = await _plutusApiClient.GetGoalInsightsAsync(id, "monthly");
            daysLeftLabel.Text = _goal.CalculateDaysLeft();
        }

        private async void SetMainGoal_Clicked(object sender, System.EventArgs e)
        {
            await _plutusApiClient.SetAsMainGoalAsync(_goal);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void EditGoal_Clicked(object sender, EventArgs e)
        {
            var page = new EditGoalPage(_goal);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
