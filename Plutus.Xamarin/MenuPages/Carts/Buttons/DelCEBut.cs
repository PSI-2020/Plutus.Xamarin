using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    class DelCEBut : Button
    {
        public int elemIndex;
        public DelCEBut(int i)
        {
            Text = "-";
            FontAttributes = FontAttributes.Bold;
            TextColor = Color.White;
            BackgroundColor = Color.FromHex("#864F48");
            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.Center;
            Padding = 0;
            WidthRequest = 20;
            HeightRequest = 15;
            CornerRadius = 50;
            Margin = new Thickness(5, 7, 2, 0);
            elemIndex = i;
        }

    }
}
