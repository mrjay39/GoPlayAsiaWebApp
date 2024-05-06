using AutoMapper;
using Blazored.Modal.Services;
using Blazored.Modal;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using static GoplayasiaBlazor.Models.Constants.Settings;
using GoPlayAsiaWebApp.Goplay.ViewModels;

namespace GoPlayAsiaWebApp.Goplay.Main.SignupAgent
{
    public partial class SignupAgent
    {
        public string showpass = "password";
        public string showpassicon = "bi bi-eye-fill";
        public string showpassConfirm = "password";
        public string showpassiconConfirm = "bi bi-eye-fill";
        #region Injected Services 
        [Inject] IConstantService iConstantService { get; set; }
        [Inject] IAccountService _accountService { get; set; }
        [Inject] IMapper _mapper { get; set; }
        [Inject] SignUpViewModel _signupViewModel { get; set; }

        #endregion

        #region Local Variables

        [Parameter]
        public string refkey { get; set; } = "";
        [CascadingParameter] public IModalService popupModal { get; set; }
        private bool step1 { get; set; } = false;
        private bool step2 { get; set; } = true;
        private bool step3 { get; set; } = true;
        private bool step4 { get; set; } = true;
        private bool step5 { get; set; } = true;
        private bool step6 { get; set; } = true;
        private string? ProfileImageUri = "/img/default_head_silhouette.png";
        private string? IDImageUri = "/img/default_id_card.png";
        //private string? ProfileImageUrib64 = "/img/default_head_silhouette.png";
        //private string? IDImageUrib64 = "/img/default_id_card.png";

        //SignupDTO signupDTO = new SignupDTO();
        private EditContext? editContext;

        [Parameter]
        public IEnumerable<CityModel> CityList { get; set; }
        public IEnumerable<NationalityModel> NationalityList { get; set; }
        public IEnumerable<NatureOfWorkModel> NatureOfWorkList { get; set; }
        public IEnumerable<SourceOfIncomeModel> SourceOfIncomeList { get; set; }
        [CascadingParameter] public IModalService popuLoadingpModal { get; set; }
        public IModalReference refpopuLoadingpModal { get; set; }
        [Inject] IToastService toastService { get; set; }

        private bool hideUsername = true;
        private string msgUsername = "";
        private bool hidePassword = true;
        private string msgPassword = "";
        private bool hideEmail = true;
        private string msgEmail = "";
        private bool hideMobile = true;
        private string msgMobile = "";

        private NationalityModel? selectedNationality { get; set; }
        private CityModel? selectedCurrCity;
        private CityModel? selectedCurrProv;
        private CityModel? selectedPermCity;
        private CityModel? selectedPermProv;
        private CityModel? selectedPermProvOld;
        private string PermanentStreetOldVal = "";
        private bool isChecked { get; set; } = false;
        private bool hidePermanentAdd = false;

        private NatureOfWorkModel? selectedNatureofwork;
        private bool hideOtherNatureOfWOrk = true;
        private SourceOfIncomeModel? selectedSourceofincome;
        private bool hideOtherSourceOfIncome = true;

        private string refClass = "referralKey";
        private bool refDisable = true;
        private bool refIsChecked = false;
        private bool refWarning = true;
        private string refValue { get; set; }
        private string refOldvalue = "";

        private bool agreePolicy = false;
        private bool disableSubmit = true;
        private string submitClass = "referralKey";

        private string _termsAndConditionsUrl;
        FileUploadProgress? progress { get; set; }
        FileUploadProgress? progressid { get; set; }
        [Inject]
        NavigationManager _navigationManager { get; set; }
        string CurrUrl;
        private CancellationTokenSource cancelation;
        private bool isrefkeyenabled = true;
        protected override void OnParametersSet()
        {

            //CurrUrl = _navigationManager.Uri.ToString();
        }

        public string TermsAndConditionsUrl
        {
            get => _termsAndConditionsUrl;
            set
            {
                _termsAndConditionsUrl = value;
            }
        }

        private string _privacyPolicyUrl;
        public string PrivacyPolicyUrl
        {
            get => _privacyPolicyUrl;
            set
            {
                _privacyPolicyUrl = value;
            }
        }

        private ElementReference txtUsername;
        #endregion

        #region Local Methods

