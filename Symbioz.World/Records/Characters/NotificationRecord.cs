using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Core;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Characters
{
    [Table("Notifications"),Resettable]
    public class NotificationRecord : ITable
    {
        public static List<NotificationRecord> Notifications = new List<NotificationRecord>();

        [Primary]
        public int Id;

        public int AccountId;

        public string Notification;

        public NotificationRecord(int id, int accountId, string notification)
        {
            this.Id = id;
            this.AccountId = accountId;
            this.Notification = notification;
        }

        public static NotificationRecord[] GetConnectionNotifications(int accountId)
        {
            return Notifications.FindAll(x => x.AccountId == accountId).ToArray();
        }
        public static void Add(int accountId, string notification)
        {
            new NotificationRecord(Notifications.DynamicPop(x => x.Id), accountId, notification).AddElement();
        }
    }
}
