using System;
using System.Collections.Generic;
using System.Text;

namespace MyZadERP.Models
{
    public enum MenuItemType
    {
        Imputaciones,
        About
    }
    public class HomeMenuItem
    {
        public string Image { get; set; }
        public MenuItemType Id { get; set; }
        public string Title { get; set; }
    }
}
