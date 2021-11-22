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
    public partial class NationalBookingView : ContentPage
    {


        BookingVm vm;

        public NationalBookingView()
        {
            vm = new BookingVm();
            InitializeComponent();
            this.BindingContext = vm;

        }
        



        private void BackButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new BookingView());
        }

        private void BranchList_ItemSeleced(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}