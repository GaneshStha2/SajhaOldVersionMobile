using Riddha.CourierMS.Common.Model;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riddha.CourierMS.Services
{
    public class UserInformationServices
    {
        WebrequestService wrs;
        public async Task<ServiceResult<UserModel>> getUserInformationByUserId(int userId)
        {
            wrs = new WebrequestService();
            var data = await wrs.Get<ServiceResult<UserModel>>("Api/MLoginApi/GetUserInformationById?userId=" + userId );
            return new ServiceResult<UserModel>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
                Token = data.Token

            };


        }

    }
}
