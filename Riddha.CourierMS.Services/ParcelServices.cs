using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riddha.CourierMS.Services
{
    public class ParcelServices
    {
        WebrequestService wrs;
        public async Task<ServiceResult<ParcelReceiveModel>> GetParcelReceive(string trackingNO)
        {


            wrs = new WebrequestService();
            //var data = await wrs.Post<ServiceResult<LoginModel>>("Api/MLogin/Login", wrs.SerializeObject<LoginModel>(model));
            ServiceResult<ParcelReceiveModel> data = new ServiceResult<ParcelReceiveModel>();
            data.Data = new ParcelReceiveModel()
            {
                BookingId = 1,
                CustomerName = "Umesh",
                FromLocation = "Kathmandu",
                LocationId = 4,
                LocationName = "Dharan",
                ReceivedByUserId = 1,
                ToLocation = "Dharan",
                TrackingNo = "#Ktm-0005",
                ContactNo = "98510525478"
            };

            data.Message = "";
            data.Status = MobileResultStatus.Ok;
            return new ServiceResult<ParcelReceiveModel>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
                Token = data.Token

            };
        }

        public async Task<ServiceResult<ParcelReceiveModel>> GetParcelDispatch(string trackingNO)
        {


            wrs = new WebrequestService();
            //var data = await wrs.Post<ServiceResult<LoginModel>>("Api/MLogin/Login", wrs.SerializeObject<LoginModel>(model));
            ServiceResult<ParcelReceiveModel> data = new ServiceResult<ParcelReceiveModel>();
            data.Data = new ParcelReceiveModel()
            {
                BookingId = 1,
                CustomerName = "Umesh",
                FromLocation = "Kathmandu",
                LocationId = 4,
                LocationName = "Dharan",
                ReceivedByUserId = 1,
                ToLocation = "Dharan",
                TrackingNo = "#Ktm-0005",
                ContactNo = "98510525478",
                ManifestId = 5
            };

            data.Message = "";
            data.Status = MobileResultStatus.Ok;

            return new ServiceResult<ParcelReceiveModel>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
                Token = data.Token
            };
        }



        public async Task<ServiceResult<List<CourierTimeLineModel>>> CourierTimeLine(string trackingNo)
        {
            wrs = new WebrequestService();
            var data = await wrs.Get<ServiceResult<List<CourierTimeLineModel>>>("Api/MParcelApi/GetCourierTimeLine?trackingNo=" + trackingNo);

            return new ServiceResult<List<CourierTimeLineModel>>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
                Token = data.Token
            };
        }
        public async Task<ServiceResult<ParcelReceiveModel>> ReceiveParcel(ParcelReceiveModel model)
        {
    

            wrs = new WebrequestService();
            var data = await wrs.Post<ServiceResult<ParcelReceiveModel>>("Api/MParcelApi/ReceiveParcel", wrs.SerializeObject<ParcelReceiveModel>(model));

            return new ServiceResult<ParcelReceiveModel>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
                Token = data.Token
            };
        }

        public async Task<ServiceResult<List<ParcelReceiveModel>>> ReceiveParcelByManifestNo(ParcelReceiveModel model)
        {


            wrs = new WebrequestService();
            var data = await wrs.Post<ServiceResult<List<ParcelReceiveModel>>>("Api/MParcelApi/ReceiveParcelByManifestNo", wrs.SerializeObject<ParcelReceiveModel>(model));

            return new ServiceResult<List<ParcelReceiveModel>>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
                Token = data.Token
            };
        }


        public async Task<ServiceResult<ParcelReceiveModel>> DispatchParcelByBookingNumber(ParcelDispatchModel model)
        {

            wrs = new WebrequestService();
            var data = await wrs.Post<ServiceResult<ParcelReceiveModel>>("Api/MParcelApi/DispatchParcelByBookingNumber", wrs.SerializeObject<ParcelDispatchModel>(model));

            return new ServiceResult<ParcelReceiveModel>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
                Token = data.Token
            };
        }

        public async Task<ServiceResult<List<ParcelReceiveModel>>> DispatchParcelByManifestNumber(ParcelDispatchModel model)
        {

            wrs = new WebrequestService();
            var data = await wrs.Post<ServiceResult<List<ParcelReceiveModel>>>("Api/MParcelApi/DispatchParcelByManifestNumber", wrs.SerializeObject<ParcelDispatchModel>(model));

            return new ServiceResult<List<ParcelReceiveModel>>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
                Token = data.Token
            };
        }

        public async Task<ServiceResult<BookingModel>> CreateBooking(BookingModel model)
        {
            wrs = new WebrequestService();
            var data = await wrs.Post<ServiceResult<BookingModel>>("Api/MBookingApi/CreateBooking", wrs.SerializeObject<BookingModel>(model));
            return new ServiceResult<BookingModel>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
            };

        }

        public async Task<ServiceResult<DeliveryModel>> DeliverParcel(DeliveryModel model)
        {
            wrs = new WebrequestService();
            var data = await wrs.Post<ServiceResult<DeliveryModel>>("Api/MParcelApi/DeliverParcel", wrs.SerializeObject<DeliveryModel>(model));

            return new ServiceResult<DeliveryModel>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
                Token = data.Token
            };

        }

        public async Task<ServiceResult<PickUpModel>> PickUpParcel(PickUpModel model)
        {
            wrs = new WebrequestService();
            var data = await wrs.Post<ServiceResult<PickUpModel>>("Api/MParcelApi/PickUpParcel", wrs.SerializeObject<PickUpModel>(model));

            return new ServiceResult<PickUpModel>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
                Token = data.Token
            };

        }


        public async Task<ServiceResult<List<ParcelReceiveModel>>> GetParcelsToDeliveryList(int serviceCenterId)
        {
            wrs = new WebrequestService();
            var data = await wrs.Get<ServiceResult<List<ParcelReceiveModel>>>("Api/MParcelApi/GetParcelsToDeliveryList?serviceCenterId=" + serviceCenterId);

            return new ServiceResult<List<ParcelReceiveModel>>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
            };
        }


        public async Task<ServiceResult<List<ParcelReceiveModel>>> GetParcelsToPickUpList(int serviceCenterId)
        {
            wrs = new WebrequestService();
            var data = await wrs.Get<ServiceResult<List<ParcelReceiveModel>>>("Api/MParcelApi/GetParcelsToPickUpList?serviceCenterId=" + serviceCenterId);

            return new ServiceResult<List<ParcelReceiveModel>>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
            };
        }

    }
}
