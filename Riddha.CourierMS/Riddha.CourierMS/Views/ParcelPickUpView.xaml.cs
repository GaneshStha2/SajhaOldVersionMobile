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
    public partial class ParcelPickUpView : ContentPage
    {
        PickUpVm vm;
        bool FullNameValid;
        public ParcelPickUpView()
        {
            InitializeComponent();
            vm = new PickUpVm();
            this.BindingContext = vm;
            if (!string.IsNullOrEmpty(vm.SelectedParcel.TrackingNo))
            {
                SelectParcelLable.Text = "Select Parcel : " + vm.SelectedParcel.TrackingNo;
            }
            else
            {
                SelectParcelLable.Text = "Select Parcel : ";
            }

        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private async void SelectParcelTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DeliveryVm model = new DeliveryVm();

            var result = await DeliveryParcelListPopup.OpenDeliveryParcelPopup(this.Navigation, model, ParcelPopUpType.PickUp);
            var selectedParcel = result.SelectedParcel;

            if (selectedParcel.BookingId > 0)
            {

                vm.PickUp.BookingId = selectedParcel.BookingId;
                vm.PickUp.TrackingNo = selectedParcel.TrackingNo;
                vm.PickUp.Name = selectedParcel.SenderName;
                vm.SelectedParcel = selectedParcel;
                if (!string.IsNullOrEmpty(vm.SelectedParcel.TrackingNo))
                {
                    SelectParcelLable.Text = "Select Parcel : " + vm.SelectedParcel.TrackingNo;
                }
                else
                {
                    SelectParcelLable.Text = "Select Parcel : ";
                }

                this.FullNameEntry.Text = selectedParcel.SenderName;
                this.TrackingNO.Text = selectedParcel.TrackingNo;
                this.ToLocation.Text = selectedParcel.ToLocation;
                this.CustomerName.Text = selectedParcel.CustomerName;
                this.ContactNo.Text = selectedParcel.ContactNo;


                if (FullNameEntry.Text.Length >= 5)
                {
                    FullNameSucessIndicator.IsVisible = true;
                    FullNameValid = true;
                }
                if (FullNameEntry.Text.Length < 5)
                {
                    FullNameSucessIndicator.IsVisible = false;
                    FullNameValid = false;
                }

                SelectedParcelStack.IsVisible = true;
            }
            else
            {
                SelectedParcelStack.IsVisible = false;
            }
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            if (!FullNameValid)
            {
                DependencyService.Get<Toast>().Show("Name is not valid...");
                return;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    ParcelServices _parcelServices = new ParcelServices();

                    PickUpModel model = new PickUpModel()
                    {
                        BookingId = vm.PickUp.BookingId,

                        PickedUpByUserId = InitializeDB.UserInfo.UserId,
                        PickedUpDateTime = DateTime.Now,
                        Name = vm.PickUp.Name,
                        Photo = vm.PickUp.Photo,
                        TrackingNo = vm.PickUp.TrackingNo,
                    };

                    var result = await _parcelServices.PickUpParcel(model);

                    switch (result.Status)
                    {
                        case MobileResultStatus.InvalidToken:
                            break;

                        case MobileResultStatus.Ok:

                            DependencyService.Get<Toast>().Show("Parcel Picked Up Successfully");
                            vm = new PickUpVm();
                            await this.Navigation.PopToRootAsync();
                            break;
                        case MobileResultStatus.ParameterError:
                            break;

                        case MobileResultStatus.DatabaseConnectionError:
                            break;

                        case MobileResultStatus.ProcessError:
                            DependencyService.Get<Toast>().Show(result.Message);
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

                    throw;
                }



            }
            else
            {
                await DisplayAlert(App.NetworkConnectionTitle, App.NetworkConnectionMessage, App.NetworkConnectionButtonText);
            }
        }

        private void FullNameEntry_Focused(object sender, FocusEventArgs e)
        {
            FullNameFrame.BorderColor = Color.FromHex("#7BE495");

        }

        private void FullNameText_Changed(object sender, TextChangedEventArgs e)
        {
            if (FullNameEntry.Text.Length >= 5)
            {
                FullNameSucessIndicator.IsVisible = true;
                FullNameValid = true;
            }
            if (FullNameEntry.Text.Length < 5)
            {
                FullNameSucessIndicator.IsVisible = false;
                FullNameValid = false;
            }
        }

        private void FullNameEntry_UnFocused(object sender, FocusEventArgs e)
        {
            FullNameFrame.BorderColor = Color.LightGray;
        }

        private async void Next_Clicked(object sender, EventArgs e)
        {
            if (!FullNameValid)
            {
                DependencyService.Get<Toast>().Show("Name is not valid...");
                return;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    ParcelServices _parcelServices = new ParcelServices();

                    PickUpModel model = new PickUpModel()
                    {
                        BookingId = vm.PickUp.BookingId,

                        PickedUpByUserId = InitializeDB.UserInfo.UserId,
                        PickedUpDateTime = DateTime.Now,
                        Name = vm.PickUp.Name,
                        Photo = vm.PickUp.Photo,
                        TrackingNo = vm.PickUp.TrackingNo,
                    };

                    var result = await _parcelServices.PickUpParcel(model);

                    switch (result.Status)
                    {
                        case MobileResultStatus.InvalidToken:
                            break;

                        case MobileResultStatus.Ok:

                            DependencyService.Get<Toast>().Show("Parcel Picked Up Successfully");
                            vm = new PickUpVm();
                            await this.Navigation.PopToRootAsync();
                            break;
                        case MobileResultStatus.ParameterError:
                            break;

                        case MobileResultStatus.DatabaseConnectionError:
                            break;

                        case MobileResultStatus.ProcessError:
                            DependencyService.Get<Toast>().Show(result.Message);
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

                    throw;
                }



            }
            else
            {
                await DisplayAlert(App.NetworkConnectionTitle, App.NetworkConnectionMessage, App.NetworkConnectionButtonText);
            }
        }
    }
}