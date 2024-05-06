using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using GoPlayAsiaWebApp.Goplay.Transactions.AddCredit;
using GoPlayAsiaWebApp.Goplay.Transactions.Withdraw;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.ObjectModel;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

public class MainCreditViewModel : BaseViewModel
{
    #region Local Variable & Properties
    //IAccountService _accountService;
    IModalService _popupModal;
    IReportService _reportService;
    ITransactionService _transactionService;
    public NavigationManager _navigationManager;

    private UserDTO _userInfo { get; set; }
    public UserDTO UserInfo
    {
        get => _userInfo;
        set
        {
            _userInfo = value;
        }
    }
    private UserModel _userParentdtl { get; set; }
    public UserModel UserParentDtl
    {
        get => _userParentdtl;
        set
        {
            _userParentdtl = value;
        }
    }

    CashinCashoutSettings _cashInSettings;
    CashinCashoutSettings _cashOutSettings;
    public CashinCashoutSettings CashInSettings
    {
        get => _cashInSettings;
        set
        {
            _cashInSettings = value;
        }
    }
    public CashinCashoutSettings CashOutSettings
    {
        get => _cashOutSettings;
        set
        {
            _cashOutSettings = value;
        }
    }

    public List<voucherActiveModel> ApplicableVouchers { get; set; }
    public List<voucherCorpModel> CorpVouchers { get; set; }
    public voucherActiveModel? activeVoucher { get; set; }

    #region TOPUP 
    public string PaymentType = "";
    public decimal voucherAmnt { get; set; } = 0;
    private string _referenceNumber;
    public string ReferenceNumber
    {
        get => _referenceNumber;
        set
        {
            _referenceNumber = value;
        }
    }
    private string _accountName;
    public string AccountName
    {
        get => _accountName;
        set
        {
            _accountName = value;
        }
    }
    private string _accountNumber;
    public string AccountNumber
    {
        get => _accountNumber;
        set
        {
            _accountNumber = value;
        }
    }
    public bool VoucherNetworkProblem { get; private set; } = false;

    // MIN & MAX AMOUNT
    private decimal _topupMinAmount;
    public decimal TopupMinAmount
    {
        get => _topupMinAmount;
        set
        {
            _topupMinAmount = value;
        }
    }
    private decimal _topupMinAmountDisplay;
    public decimal TopupMinAmountDisplay
    {
        get => _topupMinAmountDisplay;
        set
        {
            _topupMinAmountDisplay = value;
        }
    }
    private decimal _topupMaxAmount;
    public decimal TopupMaxAmount
    {
        get => _topupMaxAmount;
        set
        {
            _topupMaxAmount = value;
        }
    }

    // UB AMOUNT & PASSWORD
    private decimal? _topupUBAmount;
    public decimal? TopupUBAmount
    {
        get => _topupUBAmount;
        set
        {
            _topupUBAmount = value;
        }
    }
    private string _topupUBPassword;
    public string TopupUBPassword
    {
        get => _topupUBPassword;
        set
        {
            _topupUBPassword = value;
        }
    }

    // OTC AMOUNT & PASSWORD
    private decimal? _topupAmount;
    public decimal? TopupAmount
    {
        get => _topupAmount;
        set
        {
            _topupAmount = value;
        }
    }
    private string _topupPassword;
    public string TopupPassword
    {
        get => _topupPassword;
        set
        {
            _topupPassword = value;
        }
    }

    // GPA AMOUNT & PASSWORD
    private decimal? _topupAmountGPA;
    public decimal? TopupAmountGPA
    {
        get => _topupAmountGPA;
        set
        {
            _topupAmountGPA = value;
        }
    }
    private string _topupPasswordGPA;
    public string TopupPasswordGPA
    {
        get => _topupPasswordGPA;
        set
        {
            _topupPasswordGPA = value;
        }
    }

    #endregion

    #region WITHDRAWAL
    public List<UBBanks> InstapayBanks { get; private set; }
    public UBBanks SelectedBank { get; set; }
    private WithdrawRequestParamsDtlDTO _withdrawRequestUBParamsDtlDTO;
    public WithdrawRequestParamsDtlDTO WithdrawRequestUBParamsDtl
    {
        get => _withdrawRequestUBParamsDtlDTO;
        set
        {
            _withdrawRequestUBParamsDtlDTO = value;
        }
    }
    private WithdrawRequestParamsDtlDTO _withdrawRequestGCashParamsDtlDTO;
    public WithdrawRequestParamsDtlDTO WithdrawRequestGCashParamsDtl
    {
        get => _withdrawRequestGCashParamsDtlDTO;
        set
        {
            _withdrawRequestGCashParamsDtlDTO = value;
        }

    }

    // MIN & MAX AMOUNT
    private decimal _withdrawMinAmount;
    public decimal WithdrawMinAmount
    {
        get => _withdrawMinAmount;
        set
        {
            _withdrawMinAmount = value;
        }
    }
    private decimal _withdrawMaxAmount;
    public decimal WithdrawMaxAmount
    {
        get => _withdrawMaxAmount;
        set
        {
            _withdrawMaxAmount = value;
        }
    }

    // AMOUNT & PASSWORD
    private decimal? _withdrawAmount;
    public decimal? WithdrawAmount
    {
        get => _withdrawAmount;
        set
        {
            _withdrawAmount = value;
        }
    }
    private string _withdrawPassword;
    public string WithdrawPassword
    {
        get => _withdrawPassword;
        set
        {
            _withdrawPassword = value;
        }
    }

