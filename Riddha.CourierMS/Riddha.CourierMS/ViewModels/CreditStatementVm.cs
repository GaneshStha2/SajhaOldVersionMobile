using Riddha.CourierMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
    public class CreditStatementVm
    {
        public ObservableCollection<ParcelReceiveViewModel>  UnPaidTransactions { get; set; }

        public CreditStatementVm() :base()
        {
            UnPaidTransactions = new ObservableCollection<ParcelReceiveViewModel>();
        }


        public ObservableCollection<ParcelReceiveViewModel> getCreditStatement(int customerId)
        {
            UnPaidTransactions = new ObservableCollection<ParcelReceiveViewModel>();

            TransactionServices _transactionServices = new TransactionServices();


            var data = _transactionServices.getCreditStatement(customerId).Result;


            if (data.Status == Common.Services.MobileResultStatus.Ok)

            {
                var result = (from c in data.Data.ToList()
                              select new ParcelReceiveViewModel()
                              {
                                  Amount = c.Amount,
                                  BookingId = c.BookingId,
                                  CustomerName = c.CustomerName,
                                  FromLocation = c.FromLocation,
                                  Id = c.Id,
                                  ParcelType = c.ParcelType,
                                  ToLocation = c.ToLocation,
                                  TrackingNo = c.TrackingNo,
                                  ContactNo = c.ContactNo
                              }).ToList();

                UnPaidTransactions = new ObservableCollection<ParcelReceiveViewModel>(result);

                return UnPaidTransactions;
            }

            return UnPaidTransactions;
        }

    }
}
