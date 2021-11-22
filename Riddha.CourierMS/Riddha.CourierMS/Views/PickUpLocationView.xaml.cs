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
    public partial class PickUpLocationView : ContentPage
    {
        BookingVm vm;
        BranchVm branchVm;
        public PickUpLocationView(BookingVm model)
        {
            Shell.SetTabBarIsVisible(this, false);
            InitializeComponent();
            vm = model;
            this.BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            branchVm = new BranchVm();
            if (CrossConnectivity.Current.IsConnected)
            {
                branchVm.getBranchList();
                BranchList.ItemsSource = branchVm.BranchList;
                
                BranchList.IsVisible = false;

                if (!string.IsNullOrEmpty(vm.Booking.FromLocationName))
                {
                    PickuplocationText.Text = "Pick Up Location: " + vm.Booking.FromLocationName;
                    DownPathIcon.IsVisible = false;
                }
                else
                {
                    PickuplocationText.Text = "Pick Up Location: ";
                    DownPathIcon.IsVisible = true;
                }
            }
            else
            {
                await DisplayAlert(App.NetworkConnectionTitle, App.NetworkConnectionMessage, App.NetworkConnectionButtonText);
            }

        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Shell.SetTabBarIsVisible(this, false);
            this.Navigation.PushAsync(new BookingView());
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton radionbtn = sender as RadioButton;
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
                            vm.Booking.FromLocationId = commandpram;
                            vm.Booking.FromLocationName = item.Name;
                        }

                    }
                    if (!string.IsNullOrEmpty(vm.Booking.FromLocationName))
                    {
                        PickuplocationText.Text = "Pick Up Location: " + vm.Booking.FromLocationName;
                        DownPathIcon.IsVisible = false;
                    }
                    else
                    {
                        PickuplocationText.Text = "Pick Up Location: ";
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




        private void NextButton_Clicked(object sender, EventArgs e)
        {
            if (vm.Booking.FromLocationId == 0)
            {
                DependencyService.Get<Toast>().Show("Select Pick up location...");
                return;
            }

            else if (string.IsNullOrEmpty(vm.Booking.FromStreetAddress))
            {
                DependencyService.Get<Toast>().Show("Street Address is not valid...");
                return;
            }

            this.Navigation.PushAsync(new DeliveryLocationView(vm));

        }

        private void PickUpLocationLable_Tabbed(object sender, EventArgs e)
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