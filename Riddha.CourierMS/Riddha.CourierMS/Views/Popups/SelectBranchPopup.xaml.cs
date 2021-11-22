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
    public partial class SelectBranchPopup : PopupPage
    {
        private readonly Action<BranchViewModel> setResultAction;

        BranchVm vm;
        public SelectBranchPopup(Action<BranchViewModel> setResultAction, BranchVm model)
        {
            this.vm = model;
            InitializeComponent();
            getbranchlist();
            //BranchList.ItemsSource = vm.BranchList;
            this.setResultAction = setResultAction;
            this.BindingContext = vm;


        }

        private async void getbranchlist()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (vm.BranchList.Count == 0)
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

            }
            else
            {
                await DisplayAlert(App.NetworkConnectionTitle, App.NetworkConnectionMessage, App.NetworkConnectionButtonText);
            }
        }

        public static async Task<BranchViewModel> OpenSelectBranchPopup(INavigation navigation, BranchVm model)
        {
            TaskCompletionSource<BranchViewModel> completionSource = new TaskCompletionSource<BranchViewModel>();

            void callback(BranchViewModel didConfirm)
            {
                completionSource.TrySetResult(didConfirm);
            }

            var popup = new SelectBranchPopup(callback, model);

            await navigation.PushPopupAsync(popup);

            return await completionSource.Task;
        }

        private void CloseButtonClicked(object sender, EventArgs e)
        {

            setResultAction?.Invoke(vm.BranchViewModel);
            this.Navigation.PopPopupAsync().ConfigureAwait(false);
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
                setResultAction?.Invoke(vm.BranchViewModel);
                this.Navigation.PopPopupAsync().ConfigureAwait(true);
            }

            else
            {
                setResultAction?.Invoke(vm.BranchViewModel);
                this.Navigation.PopPopupAsync().ConfigureAwait(false);
            }

        }

        //private void YesButtonClicked(object sender, EventArgs e)
        //{
        //    setResultAction?.Invoke(true);
        //    this.Navigation.PopPopupAsync().ConfigureAwait(true);
        //}
    }
}