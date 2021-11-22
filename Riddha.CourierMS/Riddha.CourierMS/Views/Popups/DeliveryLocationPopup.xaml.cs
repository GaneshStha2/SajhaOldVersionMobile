using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class DeliveryLocationPopup : PopupPage
    {
        private readonly Action<BranchViewModel> setResultAction;

        BranchVm vm;

        public DeliveryLocationPopup(Action<BranchViewModel> setResultAction, BranchVm model)
        {
            InitializeComponent();
            this.vm = model;
            
            getbranchlist();
            this.setResultAction = setResultAction;
            this.BindingContext = vm;
        }

        public static async Task<BranchViewModel> OpenDeliveryLocationPopup(INavigation navigation, BranchVm model)
        {
            TaskCompletionSource<BranchViewModel> completionSource = new TaskCompletionSource<BranchViewModel>();

            void callback(BranchViewModel didConfirm)
            {
                completionSource.TrySetResult(didConfirm);
            }

            var popup = new DeliveryLocationPopup(callback, model);

            await navigation.PushPopupAsync(popup);

            return await completionSource.Task;
        }


        private void BackButton_Clicked(object sender, EventArgs e)
        {
            setResultAction?.Invoke(vm.BranchViewModel);
            this.Navigation.PopPopupAsync().ConfigureAwait(false);
        }

        private void BranchList_ItemSeleced(object sender, SelectedItemChangedEventArgs e)
        {
            var selectbranch = e.SelectedItem as BranchViewModel;

            if (e.SelectedItem != null)
            {
                vm.BranchViewModel.Id = selectbranch.Id;
                vm.BranchViewModel.Name = selectbranch.Name;


                foreach (var item in vm.BranchList.Where(x => x.Checked == true))
                {
                    item.Checked = false;
                }

                vm.BranchViewModel.Checked = selectbranch.Checked;

                setResultAction?.Invoke(vm.BranchViewModel);
        
            }

            else
            {
                this.Navigation.PopPopupAsync().ConfigureAwait(false);
            }

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            setResultAction?.Invoke(vm.BranchViewModel);
            this.Navigation.PopPopupAsync().ConfigureAwait(false);
        }

        private async void getbranchlist()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    vm.getBranchList();
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