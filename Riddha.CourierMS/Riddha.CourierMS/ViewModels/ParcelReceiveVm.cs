using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.Model;
using Riddha.CourierMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Riddha.CourierMS.ViewModels
{
    public class ParcelReceiveVm
    {

        public ParcelReceiveViewModel ParcelReceive { get; set; }
        public ObservableCollection<ParcelReceiveViewModel> ParcelReceivedList { get; set; }

        public ParcelReceiveVm() : base()
        {
            ParcelReceive = new ParcelReceiveViewModel();
            ParcelReceivedList = new ObservableCollection<ParcelReceiveViewModel>();
        }

        public ParcelReceiveViewModel getpaercelReceive(string trackingno)
        {
            ParcelReceive = new ParcelReceiveViewModel();
            ParcelServices parcelReceiveServices = new ParcelServices();
            var data = parcelReceiveServices.GetParcelReceive(trackingno).Result;
            if (data.Status == Common.Services.MobileResultStatus.Ok)
            {
                ParcelReceive.BookingId = data.Data.BookingId;
                ParcelReceive.CustomerName = data.Data.CustomerName;
                ParcelReceive.FromLocation = data.Data.FromLocation;
                ParcelReceive.Id = data.Data.Id;
                ParcelReceive.TrackingNo = data.Data.TrackingNo;
                ParcelReceive.FromLocation = data.Data.FromLocation;
                ParcelReceive.ToLocation = data.Data.ToLocation;
                //ParcelReceive.ContactNo = data.Data.ContactNo;
            }
            else
            {
                return ParcelReceive;
            }


            return ParcelReceive;



        }




        public ParcelReceiveViewModel ParseToParcelReceiveViewModel(ParcelReceiveModel model)
        {
            ParcelReceiveViewModel vm = new ParcelReceiveViewModel()
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
                ToLocation = model.ToLocation,
            };

            return vm;
        }

        public  void Print()
        {

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("<br>" + "Shajha Courier" + "<br>");
            stringBuilder.AppendLine("<br>" + "Received" + "<br>");


            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("");
            //DependencyService.Get<IBlueToothService>().Print(stringBuilder);

        }
    }



    public class ParcelReceiveViewModel
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int ManifestId { get; set; }
        public string TrackingNo { get; set; }

        public int ReceivedByUserId { get; set; }
        public DateTime ReceiverDate { get; set; }

        public int LocationId { get; set; }
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
