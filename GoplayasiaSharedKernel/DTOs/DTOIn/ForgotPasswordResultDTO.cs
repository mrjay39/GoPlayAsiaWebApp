using GoplayasiaBlazor.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class ForgotPasswordResultDTO : BaseResultDTO
    {
        public string ReferenceNo { get; set; }
        public string Code { get; set; }
        public string JWTToken { get; set; }
    }
}