    // FEES
    private decimal _goplayWithdrawalFee;
    public decimal GoplayWithdrawalFee
    {
        get => _goplayWithdrawalFee;
        set
        {
            _goplayWithdrawalFee = value;
        }
    }
    private decimal _uBInstapayFee;
    public decimal UBInstapayFee
    {
        get => _uBInstapayFee;
        set
        {
            _uBInstapayFee = value;
        }
    }

    #endregion

    #region TRANSACTION HISTORY
    private ObservableCollection<TransactionModel> _transactionHistory;
    public ObservableCollection<TransactionModel> TransactionHistory
    {
        get => _transactionHistory;
        set
        {
            _transactionHistory = value;
            RaisePropertyChanged(() => TransactionHistory);
        }
    }
    public TransactionModel LatestTransaction { get; private set; }

    #endregion

    #endregion

    #region Life cycle methods
    public MainCreditViewModel(IConfiguration iconfig, IAccountService accountService, IConstantService constantService, IToastService toastService, ICurrentUser currentUser, IModalService popupModal,
        NavigationManager navigationManager, IReportService reportService, AuthenticationStateProvider AuthenticationStateProvider, ITransactionService transactionService, IPromotionService promotionService)
    {
        _iaccountService = accountService;
        _constantService = constantService;
        _toastService = toastService;
        _icurrentUser = currentUser;
        _popupModal = popupModal;
        _navigationManager = navigationManager;
        _reportService = reportService;
        _AuthenticationStateProvider = AuthenticationStateProvider;
        _transactionService = transactionService;
        _config = iconfig;
        _promotionService = promotionService;

        WithdrawRequestUBParamsDtl = new WithdrawRequestParamsDtlDTO();
        WithdrawRequestGCashParamsDtl = new WithdrawRequestParamsDtlDTO();

        WithdrawRequestUBParamsDtl.PaymentType = (int)GoPlayAsiaChannel.UB;
        WithdrawRequestGCashParamsDtl.PaymentType = (int)GoPlayAsiaChannel.GCash;

        Clear();

    }


    public async Task GetUserInfo()
    {
        //var popupRes = popupModal.Show<PopupLoading>("");
        var user = await _iaccountService.GetUser();
        if (user == null || user.User == null || user.User.Id < 1)
        {
            _toastService.ShowError("Failed to fetch user information");
            return;
        }
        UserInfo = user.User;
        _icurrentUser.Credits = UserInfo.Credits;
        _icurrentUser.CreditsDisp = string.Format("{0:0,0.00}", UserInfo.Credits);
        WithdrawRequestGCashParamsDtl.AccountNumber = UserInfo.MobileNumber;

        var corporationSettings = await _constantService.GetCorporationSettings();
        if (corporationSettings != null)
        {
            TopupMinAmount = corporationSettings.FirstMinimumCreditRequest.HasValue && corporationSettings.FirstMinimumCreditRequest > 0 ?
                corporationSettings.FirstMinimumCreditRequest.Value :
                1000;

            if (user.User.ToppedUp)
            {
                TopupMinAmount = corporationSettings.MinimumCreditRequest.HasValue && corporationSettings.MinimumCreditRequest > 0 ?
                corporationSettings.MinimumCreditRequest.Value :
                500;
            }

            await GetMinTopupPromotion();
            if (CorpVouchers != null)
            {
                foreach (var item in CorpVouchers)
                {
                    switch (item.Code)
                    {
                        case "MIN100TOPUP":
                            TopupMinAmount = item.Amount;
                            break;
                        default:
                            break;
                    }
                }
            }

            GoplayWithdrawalFee = corporationSettings.GoplayWithdrawalFee.HasValue && corporationSettings.GoplayWithdrawalFee > 0 ?
                corporationSettings.GoplayWithdrawalFee.Value :
                0;

            UBInstapayFee = corporationSettings.UBInstapayFee.HasValue && corporationSettings.UBInstapayFee > 0 ?
                corporationSettings.UBInstapayFee.Value : 0;

            TopupMaxAmount = corporationSettings.MaximumCreditRequest.HasValue && corporationSettings.MaximumCreditRequest > 0 ?
                corporationSettings.MaximumCreditRequest.Value :
                450000;

            WithdrawMinAmount = corporationSettings.MinimumWithdrawRequest.HasValue && corporationSettings.MinimumWithdrawRequest > 0 ?
                corporationSettings.MinimumWithdrawRequest.Value :
                1000;

            //   if ((UserInfo.Credits - UserInfo.HoldCredits) >= WithdrawMinAmount)
            //  {
            //   WithdrawMaxAmount = (decimal)(UserInfo.Credits - UserInfo.HoldCredits);
            //}
            //else
            //{
            WithdrawMaxAmount = corporationSettings.MaximumWithdrawRequest.HasValue && corporationSettings.MaximumWithdrawRequest > 0 ?
                corporationSettings.MaximumWithdrawRequest.Value :
                450000;
            //  }
        }
        else
        {
            TopupMinAmount = 1000;
            TopupMaxAmount = 450000;
            WithdrawMinAmount = 1000;
            WithdrawMaxAmount = 450000;
        }
        //** checks if there is hold amount **/

        if (_icurrentUser.HoldCredits > 0)
        {
            WithdrawMinAmount = 1000;
        }

        //for display purpose as topup minum var is used in valdiations
        if (activeVoucher != null)
        {
            TopupMinAmountDisplay = TopupMinAmount - (activeVoucher.Amount != null ? activeVoucher.Amount : 0);
        }
        else
        {
            TopupMinAmountDisplay = TopupMinAmount;
        }
        var parentUser = await _iaccountService.GetParentUser();
        if (parentUser == null)
        {
            _toastService.ShowError("Failed to fetch user information");
            return;
        }
        UserParentDtl = parentUser;


        #region Get Cashin Cashout Settings
        var CashinCashout = await _constantService.GetCashinCashoutSettings();
        if (CashinCashout is not null)
        {
            foreach (var item in CashinCashout)
            {
                if (item.TransactionType == Constants.Cashin)
                {
                    CashInSettings = item;
                }
                if (item.TransactionType == Constants.Cashout)
                {
                    CashOutSettings = item;
                }
            }
        }

        #endregion

        await CallInvoke();
        //popupRes.Close();
    }
    public async Task GetTransactionsByUserId()
    {
        var transactionHistory = await _reportService.GetTransactionsByUserId();
        if (transactionHistory != null && transactionHistory.Count > 0)
        {
            TransactionHistory = new ObservableCollection<TransactionModel>(transactionHistory);
        }
        else
        {
            TransactionHistory = new ObservableCollection<TransactionModel>();
        }
    }
    public async Task Clear()
    {
        TopupUBAmount = null;
        TopupAmount = null;
        TopupAmountGPA = null;
        TopupUBPassword = null;
        TopupPassword = null;
        TopupPasswordGPA = null;

        WithdrawAmount = null;
        WithdrawPassword = null;
    }