        #region STEP 1
        private async Task onchange_txtUsername(ChangeEventArgs e)
        {
            var value = (string)e.Value;
            string valUsername = await _signupViewModel.ValidateUsername(value);
            msgUsername = valUsername;

            if (value.Length == 0)
            {
                msgUsername = "Username is required";
                hideUsername = false;
                return;
            }

            if (valUsername != "Ok")
            {
                hideUsername = false;
            }
            else
            {
                hideUsername = true;
            }
        }
        public void ShowPassword()
        {
            if (showpass == "password")
            {
                showpass = "text";
                showpassicon = "bi bi-eye-slash-fill";
            }
            else
            {
                showpass = "password";
                showpassicon = "bi bi-eye-fill";
            }
        }
        private async Task onchange_txtPass(ChangeEventArgs e)
        {
            var value = (string)e.Value;
            string valPass = await _signupViewModel.ValidatePassword(value, _signupViewModel.SignupDTO.ConfirmPassword);
            msgPassword = valPass;

            if (valPass != "Ok")
            {
                hidePassword = false;
            }
            else
            {
                hidePassword = true;
            }
        }
        public void ShowPasswordConfirm()
        {
            if (showpassConfirm == "password")
            {
                showpassConfirm = "text";
                showpassConfirm = "bi bi-eye-slash-fill";
            }
            else
            {
                showpassConfirm = "password";
                showpassiconConfirm = "bi bi-eye-fill";
            }
        }
        private async Task onchange_txtConfirmpass(ChangeEventArgs e)
        {
            var value = (string)e.Value;
            string valPass = await _signupViewModel.ValidatePassword(_signupViewModel.SignupDTO.Password, value);
            msgPassword = valPass;

            if (valPass != "Ok")
            {
                hidePassword = false;
            }
            else
            {
                hidePassword = true;
            }
        }
        private async Task onchange_txtEmail(ChangeEventArgs e)
        {
            var value = (string)e.Value;
            string valEmail = await _signupViewModel.ValidateEmail(value);
            msgEmail = valEmail;

            if (valEmail != "Ok")
            {
                hideEmail = false;
            }
            else
            {
                hideEmail = true;
            }
        }
        private async Task onchange_txtMobile(ChangeEventArgs e)
        {
            var value = (string)e.Value;
            string valMobile = await _signupViewModel.ValidateMobile(value);
            msgMobile = valMobile;

            if (valMobile != "Ok")
            {
                hideMobile = false;
            }
            else
            {
                hideMobile = true;
            }
        }
        private async Task ShowStep2()
        {
            refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
            bool step1val = await _signupViewModel.ValidateStep1();
            if (step1val)
            {
                step1 = true;
                step2 = false;
                step3 = true;
                step4 = true;
                step5 = true;
                step6 = true;
            }
            refpopuLoadingpModal.Close();

        }
        #endregion

        #region STEP 2
        private async Task onchange_bdmonth(ChangeEventArgs e)
        {
            var value = (string)e.Value;
            var valInt = int.Parse(value);

            if (value.Length == 1)
            {
                _signupViewModel.SignupDTO.Month = "0" + value;
            }
            else if (valInt > 12)
            {
                _signupViewModel.SignupDTO.Month = "00";
            }
            else
            {
                _signupViewModel.SignupDTO.Month = value;
            }
        }
        private async Task onchange_bdday(ChangeEventArgs e)
        {
            var value = (string)e.Value;
            var valInt = int.Parse(value);

            if (value.Length == 1)
            {
                _signupViewModel.SignupDTO.Day = "0" + value;
            }
            else if (valInt > 31)
            {
                _signupViewModel.SignupDTO.Day = "00";
            }
            else
            {
                _signupViewModel.SignupDTO.Day = value;
            }
        }
        private void ShowStep1()
        {
            step1 = false;
            step2 = true;
            step3 = true;
            step4 = true;
            step5 = true;
            step6 = true;
        }
        private async Task ShowStep3()
        {
            refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
            _signupViewModel.SignupDTO.Gender = "MALE";
            bool step2val = await _signupViewModel.ValidateStep2();
            if (step2val)
            {
                step1 = true;
                step2 = true;
                step3 = false;
                step4 = true;
                step5 = true;
            }
            refpopuLoadingpModal.Close();
        }
        #endregion

