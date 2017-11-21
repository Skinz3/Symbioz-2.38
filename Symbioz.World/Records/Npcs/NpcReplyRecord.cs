using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Npcs
{
    [Table("NpcsReplies")]
    public class NpcReplyRecord : ITable
    {
        public static List<NpcReplyRecord> NpcsReplies = new List<NpcReplyRecord>();

        public ushort MessageId;

        [Primary]
        public ushort ReplyId;

        public string ActionType;

        public string Value1;

        public string Value2;

        public string Condition;

        public string ConditionExplanation;

        public NpcReplyRecord(ushort messageid,ushort replyid, string actiontype, string value1, string value2, string condition, string conditionexplanation)
        {
            this.MessageId = messageid;
            this.ReplyId = replyid;
            this.ActionType = actiontype;
            this.Value1 = value1;
            this.Value2 = value2;
            this.Condition = condition;
            this.ConditionExplanation = conditionexplanation;
        }

        public static List<NpcReplyRecord> GetNpcReplies(ushort messageid)
        {
            return NpcsReplies.FindAll(x => x.MessageId == messageid);
        }
    }
}
