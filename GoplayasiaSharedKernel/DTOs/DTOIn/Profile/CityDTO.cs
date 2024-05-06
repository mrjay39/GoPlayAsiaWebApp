using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOIn.Profile
{
    public class CityDTO
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; }
        public ProvinceDTO Province { get; set; }
        public RegionDTO Region { get; set; }
    }
}
