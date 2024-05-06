using GoplayasiaBlazor.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class RegisterResultDTO : BaseResultDTO
    {
        public UserDTO User { get; set; }
    }
}