    #region TOPUP
    public async Task SetTopupAmount(decimal amount)
    {
        if (PaymentType == "UPAY")
        {
            TopupUBAmount = amount;
        }
        else if (PaymentType == "OTC")
        {
            TopupAmount = amount;
        }
        else if (PaymentType == "GPA GCASH")
        {
            TopupAmountGPA = amount;
        }
        await CallInvoke();
    }
    public async Task GetActiveVouchers()
    {
        try
        {
            ApplicableVouchers = await _promotionService.GetActiveVoucherForUser();
            activeVoucher = ApplicableVouchers.FirstOrDefault();
        }
        catch (Exception ex)
        {
            ApplicableVouchers = new List<voucherActiveModel>();
            activeVoucher = null;
            CallInvoke();
        }
    }
    private async Task GetMinTopupPromotion()
    {
        try
        {
            CorpVouchers = await _promotionService.GetMinTopupPromotion();
        }
        catch (Exception ex)
        {
            CorpVouchers = null;
        }
    }

    #region PG UPAY
    public async Task TopupUB()
    {
        decimal voucherAmnt = 0;
        if (activeVoucher != null)
        {
            if (activeVoucher.Count > 0)
            {
                voucherAmnt = activeVoucher.Amount;
            }
        }

        if (TopupUBAmount is null || TopupUBAmount + voucherAmnt < 1)
        {
            _toastService.ShowError("Amount cannot be empty");
            return;
        }
        if (TopupUBAmount + voucherAmnt < TopupMinAmount)
        {
            _toastService.ShowError("Topup amount cannot be lesser than minimum");
            return;
        }
        if (TopupUBAmount + voucherAmnt > TopupMaxAmount)
        {
            _toastService.ShowError("Topup amount cannot be greater than maximum");
            return;
        }

        var parameters = new ModalParameters();
        var popupRes = popupModal.Show<PopupLoading>("");

        List<voucherActiveModel>? voucherCode = new List<voucherActiveModel>();
        voucherCode.Add(activeVoucher);

        TopupRequestParamsDTO paramsModel = new TopupRequestParamsDTO()
        {
            UserId = _icurrentUser.Id,
            Password = TopupUBPassword,
            Amount = TopupUBAmount.Value,
            SelectionId = (int)TransactionSelectionTypes.PaymentChannel,
            Voucher = voucherCode,
        };

        var topupResult = await _transactionService.Topup(paramsModel);

        if (topupResult == null)
        {
            _toastService.ShowError("An error occured while attempting to create credit request");
            popupRes.Close();
            return;
        }
        if (!topupResult.Success || topupResult.Transaction == null)
        {
            _toastService.ShowError("An error occured while attempting to create credit request");
            popupRes.Close();
            return;
        }
        parameters = new ModalParameters();
        parameters.Add("Message", "Redirecting to payment portal please wait....");
        popupLoading = popupModal.Show<PopupLoading>("", parameters);

        _navigationManager.NavigateTo(topupResult.Transaction.TransactionUrl, true, true);

        //TopupUBAmount = 0;
        TopupUBPassword = "";
        popupRes.Close();
    }
    #endregion

