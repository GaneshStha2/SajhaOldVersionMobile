using Riddha.CourierMS.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Riddha.CourierMS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPageView : Shell
    {
        public TabPageView()
        {
            SetTabBarIsVisible(this, true);
            SetNavBarIsVisible(this, false);
            InitializeComponent();


            var Home = new ShellSection
            {
                Title = "",
                Icon = "TabBarHomeIcon.png",
                Items = {
                    new ShellContent
                    {
                        Title="",
                    ContentTemplate = new DataTemplate(typeof(DashBoardView)),
                    }
                             }
            };


            var search = new ShellSection
            {
                Title = "",
                Icon = "TabBarSearchIcon.png",
                Items = {
                new ShellContent
                {
                       Title = "",
                    ContentTemplate = new DataTemplate(typeof(TrackingView))
                }
            }
            };



            var notification = new ShellSection
            {
                Title = "",
                Icon = "TabBarBellIcon.png",
                Items = {
                new ShellContent
                    {
                       Title = "",
                    ContentTemplate = new DataTemplate(typeof(NotificationView)),
                    }
                             }
            };
            //var profile = new ShellSection
            //{
            //    Title = "",
            //    Icon = "TabBarUserIcon.png",
            //    Items = {
            //    new ShellContent
            //        {
            //           Title = "",
            //        ContentTemplate = new DataTemplate(typeof(DashBoardView)),
            //        }
            //                 }
            //};
            var tabBar = new TabBar();
            tabBar.Items.Add(Home);
            tabBar.Items.Add(search);
            tabBar.Items.Add(notification);
            //tabBar.Items.Add(profile);
            this.Items.Add(tabBar);
            //if (InitializeDB.UserInfo.SenderId == 0)
            //{

            //}
        }



        //this.Navigation.PushAsync(new DashboardView());



    }
}