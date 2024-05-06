
using GoplayasiaBlazor.Dtos.Base;
using GoplayasiaBlazor.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class TransactionResultDTO : BaseResultDTO
    {
        public TransactionDTO Transaction { get; set; }
    }
}
