using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.Model
{
    public class ParcelTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PackageCharge { get; set; }
        public decimal ServiceCharge { get; set; }


        public decimal MinimumAirRate { get; set; }
        public decimal MinimumSurfaceRate { get; set; }
        public decimal SurfacePerKgRate { get; set; }
        public decimal AirPerKgRate { get; set; }

    }


   
}
