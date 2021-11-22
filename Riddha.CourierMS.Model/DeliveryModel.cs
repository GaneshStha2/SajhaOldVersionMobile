using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.Model
{
    public class DeliveryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string TrackingNo { get; set; }
        public int BookingId { get; set; }


        public int DeliveredByUserId { get; set; }
        public DateTime DeliveredDateTime { get; set; }

    }
}
