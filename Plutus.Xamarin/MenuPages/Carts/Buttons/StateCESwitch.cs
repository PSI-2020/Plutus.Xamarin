using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    class StateCESwitch : Switch
    {
        public int elemIndex;
        public StateCESwitch(int i, bool state)
        {
            IsToggled = state;
            elemIndex = i;
            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.Center;
        }
    }
}
