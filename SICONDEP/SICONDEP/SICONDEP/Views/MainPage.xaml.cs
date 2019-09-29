﻿using Prism.Navigation;
using Xamarin.Forms;

namespace SICONDEP.Views
{
    public partial class MainPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation => Device.Idiom != TargetIdiom.Phone;
    }
}