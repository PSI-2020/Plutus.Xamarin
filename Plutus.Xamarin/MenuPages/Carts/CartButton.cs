using System;
using Xamarin.Forms;


namespace Plutus.Xamarin
{
    class CartButton : Button
    {
        public int cartIndex;
        public CartButton(string name, int i)
        {
            CornerRadius = 15;
            BackgroundColor = Color.FromHex("726B60");
            Text = name;
            TextColor = Color.White;
            FontAttributes = FontAttributes.Bold;
            FontFamily = "LilitaOne";
            FontSize = 20;
            cartIndex = i;
        }
    }
}
