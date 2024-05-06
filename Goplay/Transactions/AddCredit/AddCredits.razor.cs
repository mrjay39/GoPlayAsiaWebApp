using Blazored.Modal;
using Blazored.Modal.Services;
using GoPlayAsiaWebApp.Goplay.Games.Lucky9;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Transactions.AddCredit
{

    public partial class AddCredits
    {
        #region Injected Services
        [Inject] MainCreditViewModel _mainCreditsViewModel { get; set; }

        [CascadingParameter] public IModalService popupModal { get; set; }

        private bool hideAddCredit = true;
        private bool hideAddCreditGPA = true;
        private bool hideUBAddCredit = true;

        private bool hideAddCreditbtn = false;
        private bool hideAddCreditGPAbtn = false;
        private bool hideUBAddCreditbtn = false;
        #endregion
        [CascadingParameter] public IModalService popselVoucher { get; set; }
        public string showpass = "password";
        public string showpassicon = "bi bi-eye-fill";

        #region Local Methods
        private async Task UpdateState()
        {
            StateHasChanged();
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

        #region SHOW FORM
        public async Task showUBAddCreditForm()
        {
            await _mainCreditsViewModel.Clear();
            if (hideUBAddCredit)
            {
                hideAddCreditbtn = true;
                hideAddCreditGPAbtn = true;
                hideUBAddCreditbtn = false;

                hideUBAddCredit = false;
                hideAddCreditGPA = true;
                hideAddCredit = true;
                _mainCreditsViewModel.PaymentType = "UPAY";
            }
            else
            {
                hideAddCreditbtn = false;
                hideAddCreditGPAbtn = false;
                hideUBAddCreditbtn = false;

                hideUBAddCredit = true;
                hideAddCreditGPA = true;
                hideAddCredit = true;
                _mainCreditsViewModel.PaymentType = "";
            }
        }
        public async Task showAddCreditForm()
        {
            await _mainCreditsViewModel.Clear();
            if (hideAddCredit)
            {
                hideAddCreditbtn = false;
                hideAddCreditGPAbtn = true;
                hideUBAddCreditbtn = true;

                hideAddCredit = false;
                hideAddCreditGPA = true;
                hideUBAddCredit = true;
                _mainCreditsViewModel.PaymentType = "OTC";
            }
            else
            {
                hideAddCreditbtn = false;
                hideAddCreditGPAbtn = false;
                hideUBAddCreditbtn = false;

                hideAddCredit = true;
                hideAddCreditGPA = true;
                hideUBAddCredit = true;
                _mainCreditsViewModel.PaymentType = "";
            }
        }
        #endregion

        #region PG UPAY
        private async Task RequestTopupUBCredit()
        {
            await _mainCreditsViewModel.TopupUB();
        }
        #endregion

        #region GPA OTC
        private async Task RequestTopupCredit()
        {
            await _mainCreditsViewModel.TopupDirectly();
            await _mainCreditsViewModel.GetActiveVouchers();
        }
        #endregion

        #region GPA GCASH
        private async Task RequestTopupGPACredit()
        {
            await _mainCreditsViewModel.TopupGPA();
        }
        #endregion

        #endregion

        protected override async Task OnInitializedAsync()
        {
            _mainCreditsViewModel.popupModal = popupModal;

            //await _mainCreditsViewModel.ConnectSignalR();
        }

        #region UNUSED
        //public async Task showAddCreditGPAForm()
        //{
        //    await _mainCreditsViewModel.Clear();
        //    if (hideAddCreditGPA)
        //    {
        //        hideAddCreditbtn = true;
        //        hideAddCreditGPAbtn = false;
        //        hideUBAddCreditbtn = true;

        //        hideAddCredit = true;
        //        hideAddCreditGPA = false;
        //        hideUBAddCredit = true;
        //        _mainCreditsViewModel.PaymentType = "GPA GCASH";
        //    }
        //    else
        //    {
        //        hideAddCreditbtn = false;
        //        hideAddCreditGPAbtn = false;
        //        hideUBAddCreditbtn = false;

        //        hideAddCredit = true;
        //        hideAddCreditGPA = true;
        //        hideUBAddCredit = true;
        //        _mainCreditsViewModel.PaymentType = "";
        //    }
        //}
        //public async ValueTask DisposeAsync()
        //{
        //    //await _mainCreditsViewModel.DisconnectSignalR();
        //}
        #endregion
    }
}
