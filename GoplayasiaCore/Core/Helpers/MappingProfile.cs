using AutoMapper;
using GoplayasiaBlazor.Dtos.Base;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Dtos.DTOIn.Profile;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.DTOs;
using GoplayasiaBlazor.DTOs.DTOIn;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Base;
using GoplayasiaBlazor.Models.Incoming;

namespace GoplayasiaBlazor.Core.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegionDTO, RegionModel>().ReverseMap();
            CreateMap<ProvinceDTO, ProvinceModel>().ReverseMap();
            CreateMap<CityDTO, CityModel>().ReverseMap();
            CreateMap<BarangayDTO, BarangayModel>().ReverseMap();
            CreateMap<NationalityDTO, NationalityModel>().ReverseMap();
            CreateMap<NatureOfWorkDTO, NatureOfWorkModel>().ReverseMap();
            CreateMap<SourceOfIncomeDTO, SourceOfIncomeModel>().ReverseMap();
            CreateMap<CorporationSettingDTO, CorporationSettingModel>().ReverseMap();
            CreateMap<UploadDTO, UploadModel>().ReverseMap();
            CreateMap<UserDTO, UserModel>().ReverseMap();
            CreateMap<SignupDTO, UserModel>().ReverseMap();
            CreateMap<TransactionDTO, TransactionModel>().ReverseMap();
            CreateMap<TransactionResultDTO, TransactionResultModel>().ReverseMap();
            CreateMap<BetDTO, BetModel>().ReverseMap();

            CreateMap<GameChipDTO, GameChipModel>().ReverseMap();
            CreateMap<GameRoundDTO, GameRoundModel>().ReverseMap();
            CreateMap<GameSettingDTO, GameSettingModel>().ReverseMap();
            CreateMap<GameTypeDTO, GameTypeModel>().ReverseMap();
            CreateMap<BetResultDTO, BetResultModel>().ReverseMap();
            CreateMap<BaseResultDTO, BaseResultModel>().ReverseMap();
            CreateMap<AccountResultDTO, AccountResultModel>().ReverseMap();

            CreateMap<Go12BetsDTO, Go12BetsModel>().ReverseMap();
            CreateMap<Go12DTO, Go12Model>().ReverseMap();
            CreateMap<RoundBetsDetailDTO, RoundBetsDetailModel>().ReverseMap();
            CreateMap<MobileAppVersionResultDTO, MobileAppVersionResultModel>().ReverseMap();

            CreateMap<ApprovalDTO, ApprovalModel>().ReverseMap();
            CreateMap<NotificationDTO, NotificationModel>().ReverseMap();
            CreateMap<UserStatisticsResultDTO, UserStatisticsResultModel>().ReverseMap();
            CreateMap<CashinCashoutSettingsDTO, CashinCashoutSettings>().ReverseMap();

            CreateMap<L9GameRoundDTO, L9GameRoundModel>().ReverseMap();
            CreateMap<L9BetResultModel, L9BetResultDTO>().ReverseMap();
            CreateMap<L9BetModel, L9BetDTO>().ReverseMap();
            CreateMap<GameChipModel, GameVariantChipsModel>().ReverseMap();

            CreateMap<F3GameRoundDTO, F3GameRoundModel>().ReverseMap();
            CreateMap<F3BetResultModel, F3BetResultDTO>().ReverseMap();
            CreateMap<F3BetModel, F3BetDTO>().ReverseMap();

            CreateMap<DiceGameRoundDTO, DiceGameRoundModel>().ReverseMap();
            CreateMap<DiceBetModel, DiceBetDTO>().ReverseMap();
            //CreateMap<CurrencyResultDTO, CurrencyResultModel>().ReverseMap();
        }
    }
}
