using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
   public class PopUpMenuVm
    {

        public ObservableCollection<MenuViewModel> MenuList { get; set; }

        public PopUpMenuVm() : base()  {


            MenuList = new ObservableCollection<MenuViewModel>();
        
        
        }

        public ObservableCollection<MenuViewModel> getmenuforpopups()
        {
            MenuList = new ObservableCollection<MenuViewModel>();
            MenuList.Add(new MenuViewModel()
            {
                Title = "View Profile",
                Icon = "ProfileIcon.png",
                PopUpMenuFor = PopUpMenuFor.ViewProfile
                

            });
            MenuList.Add(new MenuViewModel()
            {
                Title = "Setting",
                Icon = "Setting.png",
                PopUpMenuFor = PopUpMenuFor.Setting

            });
            MenuList.Add(new MenuViewModel()
            {
                Title = "Log out",
                Icon = "Logout.png",
                PopUpMenuFor = PopUpMenuFor.Logout

            });


            return MenuList;
        }
    }

}
