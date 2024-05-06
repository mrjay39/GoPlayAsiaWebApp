using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Games.DropwinS5
{
    public partial class DropwinS5
    {
        [Inject]
        DropwinS5ViewModel iDropWinS5Model { get; set; }
        [CascadingParameter] public IModalService popupModal { get; set; }
        public IModalReference popupRef { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                iDropWinS5Model.Notify += OnNotify;
                iDropWinS5Model.popupModal = popupModal;
                popupRef = popupModal.Show<PopupLoading>("");
                await iDropWinS5Model.GetGameSetting();
                await iDropWinS5Model.GetGameRound();
                await iDropWinS5Model.GetGameChips();
                await iDropWinS5Model.GetCurrentRoundBets();
                await iDropWinS5Model.GenerateStreamKey();
                popupRef.Close();
                await iDropWinS5Model.GetTrends();
                await iDropWinS5Model.ConnectSignalR();
                await iDropWinS5Model.AssignSignalRMethods();

                //await Task.Delay(1000);
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

            //iDropWinModel.DisconnectSignalR();
        }
        public async ValueTask DisposeAsync()
        {
            iDropWinS5Model.Notify -= OnNotify;
            //await iDropWinS5Model.DisconnectSignalR();
        }
    }
}
