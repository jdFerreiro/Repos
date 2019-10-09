using MyZadERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using MyZadERP.Util;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyZadERP.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public MenuPage()
        {
            InitializeComponent();
            this.BindingContext = new Views.Menu.MenuPageViewModel();
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}