    #region GPA OTC
    public async Task TopupDirectly()
    {
        voucherAmnt = 0;
        List<voucherActiveModel>? voucherCode = new List<voucherActiveModel>();
        if (activeVoucher is not null)
        {
            voucherAmnt = activeVoucher.Amount;
            voucherCode.Add(activeVoucher);
        }

        if (TopupAmount is null || TopupAmount + voucherAmnt < 1)
        {
            _toastService.ShowError("Amount cannot be empty");
            return;
        }
        if (TopupAmount + voucherAmnt < TopupMinAmount)
        {
            _toastService.ShowError("Topup amount cannot be lesser than minimum");
            return;
        }
        if (TopupAmount + voucherAmnt > TopupMaxAmount)
        {
            _toastService.ShowError("Topup amount cannot be greater than maximum");
            return;
        }
        var popupRes = popupModal.Show<PopupLoading>("");

        TopupRequestParamsDTO paramsModel = new TopupRequestParamsDTO()
        {
            UserId = _icurrentUser.Id,
            Password = TopupPassword,
            Amount = TopupAmount.Value,
            SelectionId = (int)TransactionSelectionTypes.OverTheCounter,
            Voucher = voucherCode
        };

        var topupResult = await _transactionService.Topup(paramsModel);

        if (topupResult == null)
        {
            _toastService.ShowError("An error occured while attempting to create credit request");
            return;
        }
        if (!topupResult.Success || topupResult.Transaction == null)
        {
            _toastService.ShowError("An error occured while attempting to create credit request");
            return;
        }
        _toastService.ShowSuccess($"{(TopupAmount.Value + voucherAmnt).ToString("N0")} Credit Request to Retailer: {UserParentDtl.ReferralKey} Submitted");

        TopupAmount = 0;
        TopupPassword = "";

        popupRes.Close();
    }
    #endregion

    #region GPA GCASH
    public async Task TopupGPA()
    {
        voucherAmnt = 0;
        List<voucherActiveModel>? voucherCode = new List<voucherActiveModel>();
        if (activeVoucher is not null)
        {
            voucherAmnt = activeVoucher.Amount;
            voucherCode.Add(activeVoucher);
        }

        if (TopupAmountGPA is null || TopupAmountGPA + voucherAmnt < 1)
        {
            _toastService.ShowError("Amount cannot be empty");
            return;
        }
        if (TopupAmountGPA + voucherAmnt < TopupMinAmount)
        {
            _toastService.ShowError("Topup amount cannot be lesser than minimum");
            return;
        }
        if (TopupAmountGPA + voucherAmnt > TopupMaxAmount)
        {
            _toastService.ShowError("Topup amount cannot be greater than maximum");
            return;
        }
        //if (string.IsNullOrEmpty(TopupPasswordGPA))
        //{
        //    _toastService.ShowError("Password cannot be empty");
        //    return;
        //}
        //var verifyPassword = await _iaccountService.VerifyPassword(_icurrentUser.Id, TopupPasswordGPA);

        //if (verifyPassword == null)
        //{
        //    _toastService.ShowError("An error occured while verifying password");
        //    return;
        //}
        //if (!verifyPassword.Success)
        //{
        //    _toastService.ShowError("Incorrect password entered...");
        //    return;
        //}

        //var msg = "";
        //if (voucherAmnt > 0)
        //{
        //    msg = "You are about to cash in  \u20b1" + (TopupAmountGPA.Value).ToString("N2") + " and a bonus token of  \u20b1" + (voucherAmnt).ToString("N2") + " will be added once approved. Continue?";
        //}
        //else
        //{
        //    msg = "You are about to cash in a total amount of  \u20b1" + (TopupAmountGPA.Value).ToString("N2") + ". Continue?";
        //}

        //var parameters = new ModalParameters();
        //parameters.Add("Message", msg);

        //var pop = _popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });
        //var result = await pop.Result;
        //if (!(bool)result.Data)
        //{
        //    return;
        //}

        var popupRes = popupModal.Show<PopupLoading>("");

        TopupRequestParamsDTO paramsModel = new TopupRequestParamsDTO()
        {
            UserId = _icurrentUser.Id,
            Password = TopupPasswordGPA,
            Amount = TopupAmountGPA.Value,
            SelectionId = (int)TransactionSelectionTypes.GoPlayAsia,
            Voucher = voucherCode
        };


        var topupResult = await _transactionService.Topup(paramsModel);

        if (topupResult == null)
        {
            _toastService.ShowError("An error occured while attempting to create credit request");
            popupRes.Close();
            return;
        }
        if (!topupResult.Success || topupResult.Transaction == null)
        {
            _toastService.ShowError(topupResult.Message);
            popupRes.Close();
            return;
        }

        var GCashAcc = await _constantService.GetActiveGCashAccount();
        if (GCashAcc is not null)
        {
            AccountName = GCashAcc.AccountName;
            AccountNumber = GCashAcc.AccountNumber;
        }
        else
        {
            _toastService.ShowError("An error occured while attempting to create credit request");
            popupRes.Close();
            return;
        }

        var popupGuide = popupModal.Show<GoplayGcashInstruction>("");
        ReferenceNumber = topupResult.Transaction.ReferenceId;


        _toastService.ShowSuccess($"{(TopupAmountGPA.Value + voucherAmnt).ToString("N0")} Credit Request to GoPlayAsia via GCash Submitted");

        TopupAmountGPA = 0;
        TopupPasswordGPA = "";

        popupRes.Close();
    }
    #endregion

    #endregion

    #region WITHDRAWAL
    public async Task Verification()
    {
        _navigationManager.NavigateTo("/VerifyReg");
    }
    public async Task SetWithdrawalAmount(decimal amount)
    {
        WithdrawAmount = amount;
        await CallInvoke();
    }
    public async Task GetInstapayBanks()
    {
        var Result = await _constantService.GetInstpayBanks();
        if (Result is not null)
        {
            InstapayBanks = Result.records;
        }
    }

