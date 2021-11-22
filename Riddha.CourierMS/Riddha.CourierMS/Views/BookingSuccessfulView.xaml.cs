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
    public partial class BookingSuccessfulView : ContentPage
    {
        BookingVm vm ;
        public BookingSuccessfulView(BookingVm model )
        {
            InitializeComponent();

            vm = model;

            this.MessageHead.Text = "You've Booked " + vm.Booking.PackageTypeName + ",";
            this.MessageBody.Text = "to " + vm.Booking.ToLocationName;

            this.BindingContext = vm;
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {

            this.Navigation.PushAsync(new BookingView());

        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new DashBoardView());
        }
    }
}