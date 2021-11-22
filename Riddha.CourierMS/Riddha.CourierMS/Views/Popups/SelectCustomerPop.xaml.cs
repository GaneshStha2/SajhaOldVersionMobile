using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Riddha.CourierMS.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectCustomerPop : PopupPage
    {
        private readonly Action<CustomerViewModel> setResultAction;

        CustomerVm vm;
        public SelectCustomerPop(Action<CustomerViewModel> setResultAction, CustomerVm model)
        {
            InitializeComponent();
            this.vm = model;

            getRecentCustomerlist();
            this.setResultAction = setResultAction;
            this.BindingContext = vm;
        }

        private async void getRecentCustomerlist()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    int customerId = InitializeDB.UserInfo.CustomerId;

                    vm.getRecentCustomers(customerId);

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

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            setResultAction?.Invoke(vm.Customer);
            this.Navigation.PopPopupAsync().ConfigureAwait(false);
        }


        public static async Task<CustomerViewModel> OpenCustomerSelectionPopup(INavigation navigation, CustomerVm model)
        {
            TaskCompletionSource<CustomerViewModel> completionSource = new TaskCompletionSource<CustomerViewModel>();

            void callback(CustomerViewModel didConfirm)
            {
                completionSource.TrySetResult(didConfirm);
            }

            var popup = new SelectCustomerPop(callback, model);

            await navigation.PushPopupAsync(popup);

            return await completionSource.Task;
        }

        private void CloseButtonClicked(object sender, EventArgs e)
        {

            setResultAction?.Invoke(vm.Customer);
            this.Navigation.PopPopupAsync().ConfigureAwait(false);
        }


        private void CustomerList_ItemSeleced(object sender, SelectedItemChangedEventArgs e)
        {
            var selectCustomer = e.SelectedItem as CustomerViewModel;
            if (e.SelectedItem != null)
            {

                vm.Customer.Id = selectCustomer.Id;
                vm.Customer.FullName = selectCustomer.FullName;
                vm.Customer.Email = selectCustomer.Email;
                vm.Customer.Contact = selectCustomer.Contact;

                setResultAction?.Invoke(vm.Customer);
                this.Navigation.PopPopupAsync().ConfigureAwait(true);

            }

            else
            {
                setResultAction?.Invoke(vm.Customer);
                this.Navigation.PopPopupAsync().ConfigureAwait(false);
            }

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}