    #region PG GCASH
    public async Task WithdrawUBGcash()
    {
        if (WithdrawAmount is null || WithdrawAmount < 1)
        {
            _toastService.ShowError("Amount cannot be empty");
            return;
        }
        if (WithdrawAmount < WithdrawMinAmount)
        {
            _toastService.ShowError("Withdraw amount cannot be lesser than minimum");
            return;
        }
        if (WithdrawAmount > WithdrawMaxAmount)
        {
            _toastService.ShowError("Withdraw amount cannot be greater than maximum");
            return;
        }
        if (string.IsNullOrEmpty(WithdrawRequestGCashParamsDtl.AccountNumber))
        {
            _toastService.ShowError("Union Bank Account Number is Required");
            return;
        }

        var user = await _iaccountService.GetUser();

        if (user == null || user.User == null)
        {
            _toastService.ShowError("An error occured while attempting to validate user");
            return;
        }
        if (!user.User.Credits.HasValue || user.User.Credits < 1)
        {
            _toastService.ShowError("Insufficient amount of tokens for transfer");
            return;
        }
        if (user.User.Credits < WithdrawAmount)
        {
            _toastService.ShowError("Insufficient amount of tokens for transfer");
            return;
        }
        if (user.User.Credits - user.User.HoldCredits < WithdrawAmount)
        {
            _toastService.ShowError("You can withdraw your tokens only after you have met the bonus wagering requirement (win at least 500 accumulated tokens)");
            return;
        }

        var parameters = new ModalParameters();
        var popOtp = _popupModal.Show<PopupRequestOtp>("", parameters);
        var resOtp = await popOtp.Result;
        if (!(bool)resOtp.Data)
        {
            return;
        }

        WithdrawRequestParamsDTO withdrawParam = new WithdrawRequestParamsDTO()
        {
            UserId = _icurrentUser.Id,
            Password = WithdrawPassword,
            Amount = WithdrawAmount.Value,
            SelectionId = (int)TransactionSelectionTypes.PaymentChannel,
            SubSelectionId = (int)WithdrawPaymentChannelTypes.GCash,
            AccountNo = WithdrawRequestGCashParamsDtl.AccountNumber,
            AccountName = UserInfo.FullName
            //SubSelectionId = TransactionInfo.SubSelectionId
        };

        var withdrawResult = await _transactionService.Withdraw(withdrawParam);

        if (withdrawResult == null)
        {
            _toastService.ShowError("An error occured while attempting to contact withdraw channel");
            return;
        }
        if (!withdrawResult.Success || withdrawResult.Transaction == null)
        {
            _toastService.ShowError("An error occured while attempting to contact withdraw channel");
            return;
        }

        _toastService.ShowSuccess(withdrawResult.Message);

        await ShowWithdrawalResults(withdrawResult.Transaction.Id, (WithdrawAmount.Value - WithdrawAmount.Value * GoplayWithdrawalFee - UBInstapayFee).ToString("N0"));
        WithdrawAmount = 0;
        WithdrawPassword = "";
        await GetUserInfo();
        //_navigationManager.NavigateTo("/WithdrawalStatus/" + withdrawResult.Transaction.Id);
        //await _navigationHelper.NavigateToAsync<ValidateTransactionRequestsViewModel>(paramsModel);
    }
    #endregion

    #region PG INSTAPAY
    public async Task WithdrawUBInstaPay()
    {
        if (SelectedBank is null)
        {
            _toastService.ShowError("Please input your bank");
            return;
        }
        if (string.IsNullOrEmpty(WithdrawRequestUBParamsDtl.AccountNumber))
        {
            _toastService.ShowError("Your Bank Account Number is Required");
            return;
        }
        if (WithdrawAmount is null || WithdrawAmount < 1)
        {
            _toastService.ShowError("Amount cannot be empty");
            return;
        }
        if (WithdrawAmount < WithdrawMinAmount)
        {
            _toastService.ShowError("Withdraw amount cannot be lesser than minimum");
            return;
        }
        if (WithdrawAmount > WithdrawMaxAmount)
        {
            _toastService.ShowError("Withdraw amount cannot be greater than maximum");
            return;
        }

        var user = await _iaccountService.GetUser();

        if (user == null || user.User == null)
        {
            _toastService.ShowError("An error occured while attempting to validate user");
            return;
        }
        if (!user.User.Credits.HasValue || user.User.Credits < 1)
        {
            _toastService.ShowError("Insufficient amount of tokens for transfer");
            return;
        }
        if (user.User.Credits < WithdrawAmount)
        {
            _toastService.ShowError("Insufficient amount of tokens for transfer");
            return;
        }
        if (user.User.Credits - user.User.HoldCredits < WithdrawAmount)
        {
            _toastService.ShowError("You can withdraw your tokens only after you have met the bonus wagering requirement (win at least 500 accumulated tokens)");
            return;
        }

        var parameters = new ModalParameters();
        var popOtp = _popupModal.Show<PopupRequestOtp>("", parameters);
        var resOtp = await popOtp.Result;
        if (!(bool)resOtp.Data)
        {
            return;
        }

        WithdrawRequestParamsDTO withdrawParam = new WithdrawRequestParamsDTO()
        {
            UserId = _icurrentUser.Id,
            Password = WithdrawPassword,
            Amount = WithdrawAmount.Value,
            SelectionId = (int)TransactionSelectionTypes.PaymentChannel,
            SubSelectionId = (int)WithdrawPaymentChannelTypes.UBInstapay,
            AccountNo = WithdrawRequestUBParamsDtl.AccountNumber,
            AccountName = UserInfo.FullName,
            ChannelCode = SelectedBank.code
            //SubSelectionId = TransactionInfo.SubSelectionId
        };

        var withdrawResult = await _transactionService.Withdraw(withdrawParam);

        if (withdrawResult == null)
        {
            _toastService.ShowError("An error occured while attempting to contact withdraw channel");
            return;
        }
        if (!withdrawResult.Success || withdrawResult.Transaction == null)
        {
            _toastService.ShowError("An error occured while attempting to contact withdraw channel");
            return;
        }

        _toastService.ShowSuccess(withdrawResult.Message);

        await ShowWithdrawalResults(withdrawResult.Transaction.Id, (WithdrawAmount.Value - WithdrawAmount.Value * GoplayWithdrawalFee - UBInstapayFee).ToString("N0"));
        WithdrawAmount = 0;
        WithdrawPassword = "";
        await GetUserInfo();
    }
    #endregion

