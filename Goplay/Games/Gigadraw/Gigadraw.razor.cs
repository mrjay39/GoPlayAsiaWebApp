using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Goplay.Games.Gigadraw
{
    public partial class Gigadraw
    {
        [CascadingParameter] public IModalService popupModal { get; set; }
        public IModalReference popupRef { get; set; }
        public IModalReference popupwinner { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                iGigadrawModel.Notify += OnNotify;
                iGigadrawModel.popupModal = popupModal;

                await iGigadrawModel.ConnectSignalR();
                await iGigadrawModel.AssignSignalRMethods();
                popupRef = popupModal.Show<PopupLoading>("");

                await iGigadrawModel.GetGameSetting();
                await iGigadrawModel.GetGameRound();
                await iGigadrawModel.GetTrends();
                await iGigadrawModel.GetCurrentRoundBets();
                if (iGigadrawModel.streamKey == string.Empty)
                    await iGigadrawModel.GenerateStreamKey();
                popupRef.Close();
            }
            catch (Exception)
            {

                //throw;
            }

        }
        protected override void OnAfterRender(bool firstRender)
        {

        }

        public async void OnNotify()
        {
            await InvokeAsync(() => StateHasChanged());
        }

        public void Dispose()
        {

            //iGigadrawModel.DisconnectSignalR();
        }
        public async ValueTask DisposeAsync()
        {
            iGigadrawModel.Notify -= OnNotify;
            //await iGigadrawModel.DisconnectSignalR();
        }
    }
}
