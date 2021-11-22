using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Riddha.CourierMS.Resources.DesignComponent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComponentTwoView : ContentPage
    {
        public ComponentTwoView()
        {
            InitializeComponent();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }
    }
}