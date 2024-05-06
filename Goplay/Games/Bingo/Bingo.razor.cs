using Blazored.Modal;
using Blazored.Modal.Services;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Games.Bingo
{
    public partial class Bingo
    {
        [CascadingParameter] public IModalService popupModal { get; set; }

        public IModalReference popupRef { get; set; }
        public int chip { get; set; } = 2;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                _iBingoViewModel.Notify += OnNotify;
                _iBingoViewModel.popupModal = popupModal;

                await _iBingoViewModel.ConnectSignalR();
                await _iBingoViewModel.AssignSignalRMethods();
                popupRef = popupModal.Show<PopupLoading>("");

                List<Task> TaskList = new List<Task>();
                TaskList.Add(_iBingoViewModel.GetPlayerCategory());
                TaskList.Add(_iBingoViewModel.GetGameChipsByCategory());
                TaskList.Add(_iBingoViewModel.GetCurrentRoundBets());
                _iBingoViewModel.BetAmount = chip;
                TaskList.Add(_iBingoViewModel.GetGetPrizeByChip(chip));
                if (_iBingoViewModel.streamKey == string.Empty)
                    TaskList.Add(_iBingoViewModel.GenerateStreamKey());
                //TaskList.Add(_iBingoViewModel.GetTrends());
                await Task.WhenAll(TaskList.ToArray());

                await _iBingoViewModel.GetGameSetting();
                await _iBingoViewModel.GetGameRound();


                popupRef.Close();

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
    }

}
