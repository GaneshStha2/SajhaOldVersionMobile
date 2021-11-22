using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Riddha.CourierMS.Model
{
   
    public class CourierTimeLineModel
    {
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



}
