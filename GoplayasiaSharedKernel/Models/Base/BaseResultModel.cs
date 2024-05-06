using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models.Base
{
    public class BaseResultModel
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
