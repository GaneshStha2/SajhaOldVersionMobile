using Plugin.Connectivity;
using Riddha.CourierMS.DependencyServices;
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
    public partial class DeliveryLocationView : ContentPage
    {
        BookingVm vm;
        BranchVm branchVm;
        public DeliveryLocationView(BookingVm model)
        {
            InitializeComponent();

            vm = model;
            this.BindingContext = vm;



        }

        protected async override void OnAppearing()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                branchVm = new BranchVm();
                branchVm.getBranchList();

                BranchList.IsVisible = false;

                if (!string.IsNullOrEmpty(vm.Booking.ToLocationName))
                {
                    DeliverylocationText.Text = "Delivery Location: " + vm.Booking.ToLocationName;
                    DownPathIcon.IsVisible = false;
                }
                else
                {
                    DeliverylocationText.Text = "Delivery Location: ";
                    DownPathIcon.IsVisible = true;
                }
                this.BranchList.ItemsSource = branchVm.BranchList;

                if (App.IsEstimateOnly == true)
                {
                    this.BookingProgress.IsVisible = false;
                }
                else
                {
                    this.BookingProgress.IsVisible = true;
                }

            }
            else
            {
                await DisplayAlert(App.NetworkConnectionTitle, App.NetworkConnectionMessage, App.NetworkConnectionButtonText);
            }
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            //App.IsEstimateOnly = false;
            if (App.IsEstimateOnly == false)
            {

                this.Navigation.PushAsync(new PickUpLocationView(vm));
            }
            else
            {
                this.Navigation.PushAsync(new DashBoardView());
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            if (vm.Booking.ToLocationId == 0)
            {
                DependencyService.Get<Toast>().Show("Select delivery location...");
                return;
            }

            if (string.IsNullOrEmpty(vm.Booking.ToStreetAddress))
            {
                DependencyService.Get<Toast>().Show("Street Address is not valid...");
                return;
            }


            if (App.IsEstimateOnly == false)
            {
                this.Navigation.PushAsync(new ReceiverSelectionView(vm));
            }
            else
            {
                this.Navigation.PushAsync(new NationalDeliveryParcelEstimateView(vm));
            }

            //this.Navigation.PushModalAsync(new NationalDeliveryParcelEstimateView(vm));
        }

        private void BranchList_ItemSeleced(object sender, SelectedItemChangedEventArgs e)
        {

            var selectbranch = e.SelectedItem as BranchViewModel;

            if (e.SelectedItem != null)
            {
                vm.Booking.ToLocationId = selectbranch.Id;
                vm.Booking.ToLocationName = selectbranch.Name;




                foreach (var item in branchVm.BranchList.Where(x => x.Checked == true && x.Id != selectbranch.Id))
                {
                    item.Checked = false;
                    if (item.Id == selectbranch.Id)
                    {
                        item.Checked = true;
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
            }
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var radionbtn = sender as RadioButton;


            if (radionbtn != null)
            {
                if (radionbtn.CommandParameter != null)
                {
                    int commandpram = (int)radionbtn.CommandParameter;

                    foreach (var item in branchVm.BranchList)
                    {
                        item.Checked = false;
                        if (item.Id == commandpram)
                        {
                            item.Checked = true;
                            vm.Booking.ToLocationId = commandpram;
                            vm.Booking.ToLocationName = item.Name;
                        }

                    }
                    if (!string.IsNullOrEmpty(vm.Booking.ToLocationName))
                    {
                        DeliverylocationText.Text = "Delivery Location: " + vm.Booking.ToLocationName;
                        DownPathIcon.IsVisible = false;
                    }
                    else
                    {
                        DeliverylocationText.Text = "Delivery Location: ";
                        DownPathIcon.IsVisible = true;

                    }
                    BranchList.IsVisible = false;
                    RefreshGrid();
                }
            }



        }

        private void RefreshGrid()
        {
            this.BranchList.BeginRefresh();
            BranchList.RefreshCommand = new Command(() =>
            {
                BranchList.ItemsSource = branchVm.BranchList;
            });
            this.BranchList.EndRefresh();


        }

        private void RefreshData()
        {

            BranchList.ItemsSource = branchVm.BranchList;
        }
        private void DeliveryLocationLable_Tabbed(object sender, EventArgs e)
        {
            if (BranchList.IsVisible == true)
            {
                BranchList.IsVisible = false;
            }
            else
            {
                BranchList.IsVisible = true;
            }
        }

    }
}