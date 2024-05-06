using GoplayasiaBlazor.Dtos.DTOIn.Profile;
using GoplayasiaBlazor.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class ConstantResultDTO
    {
        public List<RegionDTO> Regions { get; set; }
        public List<ProvinceDTO> Provinces { get; set; }
        public List<CityDTO> Cities { get; set; }
        public List<BarangayDTO> Barangays { get; set; }
        public List<NationalityDTO> Nationalities { get; set; }
        public List<NatureOfWorkDTO> NaturesOfWork { get; set; }
        public List<SourceOfIncomeDTO> SourcesOfIncome { get; set; }
        public CorporationSettingDTO CorporationSettings { get; set; }
    }
}

