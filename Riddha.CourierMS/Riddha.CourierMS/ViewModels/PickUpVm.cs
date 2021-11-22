using Riddha.CourierMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
    public class PickUpVm
    {
        public PickUpViewModel PickUp { get; set; }
        public ParcelReceiveViewModel SelectedParcel { get; set; }
        public ObservableCollection<ParcelReceiveViewModel> PickUpParcelParcelList { get; set; }


        public PickUpVm() : base()
        {
            PickUp = new PickUpViewModel();
            SelectedParcel = new ParcelReceiveViewModel();
            PickUpParcelParcelList = new ObservableCollection<ParcelReceiveViewModel>();
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
    }


    public class PickUpViewModel
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
