using Riddha.CourierMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
    public class DeliveryVm
    {
        public DeliveryViewModel Delivery { get; set; }
        public ParcelReceiveViewModel SelectedParcel { get; set; }
        public ParcelTypeViewModel SelectedParcelForDestination { get; set; }
        public PopUpRequestedFrom PopUpRequestedFrom { get; set; }
        public ObservableCollection<ParcelReceiveViewModel> DeliveryParcelList { get; set; }
        public ObservableCollection<ParcelReceiveViewModel> PickUpParcelParcelList { get; set; }
        public ObservableCollection<ParcelTypeViewModel> AllParcelList { get; set; }
     
        public DeliveryVm() : base()
        {
            Delivery = new DeliveryViewModel();
            DeliveryParcelList = new ObservableCollection<ParcelReceiveViewModel>();
            PickUpParcelParcelList = new ObservableCollection<ParcelReceiveViewModel>();
            SelectedParcel = new ParcelReceiveViewModel();
            AllParcelList = new ObservableCollection<ParcelTypeViewModel>();
        }
         
        public ObservableCollection<ParcelReceiveViewModel> getDeliveryParcelList(int serviceCenterId)
        {

            DeliveryParcelList = new ObservableCollection<ParcelReceiveViewModel>();

            ParcelServices _parcelReceiveServices = new ParcelServices();

            var data = _parcelReceiveServices.GetParcelsToDeliveryList(serviceCenterId).Result;


            if (data.Status == Common.Services.MobileResultStatus.Ok)

            {
                var result = (from c in data.Data.ToList()
                              select new ParcelReceiveViewModel()
                              {
                                  Id = c.Id,
                                  FromLocation = c.FromLocation,
                                  ToLocation = c.ToLocation,
                                  BookingId = c.BookingId,
                                  CustomerName = c.CustomerName,
                                  SenderName = c.SenderName,
                                  TrackingNo = c.TrackingNo,
                                  ContactNo = c.ContactNo,
                              }).ToList();

                DeliveryParcelList = new ObservableCollection<ParcelReceiveViewModel>(result);

                return DeliveryParcelList;
            }

            return DeliveryParcelList;
        }

        public ObservableCollection<ParcelReceiveViewModel> getPickUpParcelParcelList(int serviceCenterId)
        {

            PickUpParcelParcelList = new ObservableCollection<ParcelReceiveViewModel>();

            ParcelServices _parcelReceiveServices = new ParcelServices();

            var data = _parcelReceiveServices.GetParcelsToPickUpList(serviceCenterId).Result;


            if (data.Status == Common.Services.MobileResultStatus.Ok)
            {
                var result = (from c in data.Data.ToList()
                              select new ParcelReceiveViewModel()
                              {
                                  Id = c.Id,
                                  FromLocation = c.FromLocation,
                                  ToLocation = c.ToLocation,
                                  BookingId = c.BookingId,
                                  CustomerName = c.CustomerName,
                                  SenderName = c.SenderName,
                                  TrackingNo = c.TrackingNo,
                                  ContactNo = c.ContactNo,
                              }).ToList();

                PickUpParcelParcelList = new ObservableCollection<ParcelReceiveViewModel>(result);

                return PickUpParcelParcelList;
            }

            return PickUpParcelParcelList;
        }

        public ObservableCollection<ParcelTypeViewModel> getParcelTypeListForDestination(int destinationId)

        {

            AllParcelList = new ObservableCollection<ParcelTypeViewModel>();

            CommonServices _commonServices = new CommonServices();

            var data = _commonServices.getParcelTypeListForDestination(destinationId).Result;


            if (data.Status == Common.Services.MobileResultStatus.Ok)
            {
                var result = (from c in data.Data.ToList()
                              select new ParcelTypeViewModel()
                              {
                                  Id = c.Id,
                                AirPerKgRate = c.AirPerKgRate,
                                MinimumAirRate = c.MinimumAirRate,
                                Checked = false,
                                MinimumSurfaceRate =c.MinimumSurfaceRate,
                                Name = c.Name,
                                PackageCharge = c.PackageCharge,
                                ServiceCharge = c.ServiceCharge,
                                SurfacePerKgRate = c.SurfacePerKgRate,
                              }).ToList();

                AllParcelList = new ObservableCollection<ParcelTypeViewModel>(result);

                return AllParcelList;
            }

            return AllParcelList;
        }


    }

    public class DeliveryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string TrackingNo { get; set; }
        public int BookingId { get; set; }

        public int DeliveredByUserId { get; set; }
        public DateTime DeliveredDateTime { get; set; }

        /// <summary>
        /// used for popup in parcel Estimate
        /// </summary>
        public int DestinationId { get; set; }
    }


    public enum ParcelPopUpType
    {
        PickUp,
        Delivery,
        Estimate
    }
}
