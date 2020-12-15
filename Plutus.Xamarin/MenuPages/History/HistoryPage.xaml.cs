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
        public HistoryPage(MenuPage menuPage)
        {
            _menuPage = menuPage;
            InitializeComponent();
        }
    }
}