    #region PG UNION BANK
    public async Task WithdrawUBToUB()
    {
        if (string.IsNullOrEmpty(WithdrawRequestUBParamsDtl.AccountNumber))
        {
            _toastService.ShowError("Union Bank Account Number is Required");
            return;
        }
        if (WithdrawAmount is null || WithdrawAmount < 1)
        {
            _toastService.ShowError("Amount cannot be empty");
            return;
        }
        if (WithdrawAmount < WithdrawMinAmount)
        {
            _toastService.ShowError("Withdraw amount cannot be lesser than minimum");
            return;
        }
        if (WithdrawAmount > WithdrawMaxAmount)
        {
            _toastService.ShowError("Withdraw amount cannot be greater than maximum");
            return;
        }

        var user = await _iaccountService.GetUser();

        if (user == null || user.User == null)
        {
            _toastService.ShowError("An error occured while attempting to validate user");
            return;
        }
        if (!user.User.Credits.HasValue || user.User.Credits < 1)
        {
            _toastService.ShowError("Insufficient amount of tokens for transfer");
            return;
        }
        if (user.User.Credits < WithdrawAmount)
        {
            _toastService.ShowError("Insufficient amount of tokens for transfer");
            return;
        }
        if (user.User.Credits - user.User.HoldCredits < WithdrawAmount)
        {
            _toastService.ShowError("You can withdraw your tokens only after you have met the bonus wagering requirement (win at least 500 accumulated tokens)");
            return;
        }

        var parameters = new ModalParameters();
        var popOtp = _popupModal.Show<PopupRequestOtp>("", parameters);
        var resOtp = await popOtp.Result;
        if (!(bool)resOtp.Data)
        {
            return;
        }

        WithdrawRequestParamsDTO withdrawParam = new WithdrawRequestParamsDTO()
        {
            UserId = _icurrentUser.Id,
            Password = WithdrawPassword,
            Amount = WithdrawAmount.Value,
            SelectionId = (int)TransactionSelectionTypes.PaymentChannel,
            SubSelectionId = (int)WithdrawPaymentChannelTypes.UB,
            AccountNo = WithdrawRequestUBParamsDtl.AccountNumber,
            AccountName = UserInfo.FullName
            //SubSelectionId = TransactionInfo.SubSelectionId
        };

        var withdrawResult = await _transactionService.Withdraw(withdrawParam);

        if (withdrawResult == null)
        {
            _toastService.ShowError("An error occured while attempting to contact withdraw channel");
            return;
        }
        if (!withdrawResult.Success || withdrawResult.Transaction == null)
        {
            _toastService.ShowError("An error occured while attempting to contact withdraw channel");
            return;
        }

        _toastService.ShowSuccess(withdrawResult.Message);

        await ShowWithdrawalResults(withdrawResult.Transaction.Id, (WithdrawAmount.Value - WithdrawAmount.Value * GoplayWithdrawalFee).ToString("N0"));
        WithdrawAmount = 0;
        WithdrawPassword = "";
        await GetUserInfo();
    }
    #endregion

