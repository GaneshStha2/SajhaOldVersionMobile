using Riddha.CourierMS.Common.Model;
using Riddha.CourierMS.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Riddha.CourierMS.App;

namespace Riddha.CourierMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();
            Busy.IsRunning = true;
            DelayActionAsync(2000);
        }

        public async Task DelayActionAsync(int delay)
        {
            await Task.Delay(delay);

            GetData();
        }

        private void GetData()
        {
            try
            {


                App.Current.MainPage = new ParcelDispatchView();


                //Device.SetFlags(new string[] { "RadioButton_Experimental" });
                //InitializeDB initialize = new InitializeDB();
                //var userInfo = InitializeDB.Database.GetUsersInfoListAsync();
                //if (userInfo.Count > 0)
                //{

                //    if (InitializeDB.UserInfo.UserId != 0)
                //    {
                //        App.LoginType = LoginStatus.RecentLogin;
                //        App.Current.MainPage = new TabPageView();



                //    }
                //    else
                //    {
                //        if (!InitializeDB.UserInfo.IsFirstTime)
                //        {

                //            UserInfoDb db = new UserInfoDb()
                //            {
                //                IsFirstTime = true,
                //            };
                //            InitializeDB.Database.SaveUserInfoAsync(db);
                //            App.Current.MainPage = new MainPage();

                //        }
                //        else
                //        {
                //            App.Current.MainPage = new LoginView();
                //        }
                //    }
                //}
                //else
                //{
                //    UserInfoDb db = new UserInfoDb()
                //    {
                //        IsFirstTime = true,
                //    };
                //    InitializeDB.Database.SaveUserInfoAsync(db);
                //    App.Current.MainPage = new MainPage();
                //}

            }

            catch (Exception ex)
            {
                UserInfoDb db = new UserInfoDb()
                {
                    IsFirstTime = true,
                };
                InitializeDB.Database.SaveUserInfoAsync(db);
                App.Current.MainPage = new MainPage();
            }

            Busy.IsRunning = false;
        }
    }
}