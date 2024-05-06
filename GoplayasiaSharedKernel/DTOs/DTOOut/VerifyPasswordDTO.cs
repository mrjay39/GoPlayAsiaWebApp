using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.DTOs.DTOOut
{
    public class VerifyPasswordDTO
    {
        public long UserId { get; set; }     
        public string Password { get; set; }
    }
}
