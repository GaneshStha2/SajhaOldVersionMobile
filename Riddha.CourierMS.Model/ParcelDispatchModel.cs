using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.Model
{
    public class ParcelDispatchModel
    {
        public int Id { get; set; }
        public string TrackingNo { get; set; }
        public string BookingId { get; set; }
        public int DestinationId { get; set; }
        public int ManifestId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int ServiceCenterId { get; set; }


        public int ReceivedByUserId { get; set; }
        public DateTime ReceiverDate { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public string CustomerName { get; set; }
        public string ParcelType { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }

    }
}
