using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.Base
{
    public class BaseResultDTO
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
