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
    public partial class InsightsPage : ContentPage
    {
        private readonly MenuPage _menuPage;
        public InsightsPage(MenuPage menuPage)
        {
            _menuPage = menuPage;
            InitializeComponent();
        }
    }
}