    #region GPA OTC
    public async Task WithdrawDirectly()
    {
        if (WithdrawAmount is null || WithdrawAmount < 1)
        {
            _toastService.ShowError("Amount cannot be empty");
            return;
        }
        if (WithdrawAmount < WithdrawMinAmount)
        {
            _toastService.ShowError("Withdraw amount cannot be lesser than minimum");
            return;
        }
        if (WithdrawAmount > WithdrawMaxAmount)
        {
            _toastService.ShowError("Withdraw amount cannot be greater than maximum");
            return;
        }
        var user = await _iaccountService.GetUser();

        if (user == null || user.User == null)
        {
            _toastService.ShowError("An error occured while attempting to validate user");
            return;
        }
        if (!user.User.Credits.HasValue || user.User.Credits < 1)
        {
            _toastService.ShowError("Insufficient amount of tokens for transfer");
            return;
        }
        if (user.User.Credits < WithdrawAmount)
        {
            _toastService.ShowError("Insufficient amount of tokens for transfer");
            return;
        }
        if (user.User.Credits - user.User.HoldCredits < WithdrawAmount)
        {
            _toastService.ShowError("You can withdraw your tokens only after you have met the bonus wagering requirement (win at least 500 accumulated tokens)");
            return;
        }

        TransactionRequestsModel paramsModel = new TransactionRequestsModel()
        {
            Password = WithdrawPassword,
            Amount = WithdrawAmount.Value,
            SelectionId = (int)TransactionSelectionTypes.OverTheCounter,
            WithdrawRequest = true
            //SubSelectionId = (int)SelectedWithdrawOutlet.Id,
        };

        var parameters = new ModalParameters();
        parameters.Add("Password", WithdrawPassword);
        parameters.Add("Amount", WithdrawAmount);
        parameters.Add("SelectionId", (int)TransactionSelectionTypes.OverTheCounter);
        parameters.Add("ReferralKey", UserParentDtl.ReferralKey);
        var popOtp = _popupModal.Show<PopupRequestOtp>("", parameters);
        var resOtp = await popOtp.Result;
        if (!(bool)resOtp.Data)
        {
            return;
        }

        WithdrawRequestParamsDTO withdrawParam = new WithdrawRequestParamsDTO()
        {
            UserId = _icurrentUser.Id,
            Password = WithdrawPassword,
            Amount = WithdrawAmount.Value,
            SelectionId = (int)TransactionSelectionTypes.OverTheCounter
            //SubSelectionId = TransactionInfo.SubSelectionId
        };

        var withdrawResult = await _transactionService.Withdraw(withdrawParam);

        if (withdrawResult == null)
        {
            _toastService.ShowError("An error occured while attempting to contact withdraw channel");
            return;
        }
        if (!withdrawResult.Success || withdrawResult.Transaction == null)
        {
            _toastService.ShowError("An error occured while attempting to contact withdraw channel");
            return;
        }

        _toastService.ShowSuccess($"{WithdrawAmount.Value.ToString("N0")} Transfer Request to Retailer: {UserParentDtl.ReferralKey} Submitted");

        WithdrawAmount = 0;
        WithdrawPassword = "";
    }
    #endregion

    #region GPA GCASH
    public async Task WithdrawGCashGoPlay()
    {
        if (WithdrawAmount is null || WithdrawAmount < 1)
        {
            _toastService.ShowError("Amount cannot be empty");
            return;
        }
        if (WithdrawAmount < WithdrawMinAmount)
        {
            _toastService.ShowError("Withdraw amount cannot be lesser than minimum");
            return;
        }
        if (WithdrawAmount > WithdrawMaxAmount)
        {
            _toastService.ShowError("Withdraw amount cannot be greater than maximum");
            return;
        }
        //if (string.IsNullOrEmpty(WithdrawPassword))
        //{
        //    _toastService.ShowError("Password cannot be empty");
        //    return;
        //}
        if (string.IsNullOrEmpty(WithdrawRequestGCashParamsDtl.AccountNumber))
        {
            _toastService.ShowError("Union Bank Account Number is Required");
            return;
        }
        //var verifyPassword = await _iaccountService.VerifyPassword(_icurrentUser.Id, WithdrawPassword);

        //if (verifyPassword == null)
        //{
        //    _toastService.ShowError("An error occured while verifying password");
        //    return;
        //}
        //if (!verifyPassword.Success)
        //{
        //    _toastService.ShowError("Incorrect password entered...");
        //    return;
        //}

        var user = await _iaccountService.GetUser();

        if (user == null || user.User == null)
        {
            _toastService.ShowError("An error occured while attempting to validate user");
            return;
        }
        if (!user.User.Credits.HasValue || user.User.Credits < 1)
        {
            _toastService.ShowError("Insufficient amount of tokens for transfer");
            return;
        }
        if (user.User.Credits < WithdrawAmount)
        {
            _toastService.ShowError("Insufficient amount of tokens for transfer");
            return;
        }
        if (user.User.Credits - user.User.HoldCredits < WithdrawAmount)
        {
            _toastService.ShowError("You can withdraw your tokens only after you have met the bonus wagering requirement (win at least 500 accumulated tokens)");
            return;
        }

        //var param = new ModalParameters();
        //param.Add("Message", "You are about to receive a total amount of \u20b1" + (WithdrawAmount.Value - (WithdrawAmount.Value * GoplayWithdrawalFee)).ToString("N2") + ". Continue?");


        //var pop = _popupModal.Show<PopupConfirm>("", param, new ModalOptions() { Class = "op-modal" });
        //var result = await pop.Result;
        //if (!(bool)result.Data)
        //{
        //    return;
        //}

        var parameters = new ModalParameters();
        parameters.Add("Password", WithdrawPassword);
        parameters.Add("Amount", WithdrawAmount);
        parameters.Add("SelectionId", (int)TransactionSelectionTypes.GoPlayAsia);
        parameters.Add("ReferralKey", UserParentDtl.ReferralKey);
        var popOtp = _popupModal.Show<PopupRequestOtp>("", parameters);
        var resOtp = await popOtp.Result;
        if (!(bool)resOtp.Data)
        {
            return;
        }

        WithdrawRequestParamsDTO withdrawParam = new WithdrawRequestParamsDTO()
        {
            UserId = _icurrentUser.Id,
            Password = WithdrawPassword,
            Amount = WithdrawAmount.Value,
            SelectionId = (int)TransactionSelectionTypes.GoPlayAsia,
            SubSelectionId = (int)WithdrawPaymentChannelTypes.GCash,
            AccountNo = WithdrawRequestGCashParamsDtl.AccountNumber,
            AccountName = UserInfo.FullName
            //SubSelectionId = TransactionInfo.SubSelectionId
        };

        var withdrawResult = await _transactionService.Withdraw(withdrawParam);

        if (withdrawResult == null)
        {
            _toastService.ShowError("An error occured while attempting to contact withdraw channel");
            return;
        }
        if (!withdrawResult.Success || withdrawResult.Transaction == null)
        {
            _toastService.ShowError(withdrawResult.Message);
            return;
        }

        _toastService.ShowSuccess(withdrawResult.Message);

        WithdrawAmount = 0;
        WithdrawPassword = "";

        var uuser = await _iaccountService.GetUser();
        if (uuser == null || uuser.User == null || uuser.User.Id < 1)
        {
            _toastService.ShowError("Failed to fetch user information");
            return;
        }

        //await _navigationHelper.NavigateToAsync<ValidateTransactionRequestsViewModel>(paramsModel);
    }
    #endregion

