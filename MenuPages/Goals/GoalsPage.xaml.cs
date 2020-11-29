using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            CreateGoalButtons();

        }
        public void CreateGoalButtons()
        { 
            //test
            var list = new List<String>
            {
                "Vienas",
                "Du",
                "Trys",
                "Keturi",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10",
                "11",
                "12",
                "13",
                "14",
                "15"
            };
            foreach (var item in list)
            {
                var button = new GoalButton(new Goal(item, 50, DateTime.Now));
                button.Clicked += GoalButton_Clicked;

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
            Console.WriteLine(button.Goal.Name);
            var page = new CheckGoalPage(button.Goal,_menuPage, _services);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);

        }

    }
}