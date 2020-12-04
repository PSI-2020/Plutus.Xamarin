using System;
using Xamarin.Forms;

namespace Plutus.Xamarin
{
    public partial class EditScheduledPaymentPage : ContentPage
    {
        Services _services;
        public EditScheduledPaymentPage(Services services)
        {
            _services = services;
            InitializeComponent();

        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

    }

    
}
