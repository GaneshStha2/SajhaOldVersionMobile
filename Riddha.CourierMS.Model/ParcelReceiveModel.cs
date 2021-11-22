using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.Model
{
    public class ParcelReceiveModel
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string TrackingNo { get; set; }

        public int ReceivedByUserId { get; set; }
        public DateTime ReceiverDate { get; set; }

        public int LocationId { get; set; }
        public int ManifestId { get; set; }
        public string LocationName { get; set; }




        public string CustomerName { get; set; }
        public string SenderName { get; set; }
        public string ContactNo { get; set; }
        public string ParcelType { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }

        public decimal Amount { get; set; }
    }
}
