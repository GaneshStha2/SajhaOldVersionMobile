        using Plugin.Connectivity;
using Riddha.CourierMS.Common.Model;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Resources.DesignComponent;
using Riddha.CourierMS.ViewModels;
using Riddha.CourierMS.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Riddha.CourierMS
{
    public partial class App : Application
    {
        public static string DefaultImageId = "default_image";
        public static string ImageIdToSave = null;
        public static string TrackingNo = "";
        public static UserType UserType;
        public static LoginStatus LoginType;
        public static OnlineOrOffline NetworkStatus;
        public App()
        {
            //Device.SetFlags(new string[] { "RadioButton_Experimental" });
            InitializeComponent();
            //BookingVm vm = new BookingVm();
            MainPage = new SplashScreen();
        }



        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        public enum LoginStatus
        {
            JustLogin,
            RecentLogin

        }
          public enum OnlineOrOffline
        {
            Online,
            Ofline

        }



        #region System Message
        public static string SystemErrorTitle = "System Error";
        public static string SystemErrorMessage = "Please try again";
        public static string SystemErrorButtonText = "Ok";


        public static string ConnectionTimeoutTitle = "Connection Timeout";
        public static string ConnectionTimeoutMessage = "Please try again later";
        public static string ConnectionTimeoutButtonText = "Ok";


        public static string NetworkConnectionTitle = "Network Problem";
        public static string NetworkConnectionMessage = "Please check internet connection";
        public static string NetworkConnectionButtonText = "Ok";

        public static bool IsEstimateOnly = false;

        #endregion
    }
}
