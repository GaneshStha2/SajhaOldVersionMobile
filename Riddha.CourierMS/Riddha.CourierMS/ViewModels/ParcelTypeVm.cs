using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
    public class ParcelTypeVm
    {
        public ParcelTypeViewModel PackageType { get; set; }
        public ObservableCollection<ParcelTypeViewModel> ParcelTypeList { get; set; }

        public ParcelTypeVm() : base()
        {
            PackageType = new ParcelTypeViewModel();
            ParcelTypeList = new ObservableCollection<ParcelTypeViewModel>();
        }

        public ObservableCollection<ParcelTypeViewModel> getParcelTypes()
        {
            CommonServices commonServices = new CommonServices();

            ParcelTypeList = new ObservableCollection<ParcelTypeViewModel>();
            var data = commonServices.getParcelTypeList().Result;
            
            if (data.Status == MobileResultStatus.Ok)
            {
                var result = (from c in data.Data.ToList()
                              select new ParcelTypeViewModel()
                              {

                                  Id = c.Id,
                                  Name = c.Name,
                                  PackageCharge = c.PackageCharge,
                                  ServiceCharge = c.ServiceCharge,
                                  AirPerKgRate = c.AirPerKgRate,
                                  MinimumAirRate = c.MinimumAirRate,
                                  MinimumSurfaceRate = c.MinimumSurfaceRate,
                                  SurfacePerKgRate = c.SurfacePerKgRate
                              }).ToList();
                ParcelTypeList = new ObservableCollection<ParcelTypeViewModel>(result);
                return ParcelTypeList;

            }
            return ParcelTypeList;
        }


            public ObservableCollection<ParcelTypeViewModel> getParcelTypesForDestination(int destinationId)
            {
                CommonServices commonServices = new CommonServices();

                ParcelTypeList = new ObservableCollection<ParcelTypeViewModel>();
                var data = commonServices.getParcelTypeListForDestination(destinationId).Result;

                if (data.Status == MobileResultStatus.Ok)
                {
                    var result = (from c in data.Data.ToList()
                                  select new ParcelTypeViewModel()
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                      PackageCharge = c.PackageCharge,
                                      ServiceCharge = c.ServiceCharge,
                                      AirPerKgRate = c.AirPerKgRate,
                                      MinimumAirRate = c.MinimumAirRate,
                                      MinimumSurfaceRate = c.MinimumSurfaceRate,
                                      SurfacePerKgRate = c.SurfacePerKgRate,
                                      Checked = false,
                                  }).ToList();
                    ParcelTypeList = new ObservableCollection<ParcelTypeViewModel>(result);
                    return ParcelTypeList;

                }
                return ParcelTypeList;
            }
    }

    public class ParcelTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal PackageCharge { get; set; }
        public decimal ServiceCharge { get; set; }

        public decimal MinimumAirRate { get; set; }
        public decimal MinimumSurfaceRate { get; set; }
        public decimal SurfacePerKgRate { get; set; }
        public decimal AirPerKgRate { get; set; }


        public bool Checked { get; set; }
    }
}
