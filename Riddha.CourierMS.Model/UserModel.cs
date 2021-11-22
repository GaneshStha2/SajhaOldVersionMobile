using Riddha.CourierMS.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.Model
{
   public class UserModel
    {
        public int Id { get; set; }
        public int ServiceCenterId { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string ServiceCenterName { get; set; }
    }
}
