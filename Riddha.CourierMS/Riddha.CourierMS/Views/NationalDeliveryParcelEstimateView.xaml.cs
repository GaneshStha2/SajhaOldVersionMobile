using Plugin.Connectivity;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.Model;
using Riddha.CourierMS.Services;
using Riddha.CourierMS.ViewModels;
using Riddha.CourierMS.Views.Popups;
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
    public partial class NationalDeliveryParcelEstimateView : ContentPage
    {

        BookingVm vm;
        ParcelTypeViewModel selectedParcelType;
        public NationalDeliveryParcelEstimateView(BookingVm model)
        {
            InitializeComponent();

            selectedParcelType = new ParcelTypeViewModel();

            vm = model;
            model.Booking.Weight = (decimal)0.5;
            model.Booking.IsSurfaceMode = true;



            this.BindingContext = vm;


        }

        protected async override void OnAppearing()
        {

            if (App.IsEstimateOnly == true)
            {
                this.GoToBooking.IsVisible = true;
                this.Next.IsVisible = false;
                this.BookingProgress.IsVisible = false;
            }
            else
            {
                this.BookingProgress.IsVisible = true;
            }
            if (CrossConnectivity.Current.IsConnected)
            {
                vm.getParcelTypeListForDestination(vm.Booking.ToLocationId);

            }
            else
            {
                await DisplayAlert(App.NetworkConnectionTitle, App.NetworkConnectionMessage, App.NetworkConnectionButtonText);
            }

        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            if (App.IsEstimateOnly == false)
            {
                this.Navigation.PushAsync(new ReceiverSelectionView(vm));
            }
            else
            {
                this.Navigation.PushAsync(new DeliveryLocationView(vm));
            }
        }

        private async void NextButton_Clicked(object sender, EventArgs e)
        {
            vm.Booking.ModeType = vm.Booking.IsAirMode == true ? Model.ModeType.Air : Model.ModeType.Surface;
            vm.Booking.IsPackingRequired = this.PackingYes.IsChecked == true ? true : false;
            if (string.IsNullOrEmpty(Weight.Text))
            {
                DependencyService.Get<Toast>().Show("Enter parcel weight...");
                return;
            }

            if (vm.Booking.Weight <= 0)
            {
                DependencyService.Get<Toast>().Show("Parcel weight is zero...");
                return;
            }

            else if (vm.Booking.IsAirMode == false && vm.Booking.IsSurfaceMode == false)
            {
                DependencyService.Get<Toast>().Show("Choose Mode...");
                return;
            }

            else if (this.PackingYes.IsChecked == false && this.NoPacking.IsChecked == false)
            {
                DependencyService.Get<Toast>().Show("Choose if packing required...");
                return;
            }

            else if (vm.Booking.PackageTypeId == 0)
            {
                DependencyService.Get<Toast>().Show("Choose Parcel Type...");
                return;
            }




            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    ParcelServices _parcelServices = new ParcelServices();

                    BookingModel bookingModel = new BookingModel()
                    {
                        ServiceCharge = vm.Booking.ServiceCharge,
                        TotalAmountBeforVat = vm.Booking.TotalAmountBeforVat,
                        CreatedDate = DateTime.Now,
                        CustomerId = InitializeDB.UserInfo.CustomerId,
                        CustomerName = InitializeDB.UserInfo.UserName,
                        FromLocationId = vm.Booking.FromLocationId,
                        FromLocationName = vm.Booking.FromLocationName,
                        GrandTotal = vm.Booking.GrandTotal,
                        ModeType = vm.Booking.ModeType,
                        PackingCharge = vm.Booking.PackingCharge,
                        IsCashOnDelivery = vm.Booking.IsCashOnDelivery,
                        IsPackingRequired = vm.Booking.IsPackingRequired,
                        IsPickUpRequired = true,
                        NoOfPackage = vm.Booking.NoOfPackage,
                        Rate = vm.Booking.Rate,
                        ReceiverContact = vm.Booking.ReceiverContact,
                        ReceiverEmail = vm.Booking.ReceiverEmail,
                        ReceiverId = vm.Booking.ReceiverId,
                        ReceiverName = vm.Booking.ReceiverName,
                        RegdNo = vm.Booking.RegdNo,
                        Id = vm.Booking.Id,
                        Status = vm.Booking.Status,
                        ToLocationId = vm.Booking.ToLocationId,
                        ToLocationName = vm.Booking.ToLocationName,
                        UserId = InitializeDB.UserInfo.UserId,
                        TrackingNo = vm.Booking.TrackingNo,
                        Weight = vm.Booking.Weight,
                        Vat = vm.Booking.Vat,
                        PackageTypeId = vm.Booking.PackageTypeId,
                        PackageTypeName = vm.Booking.PackageTypeName,
                        FromStreetAddress = vm.Booking.FromStreetAddress,
                        ToStreetAddress = vm.Booking.ToStreetAddress,
                        
                    };

                    var result = await _parcelServices.CreateBooking(bookingModel);

                    switch (result.Status)
                    {
                        case MobileResultStatus.InvalidToken:
                            break;
                        case MobileResultStatus.Ok:

                            await this.Navigation.PushAsync(new BookingSuccessfulView(vm));
                            break;
                        case MobileResultStatus.ParameterError:
                            DependencyService.Get<Toast>().Show(result.Message);
                            break;
                        case MobileResultStatus.DatabaseConnectionError:
                            break;
                        case MobileResultStatus.ProcessError:
                            break;
                        case MobileResultStatus.UnhandledException:
                            await DisplayAlert(App.SystemErrorTitle, App.SystemErrorMessage, App.SystemErrorButtonText);
                            break;
                        default:
                            break;
                    }
                }



                catch (Exception)
                {
                    await DisplayAlert(App.ConnectionTimeoutTitle, App.ConnectionTimeoutMessage, App.ConnectionTimeoutButtonText);
                }

            }
            else
            {
                await DisplayAlert(App.NetworkConnectionTitle, App.NetworkConnectionMessage, App.NetworkConnectionButtonText);
            }


        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var radionbtn = sender as RadioButton;


            try
            {
                int commandpram = (int)radionbtn.CommandParameter;

                foreach (var item in vm.ParcelTypeList)
                {
                    item.Checked = false;
                    if (item.Id == commandpram)
                    {
                        item.Checked = true;
                        vm.Booking.PackageTypeId = commandpram;
                        vm.Booking.PackageTypeName = item.Name;
                        selectedParcelType = item;

                    }

                }


                CalculateEstimate();

                //this.ParcelTypeList.BeginRefresh();
                //ParcelTypeList.RefreshCommand = new Command(() =>
                //{
                //    RefreshData();
                //});

                //this.ParcelTypeList.EndRefresh();

            }
            catch (Exception)
            {
            }


        }
        private void RefreshData()
        {

            //ParcelTypeList.ItemsSource = vm.ParcelTypeList;
        }




        public void CalculateEstimate()
        {
            if (!string.IsNullOrEmpty(Weight.Text))
            {

           
            if (vm.Booking.IsAirMode == true)
            {
                if (vm.Booking.Weight > (decimal)0.5)
                {
                    vm.Booking.Rate = selectedParcelType.AirPerKgRate;
                }
                else
                {
                    vm.Booking.Rate = selectedParcelType.MinimumAirRate;
                }
            }

            else
            {
                if (vm.Booking.Weight > (decimal)0.5)
                {
                    vm.Booking.Rate = selectedParcelType.SurfacePerKgRate;
                }
                else
                {
                    vm.Booking.Rate = selectedParcelType.MinimumSurfaceRate;
                }
            }


            if (this.PackingYes.IsChecked == true)
            {
                vm.Booking.PackingCharge = selectedParcelType.PackageCharge;

            }
            else
            {
                vm.Booking.PackingCharge = 0;
            }
            vm.Booking.ServiceCharge = selectedParcelType.ServiceCharge;


            vm.Booking.TotalAmountBeforVat = vm.Booking.Rate + vm.Booking.ServiceCharge + vm.Booking.PackingCharge;

            vm.Booking.Vat = vm.Booking.TotalAmountBeforVat * (decimal)0.13;
            vm.Booking.GrandTotal = vm.Booking.TotalAmountBeforVat + vm.Booking.Vat;

            this.PackingCharge.Text = "Rs " + vm.Booking.PackingCharge;
            this.Rate.Text = "Rs " + Math.Round(vm.Booking.Rate, 2);
            this.GrandTotal.Text = "Rs " + Math.Round(vm.Booking.GrandTotal, 2);
            this.Vat.Text = "Rs " + Math.Round(vm.Booking.Vat, 2);
            this.ServiceCharge.Text = "Rs " + Math.Round(vm.Booking.ServiceCharge, 2);
            }
            else
            {
                DependencyService.Get<Toast>().Show("Please Enter Weight of Parcel...");
            }

        }

        private void Weight_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            if (selectedParcelType.Id > 0)
            {
                CalculateEstimate();
            }
        }

        private void AirMode_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            vm.Booking.IsAirMode = this.AirMode.IsChecked;
            if (selectedParcelType.Id > 0)
            {
                CalculateEstimate();
            }
        }

        private void SurfaceMode_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            vm.Booking.IsSurfaceMode = this.SurfaceMode.IsChecked;
            if (selectedParcelType.Id > 0)
            {
                CalculateEstimate();
            }
        }

        private void PackingYes_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (selectedParcelType.Id > 0)
            {
                CalculateEstimate();
            }
        }

        private void NoPacking_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (selectedParcelType.Id > 0)
            {
                CalculateEstimate();
            }
        }

        private void GoToBooking_Clicked(object sender, EventArgs e)
        {
            App.IsEstimateOnly = false;
            this.Navigation.PushAsync(new BookingView());
        }

        private async void SelectParcelTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DeliveryVm model = new DeliveryVm();

            model.Delivery.DestinationId = vm.Booking.ToLocationId;
            var result = await DeliveryParcelListPopup.OpenDeliveryParcelPopup(this.Navigation, model, ParcelPopUpType.Estimate);
            selectedParcelType = result.SelectedParcelForDestination;

            if (selectedParcelType != null)
            {
                vm.Booking.PackageTypeId = selectedParcelType.Id;
                vm.Booking.PackageTypeName = selectedParcelType.Name;
                this.beforeParcelSelectionGrid.IsVisible = false;
                this.afterParcelSelectionGrid.IsVisible = true;
                this.selectedPackageName.Text = vm.Booking.PackageTypeName;
                CalculateEstimate();
            }

        }
    }
}