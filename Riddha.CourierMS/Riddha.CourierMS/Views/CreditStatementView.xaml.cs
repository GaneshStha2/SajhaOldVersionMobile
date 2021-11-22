using Plugin.Connectivity;
using Riddha.CourierMS.Common.Services;
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
    public partial class CreditStatementView : ContentPage
    {
        CreditStatementVm vm;
        public CreditStatementView()
        {
            InitializeComponent();
            vm = new CreditStatementVm();
            GetCreditStatement();
            this.BindingContext = vm;
        }

        private async void GetCreditStatement()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    int customerId = InitializeDB.UserInfo.CustomerId;
                    vm.getCreditStatement(customerId);
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

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new DashBoardView());
        }

    }
}