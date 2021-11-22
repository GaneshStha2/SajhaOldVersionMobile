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
    public partial class ComponentOneView : ContentPage
    {
        public ComponentOneView()
        {
            InitializeComponent();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new ComponentTwoView());
        }
    }
}