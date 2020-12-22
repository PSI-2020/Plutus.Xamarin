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
    public partial class HistoryPage : ContentPage
    {
        private readonly MenuPage _menuPage;
        private readonly PlutusApiClient _plutusApiClient;

        public HistoryPage(MenuPage menuPage, PlutusApiClient plutusApi)
        {
            _menuPage = menuPage;
            _plutusApiClient = plutusApi;
            InitializeComponent();
        }
    }
}