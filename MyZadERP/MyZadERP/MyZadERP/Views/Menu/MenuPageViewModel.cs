using MyZadERP.Util;
using MyZadERP.ViewModels.DTO;
using MyZadERP.Models;
using MyZadERP.Views;
using System.Collections.Generic;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace MyZadERP.Views.Menu
{
    public class MenuPageViewModel : NotifyPropertyChangedBase
    {
        private UserDTO userInfo;
        List<HomeMenuItem> menuItems;


        public MenuPageViewModel()
        {
            userInfo = App.UserInfo;
            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Imputaciones, Title="Mis Imputaciones", Image = IconConstant.Clock },
                new HomeMenuItem {Id = MenuItemType.About, Title="Acerca"  }
            };
        }

        public UserDTO UserInfo
        {
            get => this.userInfo;
            set
            {
                if (this.userInfo == value)
                {
                    return;
                }

                this.userInfo = value;
                OnPropertyChanged();
            }
        }

        public List<HomeMenuItem> MenuItems
        {
            get => this.menuItems;
            set
            {
                if (this.menuItems == value)
                {
                    return;
                }

                this.menuItems = value;
                OnPropertyChanged();
            }
        }
    }
}
