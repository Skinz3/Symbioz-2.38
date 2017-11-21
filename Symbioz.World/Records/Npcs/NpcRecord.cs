using Symbioz.ORM;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Entities.Look;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Npcs
{
    [Table("Npcs", true, 2)]
    public class NpcRecord : ITable
    {
        public static List<NpcRecord> Npcs = new List<NpcRecord>();

        [Primary]
        public ushort Id;

        public string Name;

        public CSVDoubleArray Messages;

        public CSVDoubleArray Replies;

        public List<sbyte> ActionTypes;

        [Ignore]
        public List<NpcActionTypeEnum> ActionTypesEnum;

        public ContextActorLook Look;

        public NpcRecord(ushort id, string name, CSVDoubleArray messages, CSVDoubleArray replies, List<sbyte> actionTypes,
            ContextActorLook look)
        {
            this.Id = id;
            this.Name = name;
            this.Messages = messages;
            this.Replies = replies;
            this.ActionTypes = actionTypes;
            this.ActionTypesEnum = actionTypes.ConvertAll<NpcActionTypeEnum>(x => (NpcActionTypeEnum)x);
            this.Look = look;
            this.Look.SetColors(ContextActorLook.GetConvertedColors(this.Look.Colors.ToArray()));
        }

        public static NpcRecord GetNpc(ushort id)
        {
            return NpcRecord.Npcs.Find(x => x.Id == id);
        }
    }
}
