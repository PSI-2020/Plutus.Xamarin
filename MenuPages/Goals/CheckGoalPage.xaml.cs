using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class CheckGoalPage : ContentPage
    {
        MenuPage _menuPage;
        Services _services;
        Goal _goal;
        public CheckGoalPage(Goal goal, MenuPage menuPage, Services services)
        {
            InitializeComponent();
            _menuPage = menuPage;
            _services = services;
            _goal = goal;
            goalNameLabel.Text = _goal.Name;
            goalAmountLabel.Text = _goal.Amount.ToString("C2");
            goalDueDateLabel.Text = _goal.DueDate.ToShortDateString();
            todaySpendLabel.Text = _services.GoalService.Insights(_goal, "daily");
            monthSpendLabel.Text = _services.GoalService.Insights(_goal, "monthly");
            //daysLeftLabel.Text =

        }

        private void SetMainGoal_Clicked(object sender, System.EventArgs e)
        {
        }

        private void EditGoal_Clicked(object sender, EventArgs e)
        {
            var page = new EditGoalPage(_menuPage, _services);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}
