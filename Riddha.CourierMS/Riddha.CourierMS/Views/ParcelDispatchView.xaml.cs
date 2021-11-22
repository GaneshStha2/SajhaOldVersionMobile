using Plugin.Connectivity;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.Model;
using Riddha.CourierMS.Services;
using Riddha.CourierMS.ViewModels;
using Riddha.CourierMS.Views.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ParcelDispatchView : ContentPage
    {
        ParcelServices _parcelServics;
        ParcelDispatchVm vm;
        public ParcelDispatchView()
        {
            vm = new ParcelDispatchVm();
            InitializeComponent();
            this.BindingContext = vm;
            if (!string.IsNullOrEmpty(vm.ParcelDispatch.LocationName))
            {
                SelectBranchLable.Text = "Select Branch : " + vm.ParcelDispatch.LocationName;
            }
            else
            {
                SelectBranchLable.Text = "Select Branch : ";
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

                        Dispatch(e.NewTextValue);
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

                if (vm.DispatchedList.Count > 0)
                {
                    this.DispatchedParcelList.IsVisible = true;

                }
                else
                {
                    this.DispatchedParcelList.IsVisible = false;
                }

            }

        }

        private void MakeVibration()
        {
            try
            {
                // Use default vibration length
                Vibration.Vibrate();

                // Or use specified time
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
            }
        }

        public void RefreshData()
        {

            DispatchedParcelList.ItemsSource = null;
            DispatchedParcelList.ItemsSource = vm.DispatchedList;
        }

        private void QrCodeScanner_Tapped(object sender, EventArgs e)
        {
            Scanner();
        }

        public async void Scanner()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            try
            {



                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        await DisplayAlert("Need Camera permission", "Do you want to turn on the camera permission", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    status = results[Permission.Camera];
                }

                if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    var ScannerPage = new ZXingScannerPage();

                    ScannerPage.HasTorch = true;
                    ScannerPage.IsTorchOn = true;
                    ScannerPage.DefaultOverlayBottomText = "Scan To Pay";

                    ScannerPage.OnScanResult += (result) =>
                    {
                // Parar de escanear
                ScannerPage.IsScanning = false;


                // Alert com o código escaneado
                Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (CrossConnectivity.Current.IsConnected)
                            {
                                try
                                {

                                    Dispatch(result.Text);

                            //bool alreadyExists = vm.DispatchedList.Where(x => x.TrackingNo.Trim() == result.Text).FirstOrDefault() == null ? false : true;

                            //if (alreadyExists == true)
                            //{
                            //    DependencyService.Get<Toast>().Show("Already Dispatched this parcel");
                            //    MakeVibration();
                            //    return;
                            //}

                            //vm.getParcelDispatch(result.Text);
                            //if (vm.ParcelDispatch.BookingId == 0)
                            //{
                            //    DependencyService.Get<Toast>().Show("Parcel not found...");
                            //    MakeVibration();
                            //}
                            //else
                            //{
                            //    DependencyService.Get<Toast>().Show("Parcel Dispatched Sucessfully....");

                            //    vm.DispatchedList.Add(vm.ParcelDispatch);
                            //    var a = vm.DispatchedList;
                            //    this.DispatchedParcelList.BeginRefresh();
                            //    DispatchedParcelList.RefreshCommand = new Command(() =>
                            //    {
                            //        //Do your stuff.    
                            //        RefreshData();
                            //        //listPlatforms.IsRefreshing = false;
                            //    });
                            //    this.DispatchedParcelList.EndRefresh();
                            //    this.TrackingNoEntryText.Text = "";
                            //}


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

                            if (vm.DispatchedList.Count > 0)
                            {
                                this.DispatchedParcelList.IsVisible = true;
                            }
                            else
                            {
                                this.DispatchedParcelList.IsVisible = false;
                            }

                    //vm.getpaercelReceive(result.Text);

                    //Navigation.PopModalAsync();
                    //DisplayAlert("Código escaneado", result.Text, "OK");
                });
                    };

                    await Navigation.PushModalAsync(ScannerPage);



                }
                else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
                {
                    await DisplayAlert("Camera Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<Toast>().Show("Unexcepted, Please Try Again");
            }
        }

        private async void SelectBranchTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            BranchVm model = new BranchVm();
            var result = await SelectBranchPopup.OpenSelectBranchPopup(this.Navigation, model);
            var selectedlocation = result;

            vm.ParcelDispatch.LocationId = selectedlocation.Id;
            vm.ParcelDispatch.LocationName = selectedlocation.Name;
            if (!string.IsNullOrEmpty(vm.ParcelDispatch.LocationName))
            {
                SelectBranchLable.Text = "Select Branch : " + selectedlocation.Name;
            }
            else
            {
                SelectBranchLable.Text = "Select Branch : ";
            }

        }

        private void TrackingNoEntryText_Completed(object sender, EventArgs e)
        {

        }



        private async void Dispatch(string trackingNo)
        {
            bool alreadyExists = vm.DispatchedList.Where(x => x.TrackingNo.Trim() == trackingNo).FirstOrDefault() == null ? false : true;

            if (alreadyExists == true)
            {
                DependencyService.Get<Toast>().Show("Already Dispatched this parcel");

                return;
            }

            bool isOldManifest = false;

            ParcelDispatchModel model = new ParcelDispatchModel()
            {
                TrackingNo = trackingNo,
                DestinationId = vm.ParcelDispatch.LocationId,
                UserId = InitializeDB.UserInfo.UserId,
                ManifestId = vm.ParcelDispatch.ManifestId,
                ServiceCenterId = InitializeDB.UserInfo.ServiceCenterId
            };

            _parcelServics = new ParcelServices();
            if (!model.TrackingNo.Contains("#"))
            {

                if (vm.ParcelDispatch.ManifestId == 0 && vm.ParcelDispatch.LocationId == 0)
                {
                    DependencyService.Get<Toast>().Show("Select Branch...");
                    MakeVibration();
                    return;
                }

                var dispatchResult = await _parcelServics.DispatchParcelByBookingNumber(model);
                switch (dispatchResult.Status)
                {
                    case MobileResultStatus.InvalidToken:
                        break;
                    case MobileResultStatus.Ok:

                        DependencyService.Get<Toast>().Show("Parcel Dispatched successfully.");


                        ParcelReceiveViewModel receiveVm = vm.ParseParcelReceiveModelToParcelReceiveViewModel(dispatchResult.Data);

                        vm.DispatchedList.Add(receiveVm);

                        this.DispatchedParcelList.BeginRefresh();
                        DispatchedParcelList.RefreshCommand = new Command(() =>
                        {
                            RefreshData();
                        });

                        this.DispatchedParcelList.EndRefresh();
                        this.TrackingNoEntryText.Text = "";

                        vm.ParcelDispatch.ManifestId = dispatchResult.Data.ManifestId;
                        isOldManifest = true;


                        break;
                    case MobileResultStatus.ParameterError:
                        break;
                    case MobileResultStatus.DatabaseConnectionError:
                        break;
                    case MobileResultStatus.ProcessError:
                        DependencyService.Get<Toast>().Show("Parcel not found.");
                        MakeVibration();
                        break;
                    case MobileResultStatus.UnhandledException:
                        await DisplayAlert(App.SystemErrorTitle, App.SystemErrorMessage, App.SystemErrorButtonText);
                        break;
                    default:
                        break;
                }

            }

            else
            {
                var dispatchByManifestNoResult = await _parcelServics.DispatchParcelByManifestNumber(model);

                switch (dispatchByManifestNoResult.Status)
                {
                    case MobileResultStatus.InvalidToken:
                        break;
                    case MobileResultStatus.Ok:

                        DependencyService.Get<Toast>().Show("Parcel Dispatched successfully.");

                        foreach (var item in dispatchByManifestNoResult.Data)
                        {
                            var receiveVmData = vm.ParseParcelReceiveModelToParcelReceiveViewModel(item);

                            vm.DispatchedList.Add(receiveVmData);
                        }

                        this.DispatchedParcelList.BeginRefresh();
                        DispatchedParcelList.RefreshCommand = new Command(() =>
                        {
                            RefreshData();
                        });
                        this.DispatchedParcelList.EndRefresh();
                        this.TrackingNoEntryText.Text = "";

                        break;
                    case MobileResultStatus.ParameterError:
                        break;
                    case MobileResultStatus.DatabaseConnectionError:
                        break;
                    case MobileResultStatus.ProcessError:
                        DependencyService.Get<Toast>().Show("Manifest not found.");
                        MakeVibration();
                        break;
                    case MobileResultStatus.UnhandledException:
                        await DisplayAlert(App.SystemErrorTitle, App.SystemErrorMessage, App.SystemErrorButtonText);
                        break;
                    default:
                        break;
                }

            }
        }

    }
}