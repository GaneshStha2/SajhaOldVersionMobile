using Riddha.CourierMS.Common.Model;
using System;

namespace Riddha.CourierMS.Model
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceCenterId { get; set; }
        public UserType LoginUserType { get; set; }
        public string ProfilePhoto { get; set; }
        public string ServiceCenterName { get; set; }
    }
    
}
