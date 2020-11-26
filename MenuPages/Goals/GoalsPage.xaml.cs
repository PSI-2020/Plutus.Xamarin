﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalsPage : ContentPage
    {
        MenuPage _menuPage;
        Services _services;
        public GoalsPage(MenuPage menuPage, Services services)
        {
            _menuPage = menuPage;
            _services = services;
            InitializeComponent();
        }
    }
}