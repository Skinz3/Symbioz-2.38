using Symbioz.ORM;
using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Npcs
{
    [Table("NpcsActions")]
    public class NpcActionRecord : ITable
    {
        public static List<NpcActionRecord> NpcsActions = new List<NpcActionRecord>();

        [Primary]
        public long Id;

        public long NpcId;

        public sbyte ActionId;

        [Ignore]
        public NpcActionTypeEnum ActionIdEnum { get { return (NpcActionTypeEnum)ActionId; } }

        public string Value1;

        public string Value2;

        public NpcActionRecord(long id, long npcid, sbyte actionid, string value1, string value2)
        {
            this.Id = id;
            this.NpcId = npcid;
            this.ActionId = actionid;
            this.Value1 = value1;
            this.Value2 = value2;
        }
        public static List<NpcActionRecord> GetActions(long npcid)
        {
            return NpcsActions.FindAll(x => x.NpcId == npcid);
        }
        public static List<NpcActionRecord> GetActionByType(NpcActionTypeEnum type)
        {
            return NpcsActions.FindAll(x => x.ActionIdEnum == type);
        }
    }
}
