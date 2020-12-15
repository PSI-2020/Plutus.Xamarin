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
        public CartsPage(MenuPage menuPage)
        {
            _menuPage = menuPage;
            InitializeComponent();
        }
    }
}