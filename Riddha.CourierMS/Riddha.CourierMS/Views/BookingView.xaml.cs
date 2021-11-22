using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.ViewModels;
using Riddha.CourierMS.Views.Popups;
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
    public partial class BookingView : ContentPage
    {

        BookingVm vm;
        public BookingView()
        {
            InitializeComponent();
            vm = new BookingVm();
            this.BindingContext = vm;
        }



        private void NationalTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            NationalFrame.BackgroundColor = Color.FromHex("#E0ECDE");
            this.Navigation.PushAsync(new PickUpLocationView(vm));
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new DashBoardView());
        }

        private void InterNationalTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<Toast>().Show("Not Active Right Now");
            //InternationalFrame.BackgroundColor = Color.FromHex("#E0ECDE");
            //this.Navigation.PushAsync(new PickUpLocationView(vm));
        }
    }
}