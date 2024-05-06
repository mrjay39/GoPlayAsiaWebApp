
using GoplayasiaBlazor.Dtos.Base;
using GoplayasiaBlazor.Dtos.DTOIn.Profile;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class AccountResultDTO : BaseResultDTO
    {
        public UserDTO User { get; set; }
        public UserDTO ParentUser { get; set; }
        public List<UserDTO> Users { get; set; }
        public List<RegionDTO> Regions { get; set; }
        public List<NationalityDTO> Nationalities { get; set; }
        public List<NatureOfWorkDTO> NaturesOfWork { get; set; }
        public List<SourceOfIncomeDTO> SourcesOfIncome { get; set; }
    }
}
