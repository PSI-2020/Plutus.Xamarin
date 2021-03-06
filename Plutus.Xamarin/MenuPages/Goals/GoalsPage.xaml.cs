﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalsPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;

        public GoalsPage(PlutusApiClient plutusApi)
        {
            _plutusApiClient = plutusApi;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            CreateGoalButtonsAsync();
            base.OnAppearing();
        }
        public async void CreateGoalButtonsAsync()
        {
            goalsStack.Children.Clear();

            var goals = await _plutusApiClient.GetGoalsAsync();

            for(var i = goals.Count-1; i>=0; i--)
            {
                var button = new GoalButton(goals[i]);
                button.Clicked += new EventHandler(GoalButton_Clicked);
                if (goals.Count - 1 == i)
                {
                    button.BackgroundColor = Color.FromHex("726B60");
                }
                goalsStack.Children.Add(button);
            }

        }

        private void AddGoal_Clicked(object sender, EventArgs e)
        {
            var addGoalPage = new AddGoalPage(_plutusApiClient);
            NavigationPage.SetHasNavigationBar(addGoalPage, false);
            Navigation.PushAsync(addGoalPage);
        }

        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            var page = new MainPage(_plutusApiClient);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
        private void GoalButton_Clicked(object sender, EventArgs e)
        {
            var button = (GoalButton)sender;
            var page = new CheckGoalPage(button.Goal, _plutusApiClient);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);

        }

    }
}