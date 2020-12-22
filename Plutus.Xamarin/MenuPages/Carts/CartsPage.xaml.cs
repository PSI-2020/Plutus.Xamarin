using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartsPage : ContentPage
    {
        private readonly MenuPage _menuPage;
        private readonly PlutusApiClient _plutusApiClient;
        public CartsPage(MenuPage menuPage, PlutusApiClient plutusApi)
        {
            _menuPage = menuPage;
            _plutusApiClient = plutusApi;
            InitializeComponent();
        }
    }
}