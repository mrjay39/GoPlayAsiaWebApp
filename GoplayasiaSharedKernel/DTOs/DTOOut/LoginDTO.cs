using System.ComponentModel.DataAnnotations;

namespace GoplayasiaBlazor.Dtos.DTOOut
{
    public class LoginDTO
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? DeviceToken { get; set; }

    }
}
