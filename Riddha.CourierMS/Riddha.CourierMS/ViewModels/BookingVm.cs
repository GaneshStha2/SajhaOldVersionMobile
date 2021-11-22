using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Riddha.CourierMS.ViewModels
{
    public class BookingVm
    {
        public BookingTypeViewModel BookingType { get; set; }
        public ObservableCollection<BookingTypeViewModel> BookingTypeList { get; set; }
        public BookingViewModel Booking { get; set; }

        public ObservableCollection<ParcelTypeViewModel> ParcelTypeList { get; set; }

        public BookingVm() : base()
        {
            BookingType = new BookingTypeViewModel();
            BookingTypeList = new ObservableCollection<BookingTypeViewModel>();
            Booking = new BookingViewModel();
            ParcelTypeList = new ObservableCollection<ParcelTypeViewModel>();
        }


     


        public ObservableCollection<ParcelTypeViewModel> getParcelTypeList()
        {

            ParcelTypeList = new ObservableCollection<ParcelTypeViewModel>();

            ParcelTypeVm parcelTypeVm = new ParcelTypeVm();

             parcelTypeVm.getParcelTypes();

            ParcelTypeList = parcelTypeVm.ParcelTypeList;


           
            return ParcelTypeList;




        }
        public ObservableCollection<ParcelTypeViewModel> getParcelTypeListForDestination(int destinationId)
        {

            ParcelTypeList = new ObservableCollection<ParcelTypeViewModel>();

            ParcelTypeVm parcelTypeVm = new ParcelTypeVm();

             parcelTypeVm.getParcelTypesForDestination(destinationId);

            ParcelTypeList = parcelTypeVm.ParcelTypeList;



            return ParcelTypeList;




        }

    }

    public class BookingTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Icon { get; set; }
        public BookingType BookingType { get; set; }
    }

    public enum BookingType
    {
        National,
        International
    }

    public class BookingViewModel
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

        public int PackageTypeId { get; set; }
        public string PackageTypeName { get; set; }


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


        public bool IsAirMode { get; set; }
        public bool IsSurfaceMode { get; set; }
    }


}
