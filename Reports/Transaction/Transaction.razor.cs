using Blazored.Modal.Services;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Reports.Transaction
{
    public partial class Transaction
    {
        #region Injected Services
        [Inject] MainCreditViewModel _mainCreditsViewModel { get; set; }

        [CascadingParameter] public IModalService popupModal { get; set; }

        #endregion

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
         

        }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                _mainCreditsViewModel.popupModal = popupModal;
                _mainCreditsViewModel.Notify += OnNotify;
                await _mainCreditsViewModel.GetTransactionsByUserId();

            }
            catch (Exception)
            {

                //throw;
            }
        }
        public async void OnNotify()
        {
            await InvokeAsync(() => StateHasChanged());
        }

        public async ValueTask DisposeAsync()
        {
            _mainCreditsViewModel.Notify -= OnNotify;
            //await _mainCreditsViewModel.DisconnectSignalR();
        }

    }
}
