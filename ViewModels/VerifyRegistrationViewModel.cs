using AutoMapper;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.ViewModels
{
    public class VerifyRegistrationViewModel : BaseViewModel
    {
        #region Local Variable & Properties
        IModalService _popupModal;
        IJSRuntime _jsruntime { get; set; }
        //IAccountService _iaccountService;
        IMapper _mapper { get; set; }

        private UserDTO _userInfo { get; set; }
        public UserDTO UserInfo
        {
            get => _userInfo;
            set
            {
                _userInfo = value;
            }
        }


        public bool hasProfimg { get; set; } = false;
        public bool hasGovtimg { get; set; } = false;

        private byte[] _profileImageBytes;
        private byte[] _governmentImageBytes;
        public byte[] ProfileImageBytes
        {
            get => _profileImageBytes;
            set
            {
                _profileImageBytes = value;
            }
        }
        public byte[] GovernmentImageBytes
        {
            get => _governmentImageBytes;
            set
            {
                _governmentImageBytes = value;
            }
        }
        public string errEmail { get; set; } = "Ok";
        #endregion

        #region Life cycle methods
        public VerifyRegistrationViewModel(IAccountService accountService, IToastService toastService, ICurrentUser currentUser, IModalService popupModal, IJSRuntime JSRuntime, NavigationManager navigationManager, IAccountService iaccountService, 
            AuthenticationStateProvider AuthenticationStateProvider)
        {
            _toastService = toastService;
            _icurrentUser = currentUser;
            _popupModal = popupModal;
            _jsruntime = JSRuntime;
            _navigationManager = navigationManager;
            _iaccountService = iaccountService;
            _AuthenticationStateProvider = AuthenticationStateProvider;
        }
        #endregion

        #region Local Methods

        public async Task<UserDTO> GetUserInfo()
        {
            var user = await _iaccountService.GetUser();
            if (user == null || user.User == null || user.User.Id < 1)
            {
                _toastService.ShowError("Failed to fetch user information");
                return default;
            }

            UserInfo = user.User;
            return UserInfo;
        }

        #region STEP 1 VALIDATIONS
        public async Task<bool> Step1Validation(SignupDTO param)
        {
            if (UserInfo.FirstName == param.FirstName &&
                UserInfo.LastName == param.LastName &&
                UserInfo.DateOfBirth?.ToString("MM") == param.Month &&
                UserInfo.DateOfBirth?.ToString("dd") == param.Day &&
                UserInfo.DateOfBirth?.ToString("yyyy") == param.Year &&
                hasProfimg)
            {
                return true;
            }
            else
            {
                if (string.IsNullOrEmpty(param.FirstName))
                {
                    _toastService.ShowError("First Name cannot be empty");
                    return false;
                }

                if (string.IsNullOrEmpty(param.LastName))
                {
                    _toastService.ShowError("Last Name cannot be empty");
                    return false;
                }
                if (string.IsNullOrEmpty(param.Month) || param.Month.Length != 2 || !int.TryParse(param.Month, out int monthResult))
                {
                    _toastService.ShowError("Birthdate month cannot be empty and must be valid (e.g. 07)");
                    return false;
                }
                if (string.IsNullOrEmpty(param.Day) || param.Day.Length != 2 || !int.TryParse(param.Day, out int dayResult))
                {
                    _toastService.ShowError("Birthdate day cannot be empty and must be valid (e.g. 02)");
                    return false;
                }
                if (string.IsNullOrEmpty(param.Year) || param.Year.Length != 4 || !int.TryParse(param.Year, out int yearResult))
                {
                    _toastService.ShowError("Birthdate year cannot be empty and must be valid (e.g. 2000)");
                    return false;
                }
                if (!await ValidatorHelper.ValidateDate(monthResult, dayResult, yearResult))
                {
                    _toastService.ShowError("Birthdate is not a valid date");
                    return false;
                }
                param.DateOfBirth = new DateTime(yearResult, monthResult, dayResult);
                if (!await ValidatorHelper.ValidateAge(21, param.DateOfBirth.Value))
                {
                    _toastService.ShowError("Age is not acceptable");
                    return false;
                }
                if (!hasProfimg)
                {
                    _toastService.ShowError("Profile Image is needed");
                    return false;
                }
            }

            try
            {
                bool res = await _iaccountService.PersonTaken(param.FirstName, param.LastName, param.DateOfBirth.Value.ToString("yyy-MM-dd"), param.RoleType);
                if (res == false)
                {
                    _toastService.ShowError("Name already registered...");
                    return false;
                }
            }
            catch (Exception)
            {
                _toastService.ShowError("Connection to server failed. Make sure you are connected to internet");
                return false;
            }

            return true;
        }
        #endregion

        #region STEP 2 VALIDATIONS
        public async Task<string> ValidateEmail(string email)
        {
            var errorMsg = "Ok";

            if (string.IsNullOrEmpty(email) || email.Contains(" ") && errorMsg == "Ok")
            {
                errorMsg = "Email Address cannot be empty, cannot contain spaces and must be valid";
                return errorMsg;
            }

            if (string.IsNullOrEmpty(email) && errorMsg == "Ok")
            {
                errorMsg = "Email Address is required";
                return errorMsg;
            }

            if (!await ValidatorHelper.ValidateEmail(email) && errorMsg == "Ok")
            {
                errorMsg = "Email Address is not valid";
                return errorMsg;
            }

            if (!await _iaccountService.EmailTakenChecker(email) && errorMsg == "Ok")
            {
                errorMsg = "Email Address is already taken";
                return errorMsg;
            }

            return errorMsg;
        }
        public async Task<bool> Step2Validation(SignupDTO param)
        {
            try
            {
                var result = "Ok";

                if (!string.IsNullOrEmpty(param.EmailAddress))
                {
                    result = await ValidateEmail(param.EmailAddress);
                    if (result != "Ok")
                    {
                        _toastService.ShowError(result);
                        return false;
                    }
                }
                if (param.Gender == null)
                {
                    _toastService.ShowError("Gender selection is needed");
                    return false;
                }
                if (param.NationalityId == null)
                {
                    _toastService.ShowError("Nationality selection is needed");
                    return false;
                }
                if (param.CurrentCityId < 1 || param.CurrentCityId is null || string.IsNullOrEmpty(param.CurrentStreet))
                {
                    _toastService.ShowError("Current Address fields needs to be complete");
                    return false;
                }
                if (param.AddressAreSame == false && (param.PermanentCityId == null || param.PermanentCityId < 1 || string.IsNullOrEmpty(param.PermanentStreet)))
                {
                    _toastService.ShowError("Permanent Address fields needs to be complete if addresses are not the same");
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region STEP 3 VALIDATIONS
        public async Task<bool> Step3Validation(SignupDTO param)
        {
            if (param.NatureOfWorkId is null)
            {
                _toastService.ShowError("Nature of work is needed");
                return false;
            }
            if (param.SourceOfIncomeId is null)
            {
                _toastService.ShowError("Source of income is needed");
                return false;
            }
            if (!hasGovtimg)
            {
                _toastService.ShowError("A valid Government Issued ID Image is needed");
                return false;
            }
            if (string.IsNullOrEmpty(param.IdentificationNumber))
            {
                _toastService.ShowError("Government ID Number is needed");
                return false;
            }
            if (!await ValidatorHelper.ValidateIssueId(param.IdentificationNumber))
            {
                _toastService.ShowError("Identification Number only allows letters, dashes and numeric inputs");
                return false;
            }

            return true;
        }
        #endregion

        public async Task SubmitDetails(SignupDTO param)
        {
            if (ProfileImageBytes == null || GovernmentImageBytes == null)
            {
                _toastService.ShowError("Profile Image and a valid Government Issued ID Image is needed");
                return;
            }
            var DeviceToken = _icurrentUser.DeviceToken;
            var popupRes = _popupModal.Show<PopupLoading>("");
            var UserData = new SignupDTO()
            {
                RoleType = param.RoleType,
                FirstName = param.FirstName,
                MiddleName = param.MiddleName,
                LastName = param.LastName,
                DateOfBirth = param.DateOfBirth,
                DeviceToken = DeviceToken,
                IdentificationNumber = param.IdentificationNumber,
                PlaceOfBirth = param.PlaceOfBirth is not null ? param.PlaceOfBirth : "",
                Gender = param.Gender,
                SourceOfIncomeId = param.SourceOfIncomeId,
                OtherSourceOfIncome = param.OtherSourceOfIncome is not null ? param.OtherSourceOfIncome : "",
                NatureOfWorkId = param.NatureOfWorkId,
                OtherNatureOfWork = param.OtherNatureOfWork is not null ? param.OtherNatureOfWork : "",
                NationalityId = param.NationalityId,
                EmailAddress = param.EmailAddress is not null ? param.EmailAddress : "",
                CurrentCityId = param.CurrentCityId,
                CurrentStreet = param.CurrentStreet,
                AddressAreSame = param.AddressAreSame,
                PermanentCityId = param.PermanentCityId,
                PermanentStreet = param.PermanentStreet,
                Id = _icurrentUser?.Id,
                ProfileImage = new UploadModel
                {
                    FileBytes = ProfileImageBytes,
                    OriginalFileName = "Profile" + param.FirstName.Replace(" ", string.Empty) + DateTime.UtcNow.ToString("MMddyyyy") + ".jpeg"
                },
                GovernmentImage = new UploadModel
                {
                    FileBytes = GovernmentImageBytes,
                    OriginalFileName = "Government" + param.FirstName.Replace(" ", string.Empty) + DateTime.UtcNow.ToString("MMddyyyy") + ".jpeg"
                },
            };

            RegisterResultDTO registerUser;
            if (param.RoleType == (int)RoleTypes.MasterAgent)
            {
                registerUser = await _iaccountService.CreateMasterAgent(_mapper.Map<SignupDTO>(UserData));
            }
            else
            {
                registerUser = await _iaccountService.VerifyUser(UserData);
            }

            popupRes.Close();
            if (registerUser == null)
            {
                _toastService.ShowError("An error occured while attempting to register. Please try again later");
                return;
            }
            if (!registerUser.Success)
            {
                _toastService.ShowError("An error occured while attempting to register. Please try again later");
                return;
            }

            _icurrentUser.Verified = 2; // For verification status
            _toastService.ShowSuccess("Your verification is currently under review. Please wait for further updates");
            _navigationManager.NavigateTo("/credit/withdraw");
        }

        #endregion
    }


}
