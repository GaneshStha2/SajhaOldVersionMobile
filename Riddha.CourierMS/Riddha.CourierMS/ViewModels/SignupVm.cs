using Riddha.CourierMS.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.ViewModels
{
    public class SignupVm
    {
        public SignUpViewModel SignUpViewModel { get; set; }

        public SignupVm() : base()
        {
            SignUpViewModel = new SignUpViewModel();

        }

        public  string sendOtpCodeforverification(string mobilnumber)
        {
            SignUpViewModel.OtpCode = "";
            Services.CommonServices commonServices = new Services.CommonServices();
            var otpresult = commonServices.SendVerificationCodeByMobileNumber(mobilnumber).Result;
            if (otpresult.Status == MobileResultStatus.Ok)
            {
                SignUpViewModel.OtpCode = otpresult.Data;


            }
            return SignUpViewModel.OtpCode;
        }
    }
    public class SignUpViewModel
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string OtpCode { get; set; }
        public string ProfilePhoto { get;  set; }
        public string Address { get;  set; }
    }
   
}
