using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
    public class NationalBookingVm
    {
        public NationalBookingViewModel NationalBooking { get; set; }
        public ObservableCollection<BranchViewModel> BranchList { get; set; }
        public BranchVm BranchVm { get; set; }


        public NationalBookingVm() : base()
        {
            BranchVm = new BranchVm();
            BranchList = new ObservableCollection<BranchViewModel>();
        }
    }


    public class NationalBookingViewModel
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
    }
}
