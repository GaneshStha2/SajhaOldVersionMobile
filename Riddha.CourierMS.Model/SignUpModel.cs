using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.Model
{
   public class SignUpModel
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string ProfilePhoto { get; set; }
        public object Address { get; set; }
    }
}
