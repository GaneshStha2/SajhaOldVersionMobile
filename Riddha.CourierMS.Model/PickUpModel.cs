using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.Model
{
    public class PickUpModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string TrackingNo { get; set; }
        public int BookingId { get; set; }


        public int PickedUpByUserId { get; set; }
        public DateTime PickedUpDateTime { get; set; }
    }
}
