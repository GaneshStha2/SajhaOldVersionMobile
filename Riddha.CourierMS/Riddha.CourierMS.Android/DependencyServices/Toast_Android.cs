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

namespace Riddha.CourierMS.Droid.DependencyServices
{
    public class Toast_Android : Riddha.CourierMS.DependencyServices.Toast
    {
        public Toast_Android()
        {

        }
        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}