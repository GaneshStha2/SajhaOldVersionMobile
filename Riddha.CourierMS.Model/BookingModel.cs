using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.Model
{
    public class BookingModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverContact { get; set; }

        public int ToLocationId { get; set; }
        public string ToLocationName { get; set; }
        public string ToStreetAddress { get; set; }

        public int FromLocationId { get; set; }
        public string FromLocationName { get; set; }
        public string FromStreetAddress { get; set; }

        public decimal Weight { get; set; }
        public bool IsPackingRequired { get; set; }

        public bool IsCashOnDelivery { get; set; }
        public bool IsPickUpRequired { get; set; }

        public ModeType ModeType { get; set; }

        public string TrackingNo { get; set; }
        public int NoOfPackage { get; set; }


        public decimal GrandTotal { get; set; }
        public decimal Rate { get; set; }
        public decimal Vat { get; set; }
        public decimal PackingCharge { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal TotalAmountBeforVat { get; set; }
        public BookingStatus Status { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RegdNo { get; set; }


        public int PackageTypeId { get; set; }
        public string PackageTypeName { get; set; }
    }


    public enum ModeType
    {
        Surface,
        Air
    }



    public enum BookingStatus
    {
        BookingCreated,
        PickedUp,
        Packed,
        ParcelArrived,
        Arranged,
        Dispatched,
        ReadyToDeliver,
        DeliveryInProcess,
        Delivered,
        Cancelled
    }

}
