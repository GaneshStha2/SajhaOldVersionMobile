using Riddha.CourierMS.Common.Model;
using SQLite;
using System;
using System.Collections.Generic;

namespace Riddha.CourierMS.DB
{
    
    public class CourierMSDb
    {
        readonly SQLiteConnection database;

        public CourierMSDb(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            database.CreateTable<UserInfoDb>();
        }
        
        #region UserInfo
        public List<UserInfoDb> GetUsersInfoListAsync()
        {
            return database.Query<UserInfoDb>("SELECT * FROM [UserInfoDb]");
        }

        public UserInfoDb GetUserInfoAsync(int id)
        {
            return database.Table<UserInfoDb>().Where(i => i.Id == id).FirstOrDefault();
        }

        public int SaveUserInfoAsync(UserInfoDb item)
        {
            if (item.Id != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }

        public int DeleteUserInfo(UserInfoDb item)
        {
            return database.Delete(item);
        }

        #endregion

    }

}
