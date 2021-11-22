using Plugin.Connectivity;
using Riddha.CourierMS.Common.Model;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.Resources.DesignComponent;
using Riddha.CourierMS.ViewModels;
using Riddha.CourierMS.Views.Popups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Riddha.CourierMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashBoardView : ContentPage
    {

        DashBoardVm vm;
        public DashBoardView()
        {
            vm = new DashBoardVm();

            InitializeComponent();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (InitializeDB.UserInfo != null)
            {
                if (App.LoginType == App.LoginStatus.RecentLogin)
                {
                    if (App.NetworkStatus != App.OnlineOrOffline.Ofline)
                    {
                            getUpdatedProfile();
                    }
                }
                if (InitializeDB.UserInfo.UserType == UserType.Customer)
                {
                    vm.getListofMenuForCustomer();
                    CostumerMenuList.ItemsSource = vm.MenuListForCustomer;
                    CompanyUserMenuList.IsVisible = false;
                    CostumerMenuList.IsVisible = true;
                    Homebtn.IsVisible = true;

                    
                }
                else
                {
                    vm.getListofMenuForCompanyUser();
                    CompanyUserMenuList.ItemsSource = vm.MenuListForCompanyUser;
                    CostumerMenuList.IsVisible = false;
                    CompanyUserMenuList.IsVisible = true;
                    TopBackgroungBoard.Padding = new Thickness(10, 10, 10, 30);
                    Homebtn.IsVisible = false;
                }

                vm.DashBoardViewModel.UserName = InitializeDB.UserInfo.UserName;
                UserName.Text = InitializeDB.UserInfo.UserName;

                if (!string.IsNullOrEmpty(InitializeDB.UserInfo.ProfilePhoto))
                {/*== null ? System.Web.HttpContext.Current.Request.Url + "/Images/Picture.png" : userInfo.PhotoURL*/
                    if (InitializeDB.UserInfo.ProfilePhoto.Length > 1000)
                    {
                        img.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(InitializeDB.UserInfo.ProfilePhoto)));
                    }
                    else
                    {
                        ImageUri.Uri = new Uri(CONSTANTS.BaseUrl + InitializeDB.UserInfo.ProfilePhoto);

                    }
                }
                else
                {
                    ImageUri.Uri = new Uri(CONSTANTS.BaseUrl + "/Images/Picture.png");
                }

                //using (MemoryStream m = new MemoryStream())
                //{

                //    byte[] imageBytes = (byte[])args;

                //    // Convert byte[] to Base64 String
                //    vm.DashBoardViewModel.ImageUrl = vm.DashBoardViewModel.ImageUrl


                //};  MessagingCenter.Subscribe<byte[]>(this, "ImageSelected", (args) =>
                //{
                //    Device.BeginInvokeOnMainThread(() =>
                //    {
                //        //Set the source of the image view with the byte array
                //        img.Source = ImageSource.FromStream(() => new MemoryStream((byte[])args));


                //        using (MemoryStream m = new MemoryStream())
                //        {

                //            byte[] imageBytes = (byte[])args;

                //            // Convert byte[] to Base64 String
                //            vm.SignUpViewModel.ProfilePhoto = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);


                //        }
                //        CameraImage.IsVisible = false;
                //        PhotoFrame.IsVisible = true;
                //    });
                //});

                if (DateTime.Now.Hour < 12)
                {
                    vm.DashBoardViewModel.GreetingText = "Good Morning";
                    GreetingText.Text = "Good Morning";
                }
                else if (DateTime.Now.Hour < 17)
                {
                    vm.DashBoardViewModel.GreetingText = "Good Afternoon";
                    GreetingText.Text = "Good Afternoon";
                }
                else
                {
                    vm.DashBoardViewModel.GreetingText = "Good Evening";
                    GreetingText.Text = "Good Evening";
                }
                this.BindingContext = vm;
            }
            else
            {
                App.Current.MainPage = new LoginView();
            }






        }

        private async void getUpdatedProfile()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    vm.getInfomationByUserId(InitializeDB.UserInfo.UserId);
                    if (string.IsNullOrEmpty(vm.DashBoardViewModel.UserName))
                    {
                        App.Current.MainPage = new LoginView();
                        return;
                    }

                }
                catch (Exception ex)
                {
                    App.Current.MainPage = new LoginView();
                    return;
                }
            }
            else
            {


                await DisplayActionSheet(App.NetworkConnectionTitle, App.NetworkConnectionMessage, App.NetworkConnectionButtonText);



            }
        }

        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new LoginView());
        }

        private void CompanyUserMenuList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedmenu = e.Item as MenuViewModel;

            switch (selectedmenu.MenuFor)
            {
                case MenuFor.ReceiveParcel:
                    this.Navigation.PushAsync(new ParcelReceiveView());
                    break;
                case MenuFor.DispatchParcel:
                    this.Navigation.PushAsync(new ParcelDispatchView());
                    break;
                case MenuFor.DeliverParcel:
                    this.Navigation.PushAsync(new DeliveryView());
                    break;

                case MenuFor.PickUpParcel:
                    this.Navigation.PushAsync(new ParcelPickUpView());
                    break;

                default:
                    break;
            }

        }



        private void BookingButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new BookingView());
        }

       

        private void CostumerMenuList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedmenu = e.Item as MenuViewModel;

            switch (selectedmenu.MenuFor)
            {
                case MenuFor.ReceiveParcel:
                    break;
                case MenuFor.DispatchParcel:
                    break;
                case MenuFor.DeliverParcel:
                    break;
                case MenuFor.Booking:
                    App.IsEstimateOnly = false;
                    this.Navigation.PushAsync(new BookingView());

                    break;
                case MenuFor.CreditStatement:
                    this.Navigation.PushAsync(new CreditStatementView());
                    break;
                case MenuFor.Rates:

                    App.IsEstimateOnly = true;
                    BookingVm bookingVm = new BookingVm();

                    this.Navigation.PushAsync(new DeliveryLocationView(bookingVm));

                    break;
                default:
                    break;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private async void ImageTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var result = await HomeProfilePicturePopups.OpenHomeProfilePicturePopupsPopup(this.Navigation);

        }

        private void CompanyUserMenuList_Clicked(object sender, EventArgs e)
        {
            var button = sender as Xamarin.Forms.Button;
            if (button != null)
            {
                var cmdParam = button.CommandParameter;

                if (cmdParam != null)
                {

                    MenuFor selectedMenuFor = (MenuFor)cmdParam;

                    switch (selectedMenuFor)
                    {


                        case MenuFor.ReceiveParcel:
                            this.Navigation.PushAsync(new ParcelReceiveView());
                            break;
                        case MenuFor.DispatchParcel:
                            this.Navigation.PushAsync(new ParcelDispatchView());
                            break;
                        case MenuFor.DeliverParcel:
                            this.Navigation.PushAsync(new DeliveryView());
                            break;

                        case MenuFor.PickUpParcel:
                            this.Navigation.PushAsync(new ParcelPickUpView());
                            break;

                        default:
                            break;
                    }
                }

            }
        }

        private void ViewHistoryButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new ViewHistoryView(vm));
        }

        private void ListButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Xamarin.Forms.Button;
            if (button != null)
            {
                var cmdParam = button.CommandParameter;

                if (cmdParam != null)
                {

                    MenuFor selectedMenuFor = (MenuFor)cmdParam;

                    switch (selectedMenuFor)
                    {
                      
                        case MenuFor.Booking:
                            App.IsEstimateOnly = false;
                            this.Navigation.PushAsync(new BookingView());
                            break;
                        case MenuFor.CreditStatement:
                            this.Navigation.PushAsync(new CreditStatementView());
                            break;
                        case MenuFor.Rates:
                            App.IsEstimateOnly = true;
                            BookingVm bookingVm = new BookingVm();
                            this.Navigation.PushAsync(new DeliveryLocationView(bookingVm));
                            break;
                        default:
                            break;
                    }
                }

            }
        }
    }
}