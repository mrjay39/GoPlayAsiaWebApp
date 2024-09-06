
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
        public NationalityDTO Nationality { get; set; }
        public NatureOfWorkDTO NaturesOfWork { get; set; }
        public SourceOfIncomeDTO SourcesOfIncome { get; set; }
    }
}
