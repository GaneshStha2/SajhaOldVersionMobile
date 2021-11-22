using Riddha.CourierMS.Common.Model;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Model;
using Riddha.CourierMS.Services;
using Riddha.CourierMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
    public class DashBoardVm
    {
        public DashBoardViewModel DashBoardViewModel { get; set; }
        public ObservableCollection<MenuViewModel> MenuListForCustomer { get; set; }
        public ObservableCollection<MenuViewModel> MenuListForCompanyUser { get; set; }
        public ObservableCollection<RecentTransactionViewModel> RecentTransactionList { get; set; }

        public DashBoardVm() : base()
        {
            DashBoardViewModel = new DashBoardViewModel();
            MenuListForCustomer = new ObservableCollection<MenuViewModel>();
            MenuListForCompanyUser = new ObservableCollection<MenuViewModel>();
            RecentTransactionList = new ObservableCollection<RecentTransactionViewModel>();
        }
        #region Function Part
        public ObservableCollection<MenuViewModel> getListofMenuForCustomer()
        {
            MenuListForCustomer = new ObservableCollection<MenuViewModel>();

            MenuListForCustomer.Add(new MenuViewModel()
            {
                ButtonColor = "#F75010",
                ButtonText = "OK",
                Icon = "HomeParcelIcon.png",
                Message = "Have an parcel today?",
                Title = "Book A Parcel?",
                MenuFor = MenuFor.Booking
            });
            MenuListForCustomer.Add(new MenuViewModel()
            {
                ButtonColor = "#47CACC",
                ButtonText = "VIEW",
                Icon = "HomeCreditIcon.png",
                Message = "View Statement",
                Title = "Credit Statement",
                MenuFor = MenuFor.CreditStatement
            });
            MenuListForCustomer.Add(new MenuViewModel()
            {
                ButtonColor = "#F75010",
                ButtonText = "CHECK",
                Icon = "HomeDollor.png",
                Message = "You can estimate your courier cost.",
                Title = "Wodering About Rates?",
                MenuFor = MenuFor.Rates
            });
            //MenuListForCustomer.Add(new MenuViewModel() { });
            //MenuListForCustomer.Add(new MenuViewModel() { });
            //MenuListForCustomer.Add(new MenuViewModel() { });
            //MenuListForCustomer.Add(new MenuViewModel() { });
            return MenuListForCustomer;
        }


        public DashBoardViewModel getInfomationByUserId(int userId)
        {

            DashBoardViewModel = new DashBoardViewModel();

            UserInformationServices userInformationServices = new UserInformationServices();


            var result = userInformationServices.getUserInformationByUserId(userId).Result;

            if (result.Status == MobileResultStatus.Ok)
            {
                DashBoardViewModel.UserName = result.Data.UserName;
                DashBoardViewModel.ImageUrl = result.Data.ImageUrl;
                DashBoardViewModel.LoginUserType = result.Data.UserType;

                var item = InitializeDB.Database.GetUsersInfoListAsync();
                if (item.Count > 0)
                {
                    item[0].UserId = result.Data.UserId;
                    item[0].UserName = result.Data.UserName;
                    item[0].Password = result.Data.Password;
                    item[0].Token = result.Token;
                    item[0].UserType = result.Data.UserType;
                    item[0].CustomerId = result.Data.CustomerId;
                    item[0].ProfilePhoto = result.Data.ImageUrl;
                    item[0].ServiceCenterName = result.Data.ServiceCenterName;
                    InitializeDB.Database.SaveUserInfoAsync(item[0]);
                }
                else
                {
                    var userInfo = new UserInfoDb()
                    {
                        UserId = result.Data.UserId,
                        UserName = result.Data.UserName,
                        Password = result.Data.Password,
                        Token = result.Token,
                        UserType = result.Data.UserType,
                        CustomerId = result.Data.CustomerId,
                        ProfilePhoto = result.Data.ImageUrl,
                        ServiceCenterName = result.Data.ServiceCenterName
                    };
                    var taskresult = InitializeDB.Database.SaveUserInfoAsync(userInfo);
                   
                }

            }
           

            return DashBoardViewModel;
        }


        public ObservableCollection<RecentTransactionViewModel> getRecentTransactionByUserId(int userId)
        {
            RecentTransactionList = new ObservableCollection<RecentTransactionViewModel>();

            TransactionServices transactionServices = new TransactionServices();


            var result = transactionServices.getRecentTransactionByUserId(userId).Result;

            if (result.Status == MobileResultStatus.Ok)
            {
                var recenttransactionlist = (from c in result.Data.ToList()
                                             select new RecentTransactionViewModel()
                                             {
                                                 TransactionType = c.TransactionType,
                                                 DocumentTypeName = c.DocumentTypeName,
                                                 RecipientOrSenderName = c.RecipientOrSenderName,
                                                 SendOrReceiveAddress = c.SendOrReceiveAddress,
                                                 TransactionId = c.TransactionId,
                                                 TrackingNo = c.TrackingNo,
                                                 Amount = c.Amount
                                             }).ToList();


                RecentTransactionList = new ObservableCollection<RecentTransactionViewModel>(recenttransactionlist);
            }
            
            return RecentTransactionList;


        }

        public ObservableCollection<MenuViewModel> getListofMenuForCompanyUser()
        {
            MenuListForCompanyUser = new ObservableCollection<MenuViewModel>();

            MenuListForCompanyUser.Add(new MenuViewModel()
            {
                ButtonColor = "#F75010",
                ButtonText = "OK",
                Icon = "HomeReciveParcelIcon.png",
                Message = "Click Here To Receive Parcel.",
                Title = "Receive Parcel",
                MenuFor = MenuFor.ReceiveParcel
            });
            MenuListForCompanyUser.Add(new MenuViewModel()
            {
                ButtonColor = "#47CACC",
                ButtonText = "VIEW",
                Icon = "HomeDispatchParcel.png",
                Message = "View Statement",
                Title = "Dispatch Parcel",
                MenuFor = MenuFor.DispatchParcel
            });
            MenuListForCompanyUser.Add(new MenuViewModel()
            {
                ButtonColor = "#F75010",
                ButtonText = "CHECK",
                Icon = "HomeDeliverParcel.png",
                Message = "You can estimate your courier cost.",
                Title = "Deliver Parcel",
                MenuFor = MenuFor.DeliverParcel
            }); MenuListForCompanyUser.Add(new MenuViewModel()
            {
                ButtonColor = "#F75010",
                ButtonText = "CHECK",
                Icon = "HomePickupParcel.png",
                Message = "You can estimate your courier cost.",
                Title = "Pick Up Parcel",
                MenuFor = MenuFor.PickUpParcel
            });

            return MenuListForCompanyUser;
        }

        #endregion

    }
    public class DashBoardViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string GreetingText { get; set; }
        public string Password { get; set; }
        public UserType LoginUserType { get; set; }


    }
    public class MenuViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Icon { get; set; }
        public string ButtonColor { get; set; }
        public string ButtonText { get; set; }
        public MenuFor MenuFor { get; set; }
        public PopUpMenuFor PopUpMenuFor { get; set; }
    }


    



    public class RecentTransactionViewModel
    {
        public int TransactionId { get; set; }
        public string TrackingNo { get; set; }
        public string DocumentTypeName { get; set; }
        public string RecipientOrSenderName { get; set; }
        public string SendOrReceiveAddress { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
    }

    public enum MenuFor
    {
        ReceiveParcel,
        DispatchParcel,
        DeliverParcel,
        PickUpParcel,

        //Added For Customer

        Booking,
        CreditStatement,
        Rates,


        ///
        //Popups
        Logout,
        ViewProfile


    }
    public enum PopUpMenuFor
    {
        //Popups
        Logout,
        ViewProfile,
        Setting
    }

    public enum PopUpRequestedFrom
    {
        //InNavigation
        Estimate
    }
}
