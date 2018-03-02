using CoreApiPOC.Models;
using CoreApiPOC.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApiPOC.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<MasterPageItem> MenuItems { get; set; }

        public MainViewModel()
        {
            MenuItems = new ObservableCollection<MasterPageItem>(new[]
                {
                    new MasterPageItem { Id = 0, Title = "Page 1" },
                    new MasterPageItem { Id = 1, Title = "Page 2" },
                    new MasterPageItem { Id = 2, Title = "Page 3" },
                    new MasterPageItem { Id = 3, Title = "Page 4" },
                    new MasterPageItem { Id = 4, Title = "Page 5" },
                });
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is int)
            {
                var dataParameter = (int)navigationData;

            }

            return base.InitializeAsync(navigationData);
        }
    }
}
