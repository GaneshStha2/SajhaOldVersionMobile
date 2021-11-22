using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riddha.CourierMS.Services
{

    public class TransactionServices
    {

        WebrequestService wrs;

        public async Task<ServiceResult<List<TransactionModel>>> getRecentTransactionByUserId(int userId)
        {
            WebrequestService wrs = new WebrequestService();

            var data = await wrs.Get<ServiceResult<List<TransactionModel>>>("Api/MParcelAPI/getRecentTransactionByUserId?userId=" + userId); ;

            return new ServiceResult<List<TransactionModel>>
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
            };
        }
        
        public async Task<ServiceResult<List<ParcelReceiveModel>>> getCreditStatement(int customerId)
        {
            WebrequestService wrs = new WebrequestService();

            var data = await wrs.Get<ServiceResult<List<ParcelReceiveModel>>>("Api/MBookingApi/GetCreditStatement?CustomerId=" + customerId); ;

            return new ServiceResult<List<ParcelReceiveModel>>
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
            };
        }


    }
}
