using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riddha.CourierMS.Services
{
    public class SignUpServices
    {
        WebrequestService wrs;

        public async Task<ServiceResult<SignUpModel>> RegisterUser(SignUpModel model)
        {
            wrs = new WebrequestService();
            var data = await wrs.Post<ServiceResult<SignUpModel>>("Api/MLoginApi/CustomerSignUp", wrs.SerializeObject<SignUpModel>(model));
            return new ServiceResult<SignUpModel>()
            {
                Data = data.Data,
                Message = data.Message,
                Status = data.Status,
                Token = data.Token

            };


        }
    }
}
