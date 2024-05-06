using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOOut
{
    public class ForgotPasswordParamsDTO
    {
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string ReferenceNo { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string JWTToken { get; set; }
    }
}
