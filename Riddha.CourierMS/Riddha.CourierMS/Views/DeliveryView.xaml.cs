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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Riddha.CourierMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeliveryView : ContentPage
    {
        DeliveryVm vm;
        bool FullNameValid;

        public DeliveryView()
        {
            InitializeComponent();
            vm = new DeliveryVm();
            this.BindingContext = vm;
            MessagingCenter.Subscribe<byte[]>(this, "ImageSelected", (args) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    //Set the source of the image view with the byte array
                    img.Source = ImageSource.FromStream(() => new MemoryStream((byte[])args));


                    using (MemoryStream m = new MemoryStream())
                    {

                        byte[] imageBytes = (byte[])args;

                        // Convert byte[] to Base64 String
                        vm.Delivery.Photo = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);


                    }
                    CameraImage.IsVisible = false;
                    PhotoFrame.IsVisible = true;
                });
            });
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

                    DeliveryModel model = new DeliveryModel()
                    {
                        BookingId = vm.Delivery.BookingId,
                        DeliveredByUserId = InitializeDB.UserInfo.UserId,
                        DeliveredDateTime = DateTime.Now,
                        Name = vm.Delivery.Name,
                        Photo = vm.Delivery.Photo,
                        TrackingNo = vm.Delivery.TrackingNo
                    };

                    var result = await _parcelServices.DeliverParcel(model);

                    switch (result.Status)
                    {
                        case MobileResultStatus.InvalidToken:
                            break;

                        case MobileResultStatus.Ok:

                            DependencyService.Get<Toast>().Show("Parcel Delivered Successfully");
                            vm = new DeliveryVm();

                            this.Navigation.PopToRootAsync();

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

        private async void SelectParcelTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DeliveryVm model = new DeliveryVm();

            var result = await DeliveryParcelListPopup.OpenDeliveryParcelPopup(this.Navigation, model, ParcelPopUpType.Delivery);
            var selectedParcel = result.SelectedParcel;

            if (selectedParcel.BookingId > 0)
            {

                vm.Delivery.BookingId = selectedParcel.BookingId;
                vm.Delivery.TrackingNo = selectedParcel.TrackingNo;
                vm.Delivery.Name = selectedParcel.CustomerName;
                vm.SelectedParcel = selectedParcel;
                if (!string.IsNullOrEmpty(vm.SelectedParcel.TrackingNo))
                {
                    SelectParcelLable.Text = "Select Parcel : " + vm.SelectedParcel.TrackingNo;
                }
                else
                {
                    SelectParcelLable.Text = "Select Parcel : ";
                }


                this.FullNameEntry.Text = selectedParcel.CustomerName;
                //FullNameValid = true;
                //FullNameSucessIndicator.IsVisible = true;

                this.TrackingNO.Text = selectedParcel.TrackingNo;
                this.ToLocation.Text = selectedParcel.ToLocation;
                this.CustomerName.Text = selectedParcel.CustomerName;
                this.ContactNo.Text = selectedParcel.ContactNo;
                SelectedParcelStack.IsVisible = true;
            }
            else
            {
                SelectedParcelStack.IsVisible = false;
            }
        }

        private async void UploadImage_Clicked(object sender, EventArgs e)
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
                   
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            var fileName = SetImageFileName();
                            DependencyService.Get<ICameraHelper>().LaunchCamera(FileFormatEnum.JPEG, fileName);
                        });
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
        private string SetImageFileName(string fileName = null)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                if (fileName != null)
                    App.ImageIdToSave = fileName;
                else
                    App.ImageIdToSave = App.DefaultImageId;

                return App.ImageIdToSave;
            }
            else
            {
                //To iterate, on iOS, if you want to save images to the devie, set 
                if (fileName != null)
                {
                    App.ImageIdToSave = fileName;
                    return fileName;
                }
                else
                    return null;
            }
        }
    }
}