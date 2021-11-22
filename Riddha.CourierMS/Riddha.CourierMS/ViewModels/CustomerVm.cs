using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
    public class CustomerVm
    {
        public CustomerViewModel Customer { get; set; }
        public ObservableCollection<CustomerViewModel> RecentCustomers { get; set; }

        public CustomerVm() : base()
        {
            Customer = new CustomerViewModel();
            RecentCustomers = new ObservableCollection<CustomerViewModel>();
        }


        public ObservableCollection<CustomerViewModel> getRecentCustomers(int customerId)
        {
            CommonServices commonServices = new CommonServices();

            RecentCustomers = new ObservableCollection<CustomerViewModel>();

            var data = commonServices.getRecentTransactionCustomerList(customerId).Result;

            if (data.Status == MobileResultStatus.Ok)
            {
                var result = (from c in data.Data.ToList()
                              select new CustomerViewModel()
                              {

                                  Id = c.Id,
                                  FullName = c.FullName,
                                  Contact = c.Contact,
                                  Email = c.Email
                              }).ToList();

                RecentCustomers = new ObservableCollection<CustomerViewModel>(result);


                return RecentCustomers;

            }
            return RecentCustomers;
        }

    }

    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
    }
}
