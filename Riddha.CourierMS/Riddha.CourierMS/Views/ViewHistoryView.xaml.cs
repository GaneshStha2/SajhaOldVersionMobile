using Plugin.Connectivity;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Riddha.CourierMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewHistoryView : ContentPage
    {
        DashBoardVm vm;
        public ViewHistoryView(DashBoardVm model)
        {
            this.vm = model;
           
            InitializeComponent();


            this.BindingContext = vm;

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    vm.getRecentTransactionByUserId(InitializeDB.UserInfo.CustomerId);
                    if (vm.RecentTransactionList.Count > 0)
                    {

                        RecentTransactionStack.ItemsSource = vm.RecentTransactionList;
                        NoListFound.IsVisible = false;
                        RecentTransactionStack.IsVisible = true;
                    }
                    else
                    {
                        RecentTransactionStack.IsVisible = false;
                        NoListFound.IsVisible = true;
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert(App.ConnectionTimeoutTitle, App.ConnectionTimeoutMessage, App.ConnectionTimeoutButtonText);
                }

            }
            else
            {
                await DisplayAlert(App.NetworkConnectionTitle, App.NetworkConnectionMessage, App.NetworkConnectionButtonText);
            }
          

        }



        private void TrackButton_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                if (btn.CommandParameter != null)
                {
                    int commandpram = (int)btn.CommandParameter;
                    var trackingdata = vm.RecentTransactionList.Where(x => x.TransactionId == commandpram).FirstOrDefault();

                    App.TrackingNo = trackingdata.TrackingNo;
                    this.Navigation.PushAsync(new TrackingView());

                }
            }
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private void RecentTransactionStack_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selecteditem = e.Item as RecentTransactionViewModel;

            App.TrackingNo = selecteditem.TrackingNo;
            this.Navigation.PushAsync(new TrackingView());
        }
    }
}