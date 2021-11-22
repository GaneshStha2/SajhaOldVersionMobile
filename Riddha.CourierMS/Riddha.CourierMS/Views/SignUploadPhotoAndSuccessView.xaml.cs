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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Riddha.CourierMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUploadPhotoAndSuccessView : ContentPage
    {
        SignupVm vm;
        SignUpServices _signupServices;
        public SignUploadPhotoAndSuccessView(SignupVm model)
        {
            this.vm = model;
            InitializeComponent();
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
                        vm.SignUpViewModel.ProfilePhoto = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);


                    }
                    CameraImage.IsVisible = false;
                    PhotoFrame.IsVisible = true;
                });
            });
        }

        private void TurnOnNotification_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IAppSettingsHelper>().OpenAppSettings();
            //var status = await Permissions.RequestAsync<Permissions.Camera>();
            //Device.OpenUri(new Uri("app-settings:"));
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
        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                _signupServices = new SignUpServices();

                SignUpModel model = new SignUpModel()
                {
                    FullName = vm.SignUpViewModel.FullName,
                    EmailAddress = vm.SignUpViewModel.EmailAddress,
                    MobileNumber = vm.SignUpViewModel.MobileNumber,
                    Password = vm.SignUpViewModel.Password,
                    Address = vm.SignUpViewModel.Address,
                    ProfilePhoto = vm.SignUpViewModel.ProfilePhoto
                };

                var result = new ServiceResult<SignUpModel>();
                try
                {
                    result = await _signupServices.RegisterUser(model);
                    switch (result.Status)
                    {
                        case Common.Services.MobileResultStatus.InvalidToken:
                            break;
                        case Common.Services.MobileResultStatus.Ok:
                            DependencyService.Get<Toast>().Show(result.Message);
                            this.Navigation.PushModalAsync(new LoginView());
                            break;
                        case Common.Services.MobileResultStatus.ParameterError:
                            break;
                        case Common.Services.MobileResultStatus.DatabaseConnectionError:
                            break;
                        case Common.Services.MobileResultStatus.ProcessError:
                            DependencyService.Get<Toast>().Show(result.Message);
                            break;
                        case Common.Services.MobileResultStatus.UnhandledException:
                            await DisplayAlert(App.SystemErrorTitle, App.SystemErrorMessage, App.SystemErrorButtonText);
                            break;
                        default:
                            break;
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



    }
}