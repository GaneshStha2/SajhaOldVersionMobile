using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.Droid.DependencyServices;
using Xamarin.Forms;
using Application = Android.App.Application;
[assembly: Dependency(typeof(AppSettingsInterface))]
namespace Riddha.CourierMS.Droid.DependencyServices
{
    public class AppSettingsInterface : IAppSettingsHelper
    {
        public void OpenAppSettings()
        {
            var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
            intent.AddFlags(ActivityFlags.NewTask);
            string package_name = "com.riddha.courierms";
            var uri = Android.Net.Uri.FromParts("package", package_name, null);
            intent.SetData(uri);
            Application.Context.StartActivity(intent);
        }
    }
}