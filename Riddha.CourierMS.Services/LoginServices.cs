using Riddha.CourierMS.Common.Model;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riddha.CourierMS.Services
{
    public class LoginServices
    {
        WebrequestService wrs;

        public async Task<ServiceResult<LoginModel>> Login(LoginModel model)
        {
            wrs = new WebrequestService();
            var data = await wrs.Post<ServiceResult<LoginModel>>("Api/MLoginApi/Login", wrs.SerializeObject<LoginModel>(model));
            return new ServiceResult<LoginModel>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
                Token = data.Token
            };
        }
    }
}
