using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Riddha.CourierMS.DependencyServices;
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
    public partial class PickUpLocationPopup : PopupPage
    {
        private readonly Action<BranchViewModel> setResultAction;

        int selectedId;
        string selectedBranchName;

        BranchVm vm;
        public PickUpLocationPopup(Action<BranchViewModel> setResultAction, BranchVm model)
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

        public static async Task<BranchViewModel> OpenPickUpLocationPopup(INavigation navigation, BranchVm model)
        {
            TaskCompletionSource<BranchViewModel> completionSource = new TaskCompletionSource<BranchViewModel>();

            void callback(BranchViewModel didConfirm)
            {
                completionSource.TrySetResult(didConfirm);
            }

            var popup = new PickUpLocationPopup(callback, model);

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

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var radionbtn = sender as RadioButton;
            int commandpram = (int)radionbtn.CommandParameter;

            foreach (var item in vm.BranchList)
            {
                item.Checked = false;
                if (item.Id == commandpram)
                {
                    item.Checked = true;
                    selectedId = item.Id;
                    selectedBranchName = item.Name;
                }

            }
            this.BranchList.BeginRefresh();
            BranchList.RefreshCommand = new Command(() =>
            {
                RefreshData();
            });
            this.BranchList.EndRefresh();


        }

        private void BranchList_ItemSeleced(object sender, ItemTappedEventArgs e)
        {
            var selectbranch = e.Item as BranchViewModel;
            if (selectbranch != null)
            {
                foreach (var item in vm.BranchList)
                {
                    item.Checked = false;
                    if (item.Id == selectbranch.Id)
                    {
                        item.Checked = true;
                        selectedId = item.Id;
                        selectedBranchName = item.Name;

                    }

                }
                this.BranchList.BeginRefresh();
                BranchList.RefreshCommand = new Command(() =>
                {
                    RefreshData();
                });
                this.BranchList.EndRefresh();


            }
            else
            {
                //setResultAction?.Invoke(vm.BranchViewModel);
                //validation msg

                this.Navigation.PopPopupAsync().ConfigureAwait(false);
            }
        }

        private void RefreshData()
        {

            BranchList.ItemsSource = vm.BranchList;
        }



        private void NextButton_Clicked(object sender, EventArgs e)
        {
            vm.BranchViewModel.Id = selectedId;
            vm.BranchViewModel.Name = selectedBranchName;


            if (vm.BranchViewModel.Id == 0)
            {
                DependencyService.Get<Toast>().Show("Select Branch...");
                return;
            }

            else if (string.IsNullOrEmpty(vm.BranchViewModel.StreetAddress))
            {
                DependencyService.Get<Toast>().Show("Street Address is not valid...");
                return;
            }

            setResultAction?.Invoke(vm.BranchViewModel);
            this.Navigation.PopPopupAsync().ConfigureAwait(false);

        }


    }
}