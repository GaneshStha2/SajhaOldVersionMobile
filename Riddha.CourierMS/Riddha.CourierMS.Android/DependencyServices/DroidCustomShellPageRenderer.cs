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
using Riddha.CourierMS.Droid.DependencyServices;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Shell), typeof(DroidCustomShellPageRenderer))]
namespace Riddha.CourierMS.Droid.DependencyServices
{
    class DroidCustomShellPageRenderer : ShellRenderer
    {
        public DroidCustomShellPageRenderer(Context context) : base(context)
        {
        }

        protected override IShellItemRenderer CreateShellItemRenderer(ShellItem shellItem)
        {
            return new DroidCustomShellPageItemRenderer(this);
        }
    }

    public class DroidCustomShellPageItemRenderer : ShellItemRenderer
    {
        public DroidCustomShellPageItemRenderer(IShellContext shellContext) : base(shellContext)
        {
        }


        /// <summary>
        /// Pops to root when the selected tab is pressed.
        /// </summary>
        /// <param name="shellSection"></param>
        protected override void OnTabReselected(ShellSection shellSection)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                await shellSection?.Navigation.PopToRootAsync();
            });
        }
    }
}