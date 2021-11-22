using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.Droid.DependencyServices;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TextBoarderlessEntry), typeof(TextBoarderlessEntryRenderer))]
namespace Riddha.CourierMS.Droid.DependencyServices
{
    public class TextBoarderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
#pragma warning disable CS0618 // 'View.SetBackgroundDrawable(Drawable?)' is obsolete: 'deprecated'
                this.Control.SetBackgroundDrawable(gd);
#pragma warning restore CS0618 // 'View.SetBackgroundDrawable(Drawable?)' is obsolete: 'deprecated'
                //this.Control.SetRawInputType(InputTypes.ClassText);
                //Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));
            }
        }
    }
}