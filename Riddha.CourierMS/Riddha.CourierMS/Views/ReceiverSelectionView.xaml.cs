using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.ViewModels;
using Riddha.CourierMS.Views.Popups;
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
    public partial class ReceiverSelectionView : ContentPage
    {
        BookingVm vm;
        bool FullNameValid;
        bool EmailAddressValid;
        bool MobileNumberValid;
        public ReceiverSelectionView(BookingVm model)
        {
            InitializeComponent();
            vm = model;
            this.BindingContext = vm;
        }


        private void BackButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new DeliveryLocationView(vm));
        }


        private async void SelectReceiverTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            CustomerVm model = new CustomerVm();

            var result = await SelectCustomerPop.OpenCustomerSelectionPopup(this.Navigation, model);

            var selectedCustomer = result;


            vm.Booking.ReceiverId = selectedCustomer.Id;
            vm.Booking.ReceiverName = selectedCustomer.FullName;
            vm.Booking.ReceiverEmail = selectedCustomer.Email;
            vm.Booking.ReceiverContact = selectedCustomer.Contact;


            this.ReceiverNameEntry.Text = selectedCustomer.FullName;
            this.ReceiverEmailEntry.Text = selectedCustomer.Email;
            this.ReceiverContactEntry.Text = selectedCustomer.Contact;


        }


        #region entry form receiver

        private void ReceiverNameEntry_Focused(object sender, FocusEventArgs e)
        {
            ReceiverNameFrame.BorderColor = Color.FromHex("#7BE495");
        }

        private void FullNameText_Changed(object sender, TextChangedEventArgs e)
        {
            if (ReceiverNameEntry.Text.Length == 5)
            {
                FullNameSucessIndicator.IsVisible = true;

                FullNameValid = true;
            }
            if (ReceiverNameEntry.Text.Length < 5)
            {
                FullNameSucessIndicator.IsVisible = false;
                FullNameValid = false;
            }
        }

        private void ReceiverNameEntry_UnFocused(object sender, FocusEventArgs e)
        {
            ReceiverNameFrame.BorderColor = Color.LightGray;
        }



        private void ReceiverEmailEntry_Focused(object sender, FocusEventArgs e)
        {
            ReceiverEmailFrame.BorderColor = Color.FromHex("#7BE495");
        }

        private void ReceiverEmailEntry_UnFocused(object sender, FocusEventArgs e)
        {
            ReceiverEmailFrame.BorderColor = Color.LightGray;
        }

        private void EmailAddressText_Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                MailAddress m = new MailAddress(ReceiverEmailEntry.Text);
                EmailSucessIndicator.IsVisible = true;
                EmailAddressValid = true;
            }
            catch (Exception ex)
            {
                EmailSucessIndicator.IsVisible = false;
                EmailAddressValid = false;
            }
        }



        #endregion

        private void ReceiverContactEntry_Focused(object sender, FocusEventArgs e)
        {
            ReceiverContactFrame.BorderColor = Color.FromHex("#7BE495");
        }

        private void ReceiverContactText_Changed(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length == 10)
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
        private void ReceiverContactEntry_UnFocused(object sender, FocusEventArgs e)
        {
            ReceiverContactFrame.BorderColor = Color.LightGray;
        }

        private void ReceiverNameText_Changed(object sender, TextChangedEventArgs e)
        {
            if (ReceiverNameEntry.Text.Length >= 5)
            {
                FullNameSucessIndicator.IsVisible = true;
                FullNameValid = true;
            }
            if (ReceiverNameEntry.Text.Length < 5)
            {
                FullNameSucessIndicator.IsVisible = false;
                FullNameValid = false;
            }
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            if (vm.Booking.ReceiverId == 0)
            {
                if (!FullNameValid)
                {
                    DependencyService.Get<Toast>().Show("Receiver Name is not valid...");
                    return;
                }
                else if (!EmailAddressValid)
                {
                    DependencyService.Get<Toast>().Show(" Receiver Email Address is not valid...");
                    return;
                }
                else if (!MobileNumberValid)
                {
                    DependencyService.Get<Toast>().Show("Receiver Mobile Number is not valid...");
                    return;
                }
            }

            this.Navigation.PushAsync(new NationalDeliveryParcelEstimateView(vm));
        }
    }
}