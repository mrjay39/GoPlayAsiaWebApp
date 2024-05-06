using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.DTOs
{
    public class NotificationDTO
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public int? NotificationType { get; set; }
        public long? ReferenceId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; }
        public DateTime? DateCreated { get; set; }

        public DateTime DateNow { get; set; }
        public DateTime? ConvertedDateCreated { get; set; }
        public TimeSpan? DateDifference { get; set; }
        public string DateSent { get; set; }

    }
}
