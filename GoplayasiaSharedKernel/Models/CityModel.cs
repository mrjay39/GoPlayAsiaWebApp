using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models
{
    public class CityModel
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; }
        public ProvinceModel Province { get; set; }
        public RegionModel Region { get; set; }
    }
}
