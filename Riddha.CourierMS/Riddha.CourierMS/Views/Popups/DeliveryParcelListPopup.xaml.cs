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
    public partial class DeliveryParcelListPopup : PopupPage
    {
        DeliveryVm vm;
        ParcelPopUpType popUpType;
        private readonly Action<DeliveryVm> setResultAction;

        //private readonly Action<ParcelReceiveViewModel> setRexResultAction;
        //private readonly Action<ParcelTypeViewModel> setEstimationResultAction;

        public DeliveryParcelListPopup(Action<DeliveryVm> setResultAction, DeliveryVm model, ParcelPopUpType popUpTypeModel)
        {
            InitializeComponent();
            this.vm = model;

            popUpType = popUpTypeModel;
            getDeliveryParcelList(popUpType);

            this.setResultAction = setResultAction;
            this.BindingContext = vm;
        }


        private void BackButton_Clicked(object sender, EventArgs e)
        {
            setResultAction.Invoke(vm);
            this.Navigation.PopPopupAsync(false).ConfigureAwait(false);
        }



        private async void getDeliveryParcelList(ParcelPopUpType popupType)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    int serviceCenterId = InitializeDB.UserInfo.ServiceCenterId;

                    switch (popUpType)
                    {
                        case ParcelPopUpType.PickUp:
                            if (vm.PickUpParcelParcelList.Count ==  0)
                            {

                                vm.getPickUpParcelParcelList(serviceCenterId);
                            }
                            this.DeliveryParcelList.ItemsSource = vm.PickUpParcelParcelList;

                            this.DeliveryParcelList.IsVisible = true;
                            this.EstimateParcelList.IsVisible = false;

                            break;
                        case ParcelPopUpType.Delivery:
                            if (vm.DeliveryParcelList.Count == 0)
                            {

                            vm.getDeliveryParcelList(serviceCenterId);
                            }
                            this.DeliveryParcelList.ItemsSource = vm.DeliveryParcelList;
                            this.DeliveryParcelList.IsVisible = true;
                            this.EstimateParcelList.IsVisible = false;

                            break;
                        case ParcelPopUpType.Estimate:
                            int destinationId = vm.Delivery.DestinationId;
                            vm.getParcelTypeListForDestination(destinationId);
                            this.EstimateParcelList.ItemsSource = vm.AllParcelList;

                            this.DeliveryParcelList.IsVisible = false;
                            this.EstimateParcelList.IsVisible = true;

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

        private void DeliveryParcelList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedParcel = e.SelectedItem as ParcelReceiveViewModel;

            if (e.SelectedItem != null)
            {
                vm.SelectedParcel = selectedParcel;

                setResultAction?.Invoke(vm);
                this.Navigation.PopPopupAsync().ConfigureAwait(false);
            }

            else
            {
                this.Navigation.PopPopupAsync().ConfigureAwait(false);
            }
        }


        public static async Task<DeliveryVm> OpenDeliveryParcelPopup(INavigation navigation, DeliveryVm model, ParcelPopUpType parcelPopUpType)
        {


            TaskCompletionSource<DeliveryVm> completionSource = new TaskCompletionSource<DeliveryVm>();

            void callback(DeliveryVm didConfirm)
            {
                completionSource.TrySetResult(didConfirm);
            }


            var popup = new DeliveryParcelListPopup(callback, model, parcelPopUpType);
            await navigation.PushPopupAsync(popup);

            return await completionSource.Task;


        }

        private void EstimateParcelList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedParcel = e.SelectedItem as ParcelTypeViewModel;

            if (e.SelectedItem != null)
            {
                vm.SelectedParcelForDestination = selectedParcel;


                setResultAction?.Invoke(vm);
                this.Navigation.PopPopupAsync().ConfigureAwait(false);
            }

            else
            {
                this.Navigation.PopPopupAsync().ConfigureAwait(false);
            }
        }
    }


}