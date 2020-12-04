using System;
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
        MenuPage _menuPage;
        Services _services;

        public GoalsPage(MenuPage menuPage, Services services)
        {
            _menuPage = menuPage;
            _services = services;

            InitializeComponent();
            CreateGoalButtonsAsync();

        }
        public async void CreateGoalButtonsAsync()
        {
            var httpClient = new HttpClient();
            var reponse = await httpClient.GetStringAsync("https://aspnet-ybkkj2yjkwqhk.azurewebsites.net/api/Goals");
            var goals = JsonConvert.DeserializeObject<List<Goal>>(reponse);

            foreach (var item in goals)
            {
                var button = new GoalButton(item);
                button.Clicked += new EventHandler(GoalButton_Clicked);

                goalsStack.Children.Add(button);
            }

        }

        private void AddGoal_Clicked(object sender, EventArgs e)
        {
            var addGoalPage = new AddGoalPage(_services);
            NavigationPage.SetHasNavigationBar(addGoalPage, false);
            Navigation.PushAsync(addGoalPage);
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
        private void GoalButton_Clicked(object sender, EventArgs e)
        {
            var button = (GoalButton)sender;
            var page = new CheckGoalPage(button.Goal, _menuPage, _services);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);

        }

    }
}