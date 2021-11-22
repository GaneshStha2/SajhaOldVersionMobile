using SQLite;
using System;

namespace Riddha.CourierMS.Common.Model
{
    public class UserInfoDb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public int ServiceCenterId { get; set; }
        public string ProfilePhoto { get; set; }
        public string Token { get; set; }
        public string ServerIp { get; set; }
        public bool IsFirstTime { get; set; }
        public UserType UserType { get; set; }
        public int CustomerId { get; set; }
        public string ServiceCenterName { get; set; }
    }

    public enum UserType
    {
        Customer,
        ShajhaUser

    }
}
