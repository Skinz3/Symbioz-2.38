using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records
{
    [Table("DelayedActions")]
    public class DelayedActionRecord : ITable
    {
        public static List<DelayedActionRecord> DelayedActions = new List<DelayedActionRecord>();

        [Primary]
        public int Id;

        public string ActionType;

        /// <summary>
        /// En secondes
        /// </summary>
        public int Interval;

        public string Value1;

        public string Value2;

        public DelayedActionRecord(int id, string actionType, int interval,
            string value1, string value2)
        {
            this.Id = id;
            this.ActionType = actionType;
            this.Interval = interval;
            this.Value1 = value1;
            this.Value2 = value2;
        }
    }
}
