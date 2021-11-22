using Riddha.CourierMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
   public class TrackingVm
    {
        public ObservableCollection <CourierTimeLineViewModel> TimeLineList { get; set; }
        public TrackingViewModel TimeLine { get; set; }
        public string BackBtnSource { get; set; }
        public TrackingVm() : base(){
            TimeLineList = new ObservableCollection<CourierTimeLineViewModel>();
            TimeLine = new TrackingViewModel();
        }

        public ObservableCollection<CourierTimeLineViewModel> GetTimeLineByTrackingNo(string trackingNo)
        {
            ParcelServices parcelServices = new ParcelServices();
            TimeLineList = new ObservableCollection<CourierTimeLineViewModel>();

            var DATA = parcelServices.CourierTimeLine(trackingNo).Result;
            if (DATA.Data.Count > 0)
            {
                var RESULT = (from c in DATA.Data.ToList()
                              select new CourierTimeLineViewModel()
                              {
                                  BookingStatus = c.BookingStatus,
                                  Date = c.Date,
                                  DispactchedLocation = c.DispactchedLocation,
                                  ReceivedBy = c.ReceivedBy,
                                  ServiceCenter = c.ServiceCenter,
                                  ServiceCenterCode = c.ServiceCenterCode,
                                  TrackingNo = c.TrackingNo,
                                  UpdatedBy = c.UpdatedBy,
                                  UpdatedTime = c.UpdatedTime,
                                  
                              }).ToList();

                TimeLineList = new ObservableCollection<CourierTimeLineViewModel>(RESULT);

            }
            return TimeLineList;
        }
    }

    public class CourierTimeLineViewModel {

        public string BookingStatus { get; set; }
        public string UpdatedTime { get; set; }
        public string Date { get; set; }
        public string UpdatedBy { get; set; }
        public string ServiceCenter { get; set; }
        public string ServiceCenterCode { get; set; }
        public string DispactchedLocation { get; set; }
        public string ReceivedBy { get; set; }
        public string TrackingNo { get; set; }

    }
    public class TrackingViewModel {
        public string TrackingNo { get; set; }

    }

}
