using Plugin.Connectivity;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
    public partial class ParcelReceiveView : ContentPage
    {
        ParcelReceiveVm vm;
        ParcelServices _parcelServices;
        public ParcelReceiveView()
        {
            vm = new ParcelReceiveVm();
            InitializeComponent();
            TrackingNoEntryText.Focus();
            this.BindingContext = vm;
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

                        Receive(e.NewTextValue);

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

                if (vm.ParcelReceivedList.Count > 0)
                {
                    this.RecivedParcelList.IsVisible = true;
                }
                else
                {
                    this.RecivedParcelList.IsVisible = false;
                }
            }
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
                                            Receive(result.Text);


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

                                    if (vm.ParcelReceivedList.Count > 0)
                                    {
                                        this.RecivedParcelList.IsVisible = true;
                                    }
                                    else
                                    {
                                        this.RecivedParcelList.IsVisible = false;
                                    }


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


        public void RefreshData()
        {

            RecivedParcelList.ItemsSource = null;
            RecivedParcelList.ItemsSource = vm.ParcelReceivedList;
        }



        private async void Receive(string trackingNo)
        {
            bool alreadyExists = vm.ParcelReceivedList.Where(x => x.TrackingNo.Trim() == trackingNo).FirstOrDefault() == null ? false : true;

            if (alreadyExists == true)
            {
                DependencyService.Get<Toast>().Show("Already received this parcel");
                return;
            }


            ParcelReceiveModel model = new ParcelReceiveModel()
            {
                TrackingNo = trackingNo,
                ReceivedByUserId = InitializeDB.UserInfo.UserId,
                LocationId = InitializeDB.UserInfo.ServiceCenterId
            };

            _parcelServices = new ParcelServices();



            if (!model.TrackingNo.Contains("#"))
            {



                var result = await _parcelServices.ReceiveParcel(model);
                switch (result.Status)
                {
                    case MobileResultStatus.InvalidToken:
                        break;
                    case MobileResultStatus.Ok:
                        //vm.Print();

                        vm.ParcelReceive = new ParcelReceiveViewModel()
                        {
                            BookingId = result.Data.BookingId,
                            ContactNo = result.Data.ContactNo,
                            CustomerName = result.Data.CustomerName,
                            FromLocation = result.Data.FromLocation,
                            LocationId = result.Data.LocationId,
                            LocationName = result.Data.LocationName,
                            ParcelType = result.Data.ParcelType,
                            ToLocation = result.Data.ToLocation,
                            TrackingNo = result.Data.TrackingNo,
                            Id = result.Data.Id
                        };

                        DependencyService.Get<Toast>().Show("Parcel Received Sucessfully....");

                        vm.ParcelReceivedList.Add(vm.ParcelReceive);

                        //this.RecivedParcelList.BeginRefresh();
                        //RecivedParcelList.RefreshCommand = new Command(() =>
                        //{
                        RefreshData();
                        //});

                        //this.RecivedParcelList.EndRefresh();

                        break;
                    case MobileResultStatus.ParameterError:
                        break;
                    case MobileResultStatus.DatabaseConnectionError:
                        break;
                    case MobileResultStatus.ProcessError:
                        DependencyService.Get<Toast>().Show("Parcel not found....");

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
                var dispatchByManifestNoResult = await _parcelServices.ReceiveParcelByManifestNo(model);

                switch (dispatchByManifestNoResult.Status)
                {
                    case MobileResultStatus.InvalidToken:
                        break;
                    case MobileResultStatus.Ok:
                        //vm.Print();
                        DependencyService.Get<Toast>().Show("Parcel Received Sucessfully....");


                        foreach (var item in dispatchByManifestNoResult.Data)
                        {
                            var receiveVmData = vm.ParseToParcelReceiveViewModel(item);

                            vm.ParcelReceivedList.Add(receiveVmData);
                        }

                        //this.RecivedParcelList.BeginRefresh();
                        //RecivedParcelList.RefreshCommand = new Command(() =>
                        //{
                        //    //Do your stuff.    
                        //    RefreshData();
                        //    //listPlatforms.IsRefreshing = false;
                        //});
                        //this.RecivedParcelList.EndRefresh();

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

        private void MakeVibration()
        {
            try
            {
                Vibration.Vibrate();
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException ex)
            {
            }
        }
    }
}