        #region STEP 3
        private async Task<IEnumerable<NationalityModel>> SearchNationality(string searchText)
        {
            return await Task.FromResult(NationalityList.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList());
        }
        private NationalityModel LoadNationality(int? id) => NationalityList.FirstOrDefault(p => p.Id == id);
        private async Task ShowStep4()
        {
            refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
            if (selectedNationality is not null)
            {
                _signupViewModel.SignupDTO.NationalityId = selectedNationality.Id;
            }
            else
            {
                _signupViewModel.SignupDTO.NationalityId = null;
            }
            bool step3val = await _signupViewModel.ValidateStep3();
            if (step3val)
            {
                step1 = true;
                step2 = true;
                step3 = true;
                step4 = false;
                step5 = true;
                step6 = true;
            }
            refpopuLoadingpModal.Close();
        }
        #endregion

        #region STEP 4
        private async Task<IEnumerable<CityModel>> SearchCity(string searchText)
        {
            return await Task.FromResult(CityList.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList());
        }
        private CityModel LoadSelCurrCity(int? id) => CityList.FirstOrDefault(p => p.Id == id);
        private async Task selectedCurrCityChanged(CityModel result)
        {
            selectedCurrCity = result;
            if (selectedCurrCity is not null)
            {
                refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
                selectedCurrProv = await iConstantService.GetParentsOfCity(result.Id);
                refpopuLoadingpModal.Close();

                _signupViewModel.SignupDTO.CurrentCityId = selectedCurrCity.Id;
                _signupViewModel.SignupDTO.CurrentProvince = selectedCurrProv.Province.Name;
                _signupViewModel.SignupDTO.CurrentRegion = selectedCurrProv.Region.Name;
            }
            else
            {
                _signupViewModel.SignupDTO.CurrentCityId = null;
                _signupViewModel.SignupDTO.CurrentProvince = null;
                _signupViewModel.SignupDTO.CurrentRegion = null;
            }
        }
        private async Task CheckboxChanged(ChangeEventArgs e)
        {
            // get the checkbox state
            bool value = (bool)e.Value;
            if (value)
            {
                if (selectedCurrCity is null)
                {
                    _signupViewModel.SignupDTO.PermanentCityId = null;
                    _signupViewModel.SignupDTO.PermanentProvince = "";
                    _signupViewModel.SignupDTO.PermanentRegion = "";
                    _signupViewModel.SignupDTO.PermanentStreet = "";
                    isChecked = false;
                }
                else
                {
                    hidePermanentAdd = true;
                }
            }
            else
            {
                hidePermanentAdd = false;
                selectedPermCity = selectedPermProvOld;
                if (selectedPermCity is null)
                {
                    _signupViewModel.SignupDTO.PermanentCityId = null;
                    _signupViewModel.SignupDTO.PermanentProvince = "";
                    _signupViewModel.SignupDTO.PermanentRegion = "";
                    _signupViewModel.SignupDTO.PermanentStreet = "";
                }
                else
                {
                    _signupViewModel.SignupDTO.PermanentCityId = selectedPermProvOld.Id;
                    _signupViewModel.SignupDTO.PermanentProvince = selectedPermProvOld.Province.Name;
                    _signupViewModel.SignupDTO.PermanentRegion = selectedPermProvOld.Region.Name;
                    _signupViewModel.SignupDTO.PermanentStreet = PermanentStreetOldVal;
                }
            }
            _signupViewModel.SignupDTO.AddressAreSame = value;
        }
        private void onchangePermStreet(ChangeEventArgs e)
        {
            var value = e.Value;
            PermanentStreetOldVal = value.ToString();
        }
        private CityModel LoadSelPermCity(int? id) => CityList.FirstOrDefault(p => p.Id == id);
        private async Task selectedPermCityChanged(CityModel result)
        {
            selectedPermCity = result;
            if (selectedPermCity is not null)
            {
                refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
                selectedPermProv = await iConstantService.GetParentsOfCity(result.Id);
                selectedPermProvOld = await iConstantService.GetParentsOfCity(result.Id);
                refpopuLoadingpModal.Close();

                _signupViewModel.SignupDTO.PermanentCityId = selectedPermCity.Id;
                _signupViewModel.SignupDTO.PermanentProvince = selectedPermProv.Province.Name;
                _signupViewModel.SignupDTO.PermanentRegion = selectedPermProv.Region.Name;
            }
            else
            {
                _signupViewModel.SignupDTO.PermanentCityId = null;
                _signupViewModel.SignupDTO.PermanentProvince = null;
                _signupViewModel.SignupDTO.PermanentRegion = null;
            }
        }
        private async Task ShowStep5()
        {
            try
            {
                refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
                bool step4val = await _signupViewModel.ValidateStep4();
                if (step4val)
                {
                    step1 = true;
                    step2 = true;
                    step3 = true;
                    step4 = true;
                    step5 = false;
                    step6 = true;
                }
                // copy current address
                if (isChecked)
                {
                    selectedPermCity = selectedCurrCity;
                    selectedPermProv = await iConstantService.GetParentsOfCity(selectedPermCity.Id);
                    _signupViewModel.SignupDTO.PermanentCityId = selectedPermCity.Id;
                    _signupViewModel.SignupDTO.PermanentProvince = selectedPermProv.Province.Name;
                    _signupViewModel.SignupDTO.PermanentRegion = selectedPermProv.Region.Name;
                    _signupViewModel.SignupDTO.PermanentStreet = _signupViewModel.SignupDTO.CurrentStreet;
                }
                refpopuLoadingpModal.Close();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region STEP 5
        record FileUploadProgress(string FileName, long Size)
        {
            public long UploadedBytes { get; set; }
            public double UploadedPercentage => UploadedBytes / (double)Size * 100d;
        }
        string FormatBytes(long value)
        {
            return (value / 1024f / 1024f).ToString();
        }
        public async Task LoadProfileImage(InputFileChangeEventArgs inputFileChangeEventArgs)
        {
            try
            {
                #region Main Photo
                long maxFileSize = 1024 * 1024 * 50;

                var image = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 1280, 720);
                //var image = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 5000, 5000);
                progress = new FileUploadProgress(image.Name, image.Size);

                using var stream = image.OpenReadStream(maxFileSize);

                var buffer = new byte[4 * 1096];
                int bytesRead;
                double totalRead = 0;
                await using var timer = new Timer(_ => InvokeAsync(() => StateHasChanged()));
                timer.Change(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));

                using (MemoryStream ms = new MemoryStream())
                {
                    while ((bytesRead = await stream.ReadAsync(buffer, cancelation.Token)) != 0)
                    {
                        totalRead += bytesRead;
                        ms.Write(buffer, 0, bytesRead);

                        progress.UploadedBytes = (int)totalRead;
                        StateHasChanged();
                    }
                    _signupViewModel.ProfileImageBytes = ms.ToArray();
                    ////convert stream to base64
                    //ProfileImageUrib64 = $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";
                    StateHasChanged();
                }

                #endregion
                #region Preview Photo

                var pimage = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 150, 150);
                using var pstream = pimage.OpenReadStream(maxFileSize);

                var pbuffer = new byte[4 * 1096];
                int pbytesRead;

                using (MemoryStream pms = new MemoryStream())
                {
                    while ((pbytesRead = await pstream.ReadAsync(pbuffer)) != 0)
                    {
                        pms.Write(pbuffer, 0, pbytesRead);
                    }
                    ////convert stream to base64
                    ProfileImageUri = $"data:image/png;base64,{Convert.ToBase64String(pms.ToArray())}";
                    StateHasChanged();
                }
                _signupViewModel.hasProfimg = true;

                #endregion
            }
            catch (Exception ex)
            {
                toastService.ShowError(ex.Message);
                _signupViewModel.hasProfimg = false;
            }
        }
        public async Task LoadIDImage(InputFileChangeEventArgs inputFileChangeEventArgs)
        {

            try
            {
                #region Main Photo
                long maxFileSize = 1024 * 1024 * 50;

                var image = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 1280, 720);

                //var image = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 5000, 5000);
                progressid = new FileUploadProgress(image.Name, image.Size);

                using var stream = image.OpenReadStream(maxFileSize);

                var buffer = new byte[4 * 1096];
                int bytesRead;
                double totalRead = 0;
                await using var timer = new Timer(_ => InvokeAsync(() => StateHasChanged()));
                timer.Change(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));

