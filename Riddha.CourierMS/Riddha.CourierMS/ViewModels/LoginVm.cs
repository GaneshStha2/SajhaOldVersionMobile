using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
   public class LoginVm
    {
        public LoginViewModel LoginViewModel { get; set; }

        public LoginVm() : base()
        {
            LoginViewModel = new LoginViewModel();

        }
    }
    public class LoginViewModel {

        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
