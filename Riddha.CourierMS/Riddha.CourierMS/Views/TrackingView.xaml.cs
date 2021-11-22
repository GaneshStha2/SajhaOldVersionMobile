using Plugin.Connectivity;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.Model;
using Riddha.CourierMS.Services;
using Riddha.CourierMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
namespace Riddha.CourierMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackingView : ContentPage
    {
        TrackingVm vm;

        public TrackingView()
        {
            if (!string.IsNullOrEmpty(App.TrackingNo))
            {
                vm = new TrackingVm();
                InitializeComponent();
                TrackingNoEntryText.Text = App.TrackingNo;
                vm.BackBtnSource = "Back.png";
                
                this.BindingContext = vm;
            }
            else
            {
                vm = new TrackingVm();
                InitializeComponent();
                TrackingNoEntryText.Focus();
                vm.BackBtnSource = "";
                this.BindingContext = vm;
            }
          

        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private async void TrackingNoEntryText_Changed(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 8)
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {

                        vm.GetTimeLineByTrackingNo(e.NewTextValue);

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

                if (vm.TimeLineList.Count > 0)
                {
                    TimeLineViewList.IsVisible = true;
                  

                }
                else
                {
                    await DisplayAlert("Courier TimeLine", "Tracking no not found", "Ok");

                }
            }
        }


    }
}