using System;
using System.Collections.Generic;
using System.Text;

namespace SICONDEP.ViewModels
{
   public static class UserLoggedViewModel
   {
        public static string Name { get; set; }
        public static int Id { get; set; }
        public static List<MenuViewModel> Menu { get; set; }
    }
}
