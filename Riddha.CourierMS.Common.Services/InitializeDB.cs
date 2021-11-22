using Riddha.CourierMS.Common.Model;
using Riddha.CourierMS.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Riddha.CourierMS.Common.Services
{
    public partial class InitializeDB
    {
        static CourierMSDb database;

        public InitializeDB()
        {
            var dbData = Database.GetUsersInfoListAsync();


        }
        public static CourierMSDb Database
        {
            get
            {
                if (database == null)
                {
                    var dbName = "CourierMSSQLite.db3";
                    var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                    database = new CourierMSDb(Path.Combine(documentsPath, dbName));
                }
                return database;
            }
        }

        public static List<UserInfoDb> GetUserInfo()
        {
            List<UserInfoDb> userInfo = new List<UserInfoDb>();
            userInfo = Database.GetUsersInfoListAsync();
            if (userInfo.Count > 0)
            {

                return userInfo;
            }
            return userInfo;
        }
        public static UserInfoDb UserInfo
        {
            get
            {
                var dbData = GetUserInfo();
                if (dbData.Count > 0)
                {
                    return dbData[0];
                }
                else
                {
                    return new UserInfoDb();
                }

            }


        }

    }
}
