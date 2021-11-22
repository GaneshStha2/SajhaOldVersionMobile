using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Riddha.CourierMS.Services
{
    public class CommonServices
    {
        WebrequestService wrs;

        public async Task<ServiceResult<string>> SendVerificationCodeByMobileNumber(string mobileNumber)
        {
            wrs = new WebrequestService();
            var data = await wrs.Get<ServiceResult<string>>("Api/MLoginApi/GetOtpCode?mobileNumber=" + mobileNumber);

            return new ServiceResult<string>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
            };

        }


        public async Task<ServiceResult<List<BranchModel>>> getBranchList()
        {
            WebrequestService wrs = new WebrequestService();

            var data = await wrs.Get<ServiceResult<List<BranchModel>>>("Api/MParcelApi/getBranchList");

            return new ServiceResult<List<BranchModel>>
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
            };
        }

        public async Task<ServiceResult<List<ParcelTypeModel>>> getParcelTypeList()
        {
            WebrequestService wrs = new WebrequestService();

            var data = await wrs.Get<ServiceResult<List<ParcelTypeModel>>>("Api/MParcelTypeApi/getParcelTypeList");

            return new ServiceResult<List<ParcelTypeModel>>
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
            };
        }

        public async Task<ServiceResult<List<ParcelTypeModel>>> getParcelTypeListForDestination(int destinationId)
        {
            WebrequestService wrs = new WebrequestService();

            var data = await wrs.Get<ServiceResult<List<ParcelTypeModel>>>("Api/MParcelTypeApi/getParcelTypeListForDestination?destnationId=" + destinationId);

            return new ServiceResult<List<ParcelTypeModel>>
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
            };
        }


        public async Task<ServiceResult<List<CountryModel>>> getCountryList()
        {
            WebrequestService wrs = new WebrequestService();

            var data = await wrs.Get<ServiceResult<List<CountryModel>>>("Api/MParcelApi/getCountryList");

            return new ServiceResult<List<CountryModel>>
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
            };
        }

        public async Task<ServiceResult<List<CustomerModel>>> getRecentTransactionCustomerList(int customerId)
        {
            WebrequestService wrs = new WebrequestService();

            var data = await wrs.Get<ServiceResult<List<CustomerModel>>>("Api/MCustomerApi/GetRecentTransactionCustomerList?customerId=" + customerId);

            return new ServiceResult<List<CustomerModel>>
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
            };
        }





    }
}
