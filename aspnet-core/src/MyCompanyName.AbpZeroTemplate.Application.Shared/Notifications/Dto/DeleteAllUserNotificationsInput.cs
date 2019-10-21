using System;
using Abp.Notifications;

namespace MyCompanyName.AbpZeroTemplate.Notifications.Dto
{
    public class DeleteAllUserNotificationsInput
    {
        public UserNotificationState? State { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
