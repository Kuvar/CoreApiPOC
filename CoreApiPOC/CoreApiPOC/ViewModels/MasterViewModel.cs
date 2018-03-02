
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApiPOC.ViewModels
{
    using System.Collections.ObjectModel;
    using Base;
    using Models;  
    using Views;

    public class MasterViewModel : ViewModelBase
    {
        public ObservableCollection<MasterPageItem> MenuItems { get; set; }
        public bool IsAdmin { get; set; }

        public MasterViewModel()
        {
            MenuItems = new ObservableCollection<MasterPageItem>(new[]
            {
                 new MasterPageItem { Id = 0, Title = "Home", TargetType = typeof(LandingView) },
                 new MasterPageItem { Id = 1, Title = "Users" },
                 new MasterPageItem { Id = 2, Title = "Edit User Info", TargetType = typeof(EditUserInfoView)  },
                 new MasterPageItem { Id = 3, Title = "Logout"}
            });

            if (!App.IsAdmin)
                MenuItems.Remove(MenuItems.FirstOrDefault(c => c.Id == 1));
           
        }
    }
}
