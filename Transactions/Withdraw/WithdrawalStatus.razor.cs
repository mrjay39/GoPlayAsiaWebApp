using Blazored.Modal.Services;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Transactions.Withdraw;

public partial class WithdrawalStatus
{
    [Parameter]
    public long tranid { get; set; }
    [Parameter]
    public string amount { get; set; }
    public TransactionModel Transaction { get; set; }
    [CascadingParameter] public IModalService popupModal { get; set; }
    protected override async Task OnParametersSetAsync()
    {
        var popupRes = popupModal.Show<PopupLoading>("");
        Transaction = await _transactionService.Transaction(tranid);
        popupRes.Close();
    }
}
