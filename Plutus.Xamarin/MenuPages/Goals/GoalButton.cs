using System;
using Xamarin.Forms;
namespace Plutus.Xamarin
{
    public class GoalButton : Button
    {
        public Goal Goal;
        public GoalButton(Goal goal)
        {
            CornerRadius = 15;
            BackgroundColor = Color.FromHex("B7B2AA");
            Text = goal.Name;
            TextColor = Color.White;
            FontAttributes = FontAttributes.Bold;
            FontSize = 20;
            Goal = goal;
        }
    }
}
