using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
   public class BranchVm
    {
        public ObservableCollection<BranchViewModel> BranchList { get; set; }
        public BranchViewModel BranchViewModel { get; set; }
        public BranchVm() : base(){
            BranchList = new ObservableCollection<BranchViewModel>();
            BranchViewModel = new BranchViewModel();
        }

        public ObservableCollection<BranchViewModel> getBranchList()
        {
            CommonServices commonServices = new CommonServices();
            BranchList = new ObservableCollection<BranchViewModel>();
            var data = commonServices.getBranchList().Result;
            if (data.Status == MobileResultStatus.Ok)
            {
                var result = (from c in data.Data.ToList()
                              select new BranchViewModel()
                              {

                                  Id = c.Id,
                                  Name = c.Name

                              }).ToList();
                BranchList = new ObservableCollection<BranchViewModel>(result);
                return BranchList;

            }
            return BranchList;
        }
    }

    
    public class BranchViewModel {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Checked { get; set; }
        public string StreetAddress { get; set; }
    }
}
