using GoplayasiaBlazor.Dtos.DTOIn.Profile;
using GoplayasiaBlazor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class UserDTO
    {
        public long Id { get; set; }
        public int RoleType { get; set; }
        public long? ParentUserId { get; set; }
        public string ReferralKey { get; set; }
        public string DeviceToken { get; set; }
        public long? ProfileImageId { get; set; }
        public long? GovernmentImageId { get; set; }
        public string IdentificationNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; } 
        public string PlaceOfBirth { get; set; }
        public string Gender { get; set; }
        public int? SourceOfIncomeId { get; set; }
        public string OtherSourceOfIncome { get; set; }
        public int? NatureOfWorkId { get; set; }
        public string OtherNatureOfWork { get; set; }
        public int? NationalityId { get; set; }
        public string EmailAddress { get; set; }
        public bool EmailValidated { get; set; }
        public string MobileNumber { get; set; }
        public bool MobileNumberValidated { get; set; }
        public int? PermanentRegionId { get; set; }
        public int? PermanentProvinceId { get; set; }
        public int? PermanentCityId { get; set; }
        public int? PermanentBarangayId { get; set; }
        public string PermanentStreet { get; set; }
        public bool? AddressAreSame { get; set; }
        public int? CurrentRegionId { get; set; }
        public int? CurrentProvinceId { get; set; }
        public int? CurrentCityId { get; set; }
        public int? CurrentBarangayId { get; set; }
        public string CurrentStreet { get; set; }
        public string Code { get; set; }
        public DateTime? CodeExpriry { get; set; }
        public decimal? Credits { get; set; }
        public decimal? HoldCredits { get; set; }
        public DateTime? GameViewTimer { get; set; }
        public int Status { get; set; }
        public bool ToppedUp { get; set; }
        public DateTime? LastDateLogin { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? ModifiedById { get; set; }

        public BarangayDTO CurrentBarangay { get; set; }
        public CityDTO CurrentCity { get; set; }
        public ProvinceDTO CurrentProvince { get; set; }
        public RegionDTO CurrentRegion { get; set; }
        public BarangayDTO PermanentBarangay { get; set; }
        public CityDTO PermanentCity { get; set; }
        public ProvinceDTO PermanentProvince { get; set; }
        public RegionDTO PermanentRegion { get; set; }
        public UploadDTO ProfileImage { get; set; }
        public UploadDTO GovernmentImage { get; set; }

        public string JWTToken { get; set; }
        public string FullName { get; set; }
        public string ProfileImageFullPath { get; set; }
        public string GovernmentImageFullPath { get; set; }
        public TimeSpan RemainingViewTime { get; set; }
        public string OldPassword { get; set; }
        public int OnHoldGameTypeId { get; set; }
        public int PopupTimer { get; set; }
        public int IdleTimer { get; set; }

        public static implicit operator UserDTO(UserModel v)
        {
            throw new NotImplementedException();
        }
        public int Verified { get; set; }
    }
}
