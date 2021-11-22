using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
    public class CountryVm
    {
        public ObservableCollection<CountryViewModel> CountryList { get; set; }
        public CountryViewModel CountryViewModel { get; set; }
        public CountryVm() : base()
        {
            CountryList = new ObservableCollection<CountryViewModel>();
            CountryViewModel = new CountryViewModel();
        }

        public ObservableCollection<CountryViewModel> getCountryList()
        {
            CommonServices commonServices = new CommonServices();
            CountryList = new ObservableCollection<CountryViewModel>();
            var data = commonServices.getCountryList().Result;
            if (data.Status == MobileResultStatus.Ok)
            {
                var result = (from c in data.Data.ToList()
                              select new CountryViewModel()
                              {

                                  Id = c.Id,
                                  Name = c.Name

                              }).ToList();
                CountryList = new ObservableCollection<CountryViewModel>(result);
                return CountryList;

            }
            return CountryList;
        }
    }

    public class CountryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