                using (MemoryStream ms = new MemoryStream())
                {
                    while ((bytesRead = await stream.ReadAsync(buffer, cancelation.Token)) != 0)
                    {
                        totalRead += bytesRead;
                        ms.Write(buffer, 0, bytesRead);

                        progressid.UploadedBytes = (int)totalRead;
                        StateHasChanged();
                    }
                    _signupViewModel.GovernmentImageBytes = ms.ToArray();
                    ////convert stream to base64
                    //IDImageUrib64 = $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";
                    StateHasChanged();
                }
                #endregion
                #region Preview Photo

                var pimage = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 150, 150);
                using var pstream = pimage.OpenReadStream(maxFileSize);

                var pbuffer = new byte[4 * 1096];
                int pbytesRead;

                using (MemoryStream pms = new MemoryStream())
                {
                    while ((pbytesRead = await pstream.ReadAsync(pbuffer)) != 0)
                    {
                        pms.Write(pbuffer, 0, pbytesRead);
                    }
                    ////convert stream to base64
                    IDImageUri = $"data:image/png;base64,{Convert.ToBase64String(pms.ToArray())}";
                    StateHasChanged();
                }
                _signupViewModel.hasGovtimg = true;
                #endregion
            }
            catch (Exception ex)
            {
                toastService.ShowError(ex.Message);
                _signupViewModel.hasGovtimg = false;
            }
        }
        private async Task ShowStep6()
        {
            //await VerifyPhotoandID();
            refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
            bool step5val = await _signupViewModel.ValidateStep5();
            if (step5val)
            {
                step1 = true;
                step2 = true;
                step3 = true;
                step4 = true;
                step5 = true;
                step6 = false;
            }
            refpopuLoadingpModal.Close();
        }
        #endregion

        #region STEP 6
        private async Task<IEnumerable<NatureOfWorkModel>> SearchNatureofwork(string searchText)
        {
            return await Task.FromResult(NatureOfWorkList.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList());
        }
        private NatureOfWorkModel LoadSelNatureOfWork(int? id) => NatureOfWorkList.FirstOrDefault(p => p.Id == id);
        private async Task selectedNatureOfWorChanged(NatureOfWorkModel result)
        {
            selectedNatureofwork = result;
            if (selectedNatureofwork is not null)
            {
                _signupViewModel.SignupDTO.NatureOfWorkId = selectedNatureofwork.Id;
                if (selectedNatureofwork.Id == 6)  // 6 - Others
                {
                    hideOtherNatureOfWOrk = false;
                }
                else
                {
                    hideOtherNatureOfWOrk = true;
                    _signupViewModel.SignupDTO.OtherNatureOfWork = "";
                }
            }
            else
            {
                _signupViewModel.SignupDTO.NatureOfWorkId = null;
                hideOtherNatureOfWOrk = true;
                _signupViewModel.SignupDTO.OtherNatureOfWork = "";
            }
        }
        private async Task<IEnumerable<SourceOfIncomeModel>> SearchSourcofIncome(string searchText)
        {
            return await Task.FromResult(SourceOfIncomeList.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList());
        }
        private SourceOfIncomeModel LoadSelSourceOfIncome(int? id) => SourceOfIncomeList.FirstOrDefault(p => p.Id == id);
        private async Task selectedSourceOfIncomeChanged(SourceOfIncomeModel result)
        {
            selectedSourceofincome = result;
            if (selectedSourceofincome is not null)
            {
                _signupViewModel.SignupDTO.SourceOfIncomeId = selectedSourceofincome.Id;
                if (selectedSourceofincome.Id == 4)  // 4 - Others
                {
                    hideOtherSourceOfIncome = false;
                }
                else
                {
                    hideOtherSourceOfIncome = true;
                    _signupViewModel.SignupDTO.OtherSourceOfIncome = "";
                }
            }
            else
            {
                _signupViewModel.SignupDTO.SourceOfIncomeId = null;
                hideOtherSourceOfIncome = true;
                _signupViewModel.SignupDTO.OtherSourceOfIncome = "";
            }

        }
        private async Task refInputChanged(ChangeEventArgs e)
        {
            refValue = (string)e.Value;
            if (refValue is not null)
            {
                var referralKeyChecker = await _accountService.ReferralKeyChecker(refValue);
                if (!referralKeyChecker)
                {
                    if (refValue == "" || refValue is null)
                    {
                        refWarning = true;
                    }
                    else
                    {
                        refWarning = false;
                    }
                }
                else
                {
                    refWarning = true;
                }
            }
            refOldvalue = refValue;
        }
        private void setAgentCode()
        {
            refIsChecked = true;
            refClass = "";
            refDisable = false;
        }
        private void refFocusout()
        {
            if (_signupViewModel.SignupDTO.ReferralKey == null || _signupViewModel.SignupDTO.ReferralKey == "")
            {
                refIsChecked = false;
                refClass = "referralKey";
                refDisable = true;
            }
        }
        private async Task agreeCheckboxChanged(ChangeEventArgs e)
        {
            bool value = (bool)e.Value;
            if (value)
            {
                disableSubmit = false;
                submitClass = "";
            }
            else
            {
                disableSubmit = true;
                submitClass = "referralKey";
            }
        }
        private async Task SubmitDetails()
        {
            var resRefkey = await _signupViewModel.ValidateReferralKey();
            if (resRefkey)
            {
                await _signupViewModel.SubmitDetails();
            }
        }
        #endregion


        private async Task refCheckboxChanged(ChangeEventArgs e)
        {
            bool value = (bool)e.Value;
            if (value)
            {
                refClass = "";
                refDisable = false;
                _signupViewModel.SignupDTO.ReferralKey = refOldvalue;
                var referralKeyChecker = await _accountService.ReferralKeyChecker(refOldvalue);
                if (!referralKeyChecker)
                {
                    if (refOldvalue == "" || refOldvalue is null)
                    {
                        refWarning = true;
                    }
                    else
                    {
                        refWarning = false;
                    }
                }
                else
                {
                    refWarning = true;
                }
                _signupViewModel.hasRef = true;
            }
            else
            {
                refClass = "referralKey";
                refDisable = true;
                refWarning = true;
                _signupViewModel.SignupDTO.ReferralKey = "";
                _signupViewModel.hasRef = false;
            }
        }

        #endregion


        #region Lifecycle Method
        protected override async Task OnInitializedAsync()
        {
            _signupViewModel._popupModal = popupModal;
            _signupViewModel.SignupDTO = new SignupDTO();
            if (!string.IsNullOrEmpty(refkey))
            {
                refIsChecked = true;
                refClass = "";
                refDisable = false;
            }
            _signupViewModel.SignupDTO.ReferralKey = refkey;
            if (string.IsNullOrEmpty(refkey))
            {
                isrefkeyenabled = false;
            }
            _signupViewModel.SignupDTO.AddressAreSame = isChecked;
            editContext = new(_signupViewModel.SignupDTO);

            var constants = await iConstantService.GetConstants();
            CityList = _mapper.Map<IEnumerable<CityModel>>(constants.Cities);

            NationalityList = _mapper.Map<IEnumerable<NationalityModel>>(constants.Nationalities);
            selectedNationality = NationalityList.FirstOrDefault(p => p.Id == 62);
            _signupViewModel.SignupDTO.NationalityId = selectedNationality.Id;

            NatureOfWorkList = _mapper.Map<IEnumerable<NatureOfWorkModel>>(constants.NaturesOfWork);
            selectedNatureofwork = NatureOfWorkList.FirstOrDefault(p => p.Id == 1);
            _signupViewModel.SignupDTO.NatureOfWorkId = selectedNatureofwork.Id;

            SourceOfIncomeList = _mapper.Map<IEnumerable<SourceOfIncomeModel>>(constants.SourcesOfIncome);
            selectedSourceofincome = SourceOfIncomeList.FirstOrDefault(p => p.Id == 1);
            _signupViewModel.SignupDTO.SourceOfIncomeId = selectedSourceofincome.Id;

            if (constants.CorporationSettings != null)
            {
                TermsAndConditionsUrl = constants.CorporationSettings.TermsAndConditionsUrl;
                PrivacyPolicyUrl = constants.CorporationSettings.PrivacyPolicyUrl;
            }
            CurrUrl = _navigationManager.Uri.ToString();
            if (CurrUrl.ToLower().Contains("agentsignup"))
            {
                _signupViewModel.SignupDTO.RoleType = (int)RoleTypes.Agent;
            }
            else if (CurrUrl.ToLower().Contains("masignup"))
            {
                _signupViewModel.SignupDTO.RoleType = (int)RoleTypes.Agent;
            }
            else
            {
                _signupViewModel.SignupDTO.RoleType = (int)RoleTypes.Player;
            }
            txtUsername.FocusAsync();
            cancelation = new CancellationTokenSource();
        }
        #endregion


    }
}
