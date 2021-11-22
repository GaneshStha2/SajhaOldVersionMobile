using Riddha.CourierMS.ViewModels;
using Riddha.CourierMS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Riddha.CourierMS
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SignInButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new LoginView());
        }

        private void CustomerButtton_Clicked(object sender, EventArgs e)
        {
            SignupVm vm = new SignupVm();
            this.Navigation.PushModalAsync(new SignUpView(vm));
        }

        private void ShajhaUserButtton_Clicked(object sender, EventArgs e)
        {
            SignupVm vm = new SignupVm();
            this.Navigation.PushModalAsync(new LoginView());
        }
    }
}