    public async Task ShowWithdrawalResults(long tranid, string amount = "")
    {
        var parameters = new ModalParameters();
        parameters = new ModalParameters();
        parameters.Add("tranid", tranid);
        parameters.Add("amount", amount);
        var reswith = _popupModal.Show<WithdrawalStatus>("Withdrawal Receipt", parameters, new ModalOptions() { Class = "op-modal" });
        await reswith.Result;
        _navigationManager.NavigateTo("/lobby");
    }

    #endregion

    #region UNUSED
    public async Task WithdrawUBGoPlay()
    {
        if (WithdrawAmount < 1)
        {
            _toastService.ShowError("Amount cannot be empty");
            return;
        }
        if (WithdrawAmount < WithdrawMinAmount)
        {
            _toastService.ShowError("Withdraw amount cannot be lesser than minimum");
            return;
        }
        if (WithdrawAmount > WithdrawMaxAmount)
        {
            _toastService.ShowError("Withdraw amount cannot be greater than maximum");
            return;
        }
        if (string.IsNullOrEmpty(WithdrawPassword))
        {
            _toastService.ShowError("Password cannot be empty");
            return;
        }
        if (string.IsNullOrEmpty(WithdrawRequestUBParamsDtl.AccountNumber))
        {
            _toastService.ShowError("Union Bank Account Number is Required");
            return;
        }
        var verifyPassword = await _iaccountService.VerifyPassword(_icurrentUser.Id, WithdrawPassword);

        if (verifyPassword == null)
        {
            _toastService.ShowError("An error occured while verifying password");
            return;
        }
        if (!verifyPassword.Success)
        {
            _toastService.ShowError("Incorrect password entered...");
            return;
        }

        var user = await _iaccountService.GetUser();

        if (user == null || user.User == null)
        {
            _toastService.ShowError("An error occured while attempting to validate user");
            return;
        }
        if (!user.User.Credits.HasValue || user.User.Credits < 1)
        {
            _toastService.ShowError("Insufficient amount of tokens for transfer");
            return;
        }
        if (user.User.Credits < WithdrawAmount)
        {
            _toastService.ShowError("Insufficient amount of tokens for transfer");
            return;
        }
        if (user.User.Credits - user.User.HoldCredits < WithdrawAmount)
        {
            _toastService.ShowError("You can withdraw your tokens only after you have met the bonus wagering requirement (win at least 500 accumulated tokens)");
            return;
        }

        var parameters = new ModalParameters();
        parameters.Add("Password", WithdrawPassword);
        parameters.Add("Amount", WithdrawAmount);
        parameters.Add("SelectionId", (int)TransactionSelectionTypes.GoPlayAsia);
        parameters.Add("ReferralKey", UserParentDtl.ReferralKey);
        var popOtp = _popupModal.Show<PopupRequestOtp>("", parameters);
        var resOtp = await popOtp.Result;
        if (!(bool)resOtp.Data)
        {
            return;
        }

        WithdrawRequestParamsDTO withdrawParam = new WithdrawRequestParamsDTO()
        {
            UserId = _icurrentUser.Id,
            Password = WithdrawPassword,
            Amount = WithdrawAmount.Value,
            SelectionId = (int)TransactionSelectionTypes.GoPlayAsia,
            SubSelectionId = (int)WithdrawPaymentChannelTypes.UB,
            AccountNo = WithdrawRequestUBParamsDtl.AccountNumber,
            AccountName = UserInfo.FullName
            //SubSelectionId = TransactionInfo.SubSelectionId
        };

        var withdrawResult = await _transactionService.Withdraw(withdrawParam);

        if (withdrawResult == null)
        {
            _toastService.ShowError("An error occured while attempting to contact withdraw channel");
            return;
        }
        if (!withdrawResult.Success || withdrawResult.Transaction == null)
        {
            _toastService.ShowError("An error occured while attempting to contact withdraw channel");
            return;
        }

        _toastService.ShowSuccess(withdrawResult.Message);

        WithdrawAmount = 0;
        WithdrawPassword = "";
        await ShowWithdrawalResults(withdrawResult.Transaction.Id, (WithdrawAmount.Value - WithdrawAmount.Value * GoplayWithdrawalFee - UBInstapayFee).ToString("N0"));
        //await _navigationHelper.NavigateToAsync<ValidateTransactionRequestsViewModel>(paramsModel);
    }
    public async Task<TransactionModel> GetTransaction(long tranid)
    {
        LatestTransaction = await _transactionService.Transaction(tranid);
        return LatestTransaction;
    }
    #endregion
}

#endregion
