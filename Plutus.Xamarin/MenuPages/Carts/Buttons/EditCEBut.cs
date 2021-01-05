using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    class EditCEBut : Button
    {
        public int elemIndex;
        public EditCEBut(int i)
        {
            Text = "edit";
            TextTransform = TextTransform.Lowercase;
            FontAttributes = FontAttributes.Bold;
            BackgroundColor = i % 2 == 0 ? Color.FromHex("DCD5C9") : Color.FromHex("CDC7BC");
            TextColor = Color.White;
            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.Center;
            Padding = 0;
            FontSize = 8;
            Margin = new Thickness(5, 5, 2, 0);
            WidthRequest = 60;
            HeightRequest = 20;
            elemIndex = i;
        }
    }
}
