using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Model;
using Riddha.CourierMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
    public class ParcelDispatchVm
    {
        public ParcelReceiveViewModel ParcelDispatch { get; set; }
        public ObservableCollection<ParcelReceiveViewModel> DispatchedList { get; set; }
        public ParcelDispatchVm() :base()
        {
            ParcelDispatch = new ParcelReceiveViewModel();
            DispatchedList = new ObservableCollection<ParcelReceiveViewModel>();
        }

        public ParcelReceiveViewModel getParcelDispatch(string code)
        {
            ParcelDispatch = new ParcelReceiveViewModel();
            ParcelServices _parcelServices = new ParcelServices();

            var data = _parcelServices.GetParcelDispatch(code).Result;

            if(data.Status == MobileResultStatus.Ok)
            {
                ParcelDispatch = new ParcelReceiveViewModel()
                {
                    BookingId = data.Data.BookingId,
                    Id = data.Data.Id,
                    //ContactNo = data.Data.ContactNo,
                    //CustomerName = data.Data.CustomerName,
                    //FromLocation = data.Data.FromLocation,
                    //LocationName = data.Data.LocationName,
                    //LocationId = data.Data.LocationId,
                    //ToLocation = data.Data.ToLocation,
                    TrackingNo = data.Data.TrackingNo,
                    ManifestId = data.Data.ManifestId
                };
            }
            else
            {
                return ParcelDispatch;
            }

            return ParcelDispatch;
        }

        public ParcelReceiveViewModel DispatchParcel(string code)
        {
            ParcelDispatch = new ParcelReceiveViewModel();
            ParcelServices _parcelServices = new ParcelServices();

            var data = _parcelServices.GetParcelDispatch(code).Result;

            return new ParcelReceiveViewModel();
        }



        public ParcelReceiveViewModel ParseParcelReceiveModelToParcelReceiveViewModel(ParcelReceiveModel model)
        {
            ParcelReceiveViewModel receiveVm = new ParcelReceiveViewModel()
            {
                BookingId = model.BookingId,
                ContactNo = model.ContactNo,
                CustomerName = model.CustomerName,
                FromLocation = model.FromLocation,
                Id = model.Id,
                LocationId = model.LocationId,
                LocationName = model.LocationName,
                ManifestId = model.ManifestId,
                ReceivedByUserId = model.ReceivedByUserId,
                ParcelType = model.ParcelType,
                ReceiverDate = model.ReceiverDate,
                TrackingNo = model.TrackingNo,
                ToLocation = model.ToLocation
            };


            return receiveVm;
        }
    }

    public class ParcelDispatchViewModel
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
