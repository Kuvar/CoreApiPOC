using CoreApiPOC.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApiPOC.ViewModels
{
    public class LogoutViewModel : ViewModelBase
    {
        public LogoutViewModel()
        {
            App.Id = 0;
            App.Name = string.Empty;
            App.IsAdmin = false;
            App.Email = string.Empty;
            App.Token = string.Empty;

            NavigationService.NavigateToAsync<LoginViewModel>();
            NavigationService.RemoveLastFromBackStackAsync();
        }
    }
}
