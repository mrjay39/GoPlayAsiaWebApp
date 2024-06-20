using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoPlayAsiaWebApp.Goplay.Games.Lucky9;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace GoPlayAsiaWebApp.Goplay.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Local Variable & Properties
        IModalService _popupModal;

        private UserDTO _userInfo { get; set; }
        public UserDTO UserInfo
        {
            get => _userInfo;
            set
            {
                _userInfo = value;
            }
        }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
            }
        }
        private string _oldPassword;
        private string _newPassword;
        private string _confirmNewPassword;
        private string _reason;
        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
            }
        }
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
            }
        }
        public string ConfirmNewPassword
        {
            get => _confirmNewPassword;
            set
            {
                _confirmNewPassword = value;
            }
        }
        public int? currNatureOfWorkID { get; set; }
        public string? currOtherNatureOfWork { get; set; }
        public int? currSourceofIncome { get; set; }
        public string? currOtherSourceOfIncome { get; set; }
        public string? currPlaceOfBirth { get; set; }
        public string Reason
        {
            get => _reason;
            set
            {
                _reason = value;
            }
        }
        #endregion

        #region Life cycle methods
        public SettingsViewModel(IAccountService accountService, IToastService toastService, ICurrentUser currentUser, IModalService popupModal, NavigationManager navigationManager, IAccountService iaccountService, AuthenticationStateProvider AuthenticationStateProvider, IConfiguration config)
        {
            _config = config;
            _toastService = toastService;
            _icurrentUser = currentUser;
            _popupModal = popupModal;
            _navigationManager = navigationManager;
            _iaccountService = iaccountService;
            _AuthenticationStateProvider = AuthenticationStateProvider;
        }
        #endregion

        #region Local Methods

        public async Task GetUserInfo()
        {
            var user = await _iaccountService.GetUser();
            if (user == null || user.User == null || user.User.Id < 1)
            {
                _toastService.ShowError("Failed to fetch user information");
                return;
            }
            currNatureOfWorkID = user.User.NatureOfWorkId;
            currOtherNatureOfWork = user.User.OtherNatureOfWork;
            currSourceofIncome = user.User.SourceOfIncomeId;
            currOtherSourceOfIncome = user.User.OtherSourceOfIncome;
            currPlaceOfBirth = user.User.PlaceOfBirth;
            UserInfo = user.User;
        }

        public async Task<string> ValidateEmail(string email)
        {
            var errorMsg = "Ok";

            if (string.IsNullOrEmpty(email) || email.Contains(" ") && errorMsg == "Ok")
            {
                errorMsg = "Email Address cannot be empty, cannot contain spaces and must be valid";
            }

            if (string.IsNullOrEmpty(email) && errorMsg == "Ok")
            {
                errorMsg = "Email Address is required";
            }

            if (!await ValidatorHelper.ValidateEmail(email) && errorMsg == "Ok")
            {
                errorMsg = "Email Address is not valid";
            }

            if (!await _iaccountService.EmailTakenChecker(email) && errorMsg == "Ok")
            {
                errorMsg = "Email Address is already taken";
            }

            return errorMsg;
        }
        public async Task<bool> UpdateAccountInformation()
        {

            if (string.IsNullOrEmpty(UserInfo.EmailAddress) || !await ValidatorHelper.ValidateEmail(UserInfo.EmailAddress))
            {
                _toastService.ShowError("Email Address cannot be empty and must be valid");
                return false;
            }

            var valEmail = await ValidateEmail(UserInfo.EmailAddress);
            if (valEmail != "Ok")
            {
                _toastService.ShowError(valEmail);
                return false;
            }

            if (UserInfo.EmailAddress.ToLower() != _icurrentUser.EmailAddress.ToLower())
            {
                var pop = _popupModal.Show<PopupConfirm>("Continue to change email address?", new ModalOptions() { Class = "op-modal", HideHeader = false });
                var result = await pop.Result;
                if (!(bool)result.Data)
                {
                    return false;
                }
            }

            var parameters = new ModalParameters();
            parameters.Add("EmailAddress", UserInfo.EmailAddress);
            var popupCode = _popupModal.Show<PopupEmailVerify>("", parameters, new ModalOptions() { Class = "op-modal", HideHeader = false });
            var resCode = await popupCode.Result;
            if (!(bool)resCode.Data)
            {
                return false;
            }

            UserDTO paramsModel = new UserDTO()
            {
                Id = _icurrentUser.Id,
                Username = UserInfo.Username,
                EmailAddress = UserInfo.EmailAddress,
                MobileNumber = UserInfo.MobileNumber,
                ModifiedById = _icurrentUser.Id
            };
            var update = await _iaccountService.UpdateAccountInformation(paramsModel);
            if (update == null)
            {
                _toastService.ShowError("an error occured while updating account information. please try again later");
                return false;
            }
            if (!update.Success)
            {
                _toastService.ShowError(update.Message);
                return false;
            }
            _icurrentUser.EmailAddress = paramsModel.EmailAddress;
            _icurrentUser.MobileNumber = paramsModel.MobileNumber;
            _toastService.ShowSuccess("Account has been updated");
            return true;
        }
        public async Task DeactivateAccount()
        {
            var pop = _popupModal.Show<PopupDeactivate>("Account Deactivation", new ModalOptions() { Class = "op-modal", HideHeader = false });
            var result = await pop.Result;
            if (!(bool)result.Data)
            {
                return;
            }

            if (string.IsNullOrEmpty(Reason))
            {
                _toastService.ShowError("Reason cannot be empty.");
                return;
            }

            var update = await _iaccountService.DeactivateUser(_icurrentUser.Id, Reason);

            if (!update)
            {
                _toastService.ShowError("Unable to deactivate user account.");
                return;
            }

            _toastService.ShowSuccess("Account deactivated.");
            await Logout();
        }

        public async Task<bool> UpdateProfileInformation()
        {
            if (string.IsNullOrEmpty(UserInfo.FirstName) || string.IsNullOrEmpty(UserInfo.LastName))
            {
                _toastService.ShowError("First Name and Last Name cannot be empty");
                return false;
            }
            if (string.IsNullOrEmpty(UserInfo.DateOfBirth?.ToString("MM")) || !int.TryParse(UserInfo.DateOfBirth?.ToString("MM"), out int monthResult))
            {
                _toastService.ShowError("Birthdate month cannot be empty and must be valid");
                return false;
            }
            if (string.IsNullOrEmpty(UserInfo.DateOfBirth?.ToString("dd")) || !int.TryParse(UserInfo.DateOfBirth?.ToString("dd"), out int dayResult))
            {
                _toastService.ShowError("Birthdate month cannot be empty and must be valid");
                return false;
            }
            if (string.IsNullOrEmpty(UserInfo.DateOfBirth?.ToString("yyyy")) || !int.TryParse(UserInfo.DateOfBirth?.ToString("yyyy"), out int yearResult))
            {
                _toastService.ShowError("Birthdate month cannot be empty and must be valid");
                return false;
            }
            if (!await ValidatorHelper.ValidateDate(monthResult, dayResult, yearResult))
            {
                _toastService.ShowError("Birthdate is not a valid date");
                return false;
            }
            DateOfBirth = new DateTime(yearResult, monthResult, dayResult);
            if (!await ValidatorHelper.ValidateAge(21, DateOfBirth))
            {
                _toastService.ShowError("Age is not acceptable");
                return false;
            }
            if (UserInfo.NatureOfWorkId == null || UserInfo.NatureOfWorkId < 1)
            {
                _toastService.ShowError("Nature of Work is needed");
                return false;
            }
            //if ((UserInfo.NatureOfWorkId == 6) && string.IsNullOrEmpty(UserInfo.OtherNatureOfWork))
            //{
            //    _toastService.ShowError("Other Nature of Work specification is needed");
            //    return false;
            //}
            if (UserInfo.SourceOfIncomeId == null || UserInfo.SourceOfIncomeId < 1)
            {
                _toastService.ShowError("Source of Income is needed");
                return false;
            }
            //if ((UserInfo.SourceOfIncomeId == 4) && string.IsNullOrEmpty(UserInfo.OtherSourceOfIncome))
            //{
            //    _toastService.ShowError("Other Source of Income specification is needed");
            //    return false;
            //}
            //if (string.IsNullOrEmpty(UserInfo.PlaceOfBirth))
            //{
            //    _toastService.ShowError("Place of Birth cannot be empty");
            //    return false;
            //}

            UserDTO paramsModel = new UserDTO()
            {
                Id = _icurrentUser.Id,
                FirstName = UserInfo.FirstName,
                MiddleName = UserInfo.MiddleName,
                LastName = UserInfo.LastName,
                DateOfBirth = DateOfBirth,
                NatureOfWorkId = UserInfo.NatureOfWorkId,
                SourceOfIncomeId = UserInfo.SourceOfIncomeId,
                OtherSourceOfIncome = UserInfo.OtherSourceOfIncome,
                OtherNatureOfWork = UserInfo.OtherNatureOfWork,
                PlaceOfBirth = UserInfo.PlaceOfBirth,
                ModifiedById = _icurrentUser.Id
            };

            var update = await _iaccountService.UpdateProfileInformation(paramsModel);

            if (update == null)
            {
                _toastService.ShowError("An error occured while updating profile information. Please try again later");
                return false;
            }
            if (!update.Success)
            {
                _toastService.ShowError(update.Message);
                return false;
            }
            _icurrentUser.FullName = UserInfo.FirstName + " " + (!string.IsNullOrEmpty(UserInfo.MiddleName) ? UserInfo.MiddleName.Substring(0, 1) + ". " : string.Empty) + UserInfo.LastName;
            _toastService.ShowSuccess("Successfully updated profile information");
            return true;
        }

        public async Task<string> PasswordMatchChecker(string confirmpass)
        {
            var errorMsg = "Ok";
            if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(confirmpass))
            {
                if (NewPassword != confirmpass)
                    errorMsg = "Passwords did not match";
            }
            return errorMsg;
        }

        public async Task<bool> UpdatePassword()
        {
            if (string.IsNullOrEmpty(OldPassword))
            {
                _toastService.ShowError("Old password cannot be empty");
                return false;
            }

            if (string.IsNullOrEmpty(NewPassword))
            {
                _toastService.ShowError("New password cannot be empty");
                return false;
            }
            var popOtp = _popupModal.Show<PopupRequestOtp>("");
            var resOtp = await popOtp.Result;
            if (!(bool)resOtp.Data)
            {
                return false;
            }

            int valpass = await ValidatorHelper.ValidatePassword(NewPassword, ConfirmNewPassword);
            if (valpass > 0)
            {
                switch (valpass)
                {
                    case 1:
                        _toastService.ShowError("New password and confirm password did not match");
                        break;
                    case 2:
                        _toastService.ShowError("New password cannot be empty and must be at least 8 characters");
                        break;
                    case 3:
                        _toastService.ShowError("New password cannot contain spaces");
                        break;
                    default:
                        break;
                }
                return false;
            }
            UserDTO paramsModel = new UserDTO()
            {
                Id = _icurrentUser.Id,
                OldPassword = OldPassword,
                Password = NewPassword,
                ModifiedById = _icurrentUser.Id
            };

            var update = await _iaccountService.UpdatePassword(paramsModel);

            if (update == null)
            {
                _toastService.ShowError("An error occured while updating password. Please try again later");
                return false;
            }
            if (!update.Success)
            {
                _toastService.ShowError(update.Message);
                return false;
            }
            OldPassword = string.Empty;
            NewPassword = string.Empty;
            ConfirmNewPassword = string.Empty;

            _toastService.ShowSuccess("Successfully updated password");
            return true;
        }

        #endregion
    }
}
