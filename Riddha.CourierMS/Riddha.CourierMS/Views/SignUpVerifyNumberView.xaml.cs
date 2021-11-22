using Plugin.Connectivity;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.Services;
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
    public partial class SignUpVerifyNumberView : ContentPage
    {
        CommonServices _commonServices;
        SignupVm vm;
        public SignUpVerifyNumberView(SignupVm model)
        {
            this.vm = model;
            InitializeComponent();
            this.BindingContext = vm;
           
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<IKeyboardHelper>().ShowKeyboard();
            this.OTPOneEntry.Focus();
            this.BindingContext = vm;

        }


        private void BackButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }



        private void OTPOneEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //OTPOne
            if (OTPOneEntry.Text.Length == 1)
            {
                OTPOneFrame.BackgroundColor = Color.FromHex("#68B2A0");
                OTPOneEntry.Text = OTPOneEntry.Text;
                this.OTPTwoEntry.Focus();

            }
            else
            {
                OTPOneFrame.BackgroundColor = Color.White;
            }

        }
        private void OTPTwoEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (OTPTwoEntry.Text.Length == 1)
            {
                OTPTwoFrame.BackgroundColor = Color.FromHex("#68B2A0");
                OTPTwoEntry.Text = OTPTwoEntry.Text;
                this.OTPThreeEntry.Focus();
            }
            else
            {
                OTPTwoFrame.BackgroundColor = Color.White;
            }
        }

        private void OTPThreeEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (OTPThreeEntry.Text.Length == 1)
            {
                OTPThreeFrame.BackgroundColor = Color.FromHex("#68B2A0");
                OTPThreeEntry.Text = OTPThreeEntry.Text;
                this.OTPFourEntry.Focus();
            }
            else
            {
                OTPThreeFrame.BackgroundColor = Color.White;
            }
        }
        private void OTPFourEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var newVal = e.NewTextValue;
            //var oldVal = e.OldTextValue;


            //if(e.key)
            if (OTPFourEntry.Text.Length == 1)
            {
                OTPFourFrame.BackgroundColor = Color.FromHex("#68B2A0");
                OTPFourEntry.Text = OTPOneEntry.Text;
                FurtherProcess();

            }
            else
            {
                OTPFourFrame.BackgroundColor = Color.White;
            }
        }

     

        private async void ResendButton_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                _commonServices = new Riddha.CourierMS.Services.CommonServices();
                try
                {
                    vm.sendOtpCodeforverification(vm.SignUpViewModel.MobileNumber);
                    if (!string.IsNullOrEmpty(vm.SignUpViewModel.OtpCode))
                    {

                        this.Navigation.PushModalAsync(new SignUpVerifyNumberView(vm));
                    }
                    else
                    {
                        await DisplayAlert(App.SystemErrorTitle, App.SystemErrorMessage, App.SystemErrorButtonText);
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



        private void ContinueButton_Clicked(object sender, EventArgs e)
        {
            FurtherProcess();

        }

        private void FurtherProcess()
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();
            if (!string.IsNullOrEmpty(OTPOneEntry.Text) && !string.IsNullOrEmpty(OTPTwoEntry.Text) && !string.IsNullOrEmpty(OTPThreeEntry.Text) && !string.IsNullOrEmpty(OTPFourEntry.Text))
            {
                string EnteredOtpCode = OTPOneEntry.Text + OTPTwoEntry.Text + OTPThreeEntry.Text + OTPFourEntry.Text;
                if (vm.SignUpViewModel.OtpCode == EnteredOtpCode)
                {
                    this.Navigation.PushModalAsync(new SignUploadPhotoAndSuccessView(vm));
                }
                else
                {
                    DependencyService.Get<Toast>().Show("OTP Code not match...");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(OTPOneEntry.Text))
                {
                    DependencyService.Get<Toast>().Show("Please fill first OTP Code...");
                }
                else if (string.IsNullOrEmpty(OTPTwoEntry.Text))
                {
                    DependencyService.Get<Toast>().Show("Please fill second OTP Code...");
                }
                else if (string.IsNullOrEmpty(OTPThreeEntry.Text))
                {
                    DependencyService.Get<Toast>().Show("Please fill third OTP Code...");
                }
                else if (string.IsNullOrEmpty(OTPFourEntry.Text))
                {
                    DependencyService.Get<Toast>().Show("Please fill fourth OTP Code...");
                }
            }
        }
    }
}