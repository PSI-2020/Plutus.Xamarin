using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    class SExpBut : Button
    {
        public int elemIndex;
        public SExpBut(int i, string text, Color color)
        {
            Text = text;
            FontAttributes = FontAttributes.Bold;
            TextColor = Color.White;
            BackgroundColor = color;
            VerticalOptions = LayoutOptions.Center;
            HeightRequest = 30;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Padding = 0;
            CornerRadius = 20;
            Margin = new Thickness(20, 2, 20, 2);
            elemIndex = i;
        }

    }
}
