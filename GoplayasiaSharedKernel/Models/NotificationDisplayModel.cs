using System.Collections.ObjectModel;

namespace GoplayasiaBlazor.Models
{
    public class NotificationDisplayModel
    {
        public string Date { get; set; }
        public ObservableCollection<NotificationModel> NotificationsList { get; set; }
    }
}
