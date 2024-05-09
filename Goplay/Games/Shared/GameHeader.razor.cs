using Blazored.Modal.Services;
using Blazored.Modal;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;
using GoplayasiaBlazor.Models;
using System.Collections.ObjectModel;

namespace GoPlayAsiaWebApp.Goplay.Games.Shared
{
    public partial class GameHeader
    {
        [CascadingParameter] public IModalService popupModal { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }

        [Parameter] public string StreamId { get; set; }
        [Parameter] public string RoundTimer { get; set; }
        [Parameter] public int? MaxRoundTimer { get; set; }
        [Parameter] public string RoundStatusString { get; set; }
        [Parameter] public int RoundNumber { get; set; }
        [Parameter] public string TimerBackground { get; set; }
        [Parameter] public string GameResultString { get; set; }
        [Parameter] public string ShowFlashing { get; set; }
        [Parameter] public string GifSrc { get; set; }
        [Parameter] public bool IsDisabledCategory { get; set; }

        [Parameter] public string? CreditsDisp { get; set; }
        [Parameter] public ObservableCollection<PlayerCategoryModel> PlayerCategory { get; set; }
        [Parameter] public EventCallback<string> OnClickCallback { get; set; }
        [Parameter]
        public string Token { get; set; }

        private async Task popGamerules()
        {
            _navigationManager.NavigateTo("/gamerules/" + StreamId);
        }
        private async Task logout()
        {
            var parameters = new ModalParameters();
            parameters.Add("Message", "Confirm Logout");
            var popupConfirm = popupModal.Show<PopupLogout>("", parameters, new ModalOptions() { Class = "op-modal" });
            ModalResult result = await popupConfirm.Result;
            if (result.Data != null)
            {
                if ((bool)result.Data)
                {
                    await gameHeaderViewModel.Logout();
                }
            }
        }
        private async Task setPlayerCategory(int categoryId)
        {
            iCurrentUser.CategoryId = categoryId;
            await OnClickCallback.InvokeAsync();
        }
        private async Task onclick_recharge()
        {
            _navigationManager.NavigateTo("/Credit");
        }
        private async Task onclick_Lobby()
        {
            _navigationManager.NavigateTo("/home");
        }

        #region UNUSED
        protected override void OnInitialized()
        {

        }
        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }
        #endregion

    }
}
