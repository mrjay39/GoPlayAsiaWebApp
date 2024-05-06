using GoplayasiaBlazor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.DTOs.DTOOut
{
    public class SignupDTO
    {
        public long? Id { get; set; }
        public int RoleType { get; set; }
        public string ReferralKey { get; set; }
        public string DeviceToken { get; set; }
        public string IdentificationNumber { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Year { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Gender { get; set; }
        public int? SourceOfIncomeId { get; set; }
        public string OtherSourceOfIncome { get; set; }
        public int? NatureOfWorkId { get; set; }
        public string OtherNatureOfWork { get; set; }
        public int? NationalityId { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public bool MobileNumberValidated { get; set; }
        public int? CurrentCityId { get; set; }
        public string CurrentProvince { get; set; } //cjpvaquilar: add
        public string CurrentRegion { get; set; } //cjpvaquilar: add
        public string CurrentStreet { get; set; } 
        public bool? AddressAreSame { get; set; }
        public int? PermanentCityId { get; set; }
        public string PermanentProvince { get; set; } //cjpvaquilar: add
        public string PermanentRegion { get; set; } //cjpvaquilar: add
        public string PermanentStreet { get; set; }
        public UploadModel ProfileImage { get; set; }
        public UploadModel GovernmentImage { get; set; }
       
        public List<SurveyDTO> Survey { get; set; }
    }
    public class SurveyDTO
    {
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public string? Others { get; set; }
    }
}
