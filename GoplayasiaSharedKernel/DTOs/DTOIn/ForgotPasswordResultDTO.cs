using GoplayasiaBlazor.Dtos.Base;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class ForgotPasswordResultDTO : BaseResultDTO
    {
        public string ReferenceNo { get; set; }
        public string Code { get; set; }
        public string JWTToken { get; set; }
    }
}
