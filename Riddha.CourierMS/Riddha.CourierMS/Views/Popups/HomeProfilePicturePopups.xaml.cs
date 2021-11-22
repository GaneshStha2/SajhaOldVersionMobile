using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Riddha.CourierMS.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeProfilePicturePopups : PopupPage
    {

        private readonly Action<bool> setResultAction;


        PopUpMenuVm vm;
        public HomeProfilePicturePopups(Action<bool> setResultAction)
        {
            vm = new PopUpMenuVm();
            InitializeComponent();
            vm.getmenuforpopups();
            this.MenuList.ItemsSource = vm.MenuList;
            this.setResultAction = setResultAction;
        }




        public static async Task<bool> OpenHomeProfilePicturePopupsPopup(INavigation navigation)
        {
            TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();

            void callback(bool didConfirm)
            {
                completionSource.TrySetResult(didConfirm);
            }

            var popup = new HomeProfilePicturePopups(callback);

            await navigation.PushPopupAsync(popup);

            return await completionSource.Task;
        }

        private void MenuList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedmenu = e.Item as MenuViewModel;
            if (selectedmenu!=null)
            {
                switch (selectedmenu.PopUpMenuFor)
                {
                    case PopUpMenuFor.Logout:
                        var userlist = InitializeDB.Database.GetUsersInfoListAsync();
                        if (userlist.Count > 0)
                        {
                            foreach (var item in userlist)
                            {
                                InitializeDB.Database.DeleteUserInfo(item);
                            }
                            setResultAction?.Invoke(false);
                            this.Navigation.PopPopupAsync().ConfigureAwait(false);
                            App.Current.MainPage = new LoginView(); 
                        }
                        break;
                    case PopUpMenuFor.ViewProfile:
                        setResultAction?.Invoke(false);
                        this.Navigation.PopPopupAsync().ConfigureAwait(false);
                        break;
                    case PopUpMenuFor.Setting:
                        setResultAction?.Invoke(false);
                        this.Navigation.PopPopupAsync().ConfigureAwait(false);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                setResultAction?.Invoke(false);
                this.Navigation.PopPopupAsync().ConfigureAwait(false);
            }
        }

       







    }
}