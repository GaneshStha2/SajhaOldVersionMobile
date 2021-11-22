using Plugin.Connectivity;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.Model;
using Riddha.CourierMS.Services;
using Riddha.CourierMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Riddha.CourierMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpView : ContentPage
    {
        CommonServices _commonServices;
        SignupVm vm;
        bool FullNameValid;
        bool EmailAddressValid;
        bool MobileNumberValid;
        bool PasswordValid;

        public SignUpView(SignupVm model)
        {
            this.vm = model;
            InitializeComponent();
            this.BindingContext = vm;
            FullNameValid = false;
            EmailAddressValid = false;
            MobileNumberValid = false;
            PasswordValid = false;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();
            this.Navigation.PushModalAsync(new MainPage());
        }



        private void FullNameEntry_Focused(object sender, FocusEventArgs e)
        {
            FullNameFrame.BorderColor = Color.FromHex("#7BE495");
        }

        private void FullNameEntry_UnFocused(object sender, FocusEventArgs e)
        {
            FullNameFrame.BorderColor = Color.LightGray;
        }

        private void FullNameText_Changed(object sender, TextChangedEventArgs e)
        {
            if (FullNameEntry.Text.Length == 1)
            {
                FullNameSucessIndicator.IsVisible = true;
                FullNameValid = true;
            }
            if (FullNameEntry.Text.Length ==0)
            {
                FullNameSucessIndicator.IsVisible = false;
                FullNameValid = false;
            }
        }

        private void EmailAddressEntry_Focused(object sender, FocusEventArgs e)
        {
            EmailAddressFrame.BorderColor = Color.FromHex("#7BE495");
        }

        private void EmailAddressText_Changed(object sender, TextChangedEventArgs e)
        {

            try
            {
                MailAddress m = new MailAddress(EmailAddressEntry.Text);
                EmailSucessIndicator.IsVisible = true;
                EmailAddressValid = true;
            }
            catch (Exception ex)
            {
                EmailSucessIndicator.IsVisible = false;
                EmailAddressValid = false;
            }

        }

        private void EmailAddressEntry_UnFocused(object sender, FocusEventArgs e)
        {
            EmailAddressFrame.BorderColor = Color.LightGray;
        }

        private void MobileNumberEntry_UnFocused(object sender, FocusEventArgs e)
        {
            MobileNumberFrame.BorderColor = Color.LightGray;

        }

        private void MobileNumberText_Changed(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 1)
            {
                MobileNumberIndicator.IsVisible = true;
                MobileNumberValid = true;
            }
            else
            {
                MobileNumberIndicator.IsVisible = false;
                MobileNumberValid = false;
            }
        }

        private void MobileNumberEntry_Focused(object sender, FocusEventArgs e)
        {
            MobileNumberFrame.BorderColor = Color.FromHex("#7BE495");
        }
        private void AddressEntry_Focused(object sender, FocusEventArgs e)
        {
            AddressFrame.BorderColor = Color.FromHex("#7BE495");
        }

        private void AddressText_Changed(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 1)
            {
                AddressIndicator.IsVisible = true;
            }
            else
            {
                AddressIndicator.IsVisible = false;
            }
        }

        private void AddressEntry_UnFocused(object sender, FocusEventArgs e)
        {
            AddressFrame.BorderColor = Color.LightGray;
        }
        private void VisiblePasswordButton_Clicked(object sender, EventArgs e)
        {
            if (PasswordEntry.IsPassword == true)
            {
                PasswordEntry.IsPassword = false;
            }
            else
            {
                PasswordEntry.IsPassword = true;
            }
        }

        private void PasswordEntry_Focused(object sender, FocusEventArgs e)
        {
            PasswordFrame.BorderColor = Color.FromHex("#7BE495");
        }

        private void PasswordEntry_UnFocused(object sender, FocusEventArgs e)
        {
            PasswordFrame.BorderColor = Color.LightGray;
        }

        private void PasswordText_Changed(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 1)
            {
                PasswordIndicator.IsVisible = true;
                PasswordValid = true;
            }
            else
            {
                PasswordIndicator.IsVisible = false;
                PasswordValid = false;
            }
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();
            if (FullNameValid && EmailAddressValid && MobileNumberValid && !string.IsNullOrEmpty(vm.SignUpViewModel.Address) && PasswordValid)
            {

                if (CrossConnectivity.Current.IsConnected)
                {
                    _commonServices = new Riddha.CourierMS.Services.CommonServices();
                    try
                    {
                        //vm.sendOtpCodeforverification(vm.SignUpViewModel.MobileNumber);
                        //if (!string.IsNullOrEmpty(vm.SignUpViewModel.OtpCode))
                        //{
                            this.Navigation.PushModalAsync(new SignUploadPhotoAndSuccessView(vm));
                        //}
                        //else
                        //{
                        //    await DisplayAlert(App.SystemErrorTitle, App.SystemErrorMessage, App.SystemErrorButtonText);
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

            }
            else
            {
                if (!FullNameValid)
                {
                    DependencyService.Get<Toast>().Show("Full Name is not valid...");
                }
                else if (!EmailAddressValid)
                {
                    DependencyService.Get<Toast>().Show("Email Address is not valid...");
                }
                else if (!MobileNumberValid)
                {
                    DependencyService.Get<Toast>().Show("Mobile Number is not valid...");
                }
                else if (string.IsNullOrEmpty(vm.SignUpViewModel.Address))
                {
                    DependencyService.Get<Toast>().Show("Address is not valid...");
                }
                else if (!PasswordValid)
                {
                    DependencyService.Get<Toast>().Show("Password is not valid...");
                }
            }
        }

      
    }
}