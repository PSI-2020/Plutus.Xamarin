using System;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class EditScheduledPaymentPage : ContentPage
    {
        public EditScheduledPaymentPage()
        {
            InitializeComponent();
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

    }

    
}
