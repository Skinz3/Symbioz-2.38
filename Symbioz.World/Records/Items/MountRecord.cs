using Symbioz.ORM;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities.Look;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Items
{
    [Table("Mounts")]
    public class MountRecord : ITable
    {
        public static List<MountRecord> Mounts = new List<MountRecord>();

        [Primary]
        public int Id;

        public string Name;

        public ContextActorLook Look;

        [Update]
        public ushort ItemGId;

        [Xml, Update]
        public List<EffectInstance> Effects;

        public MountRecord(int id, string name, ContextActorLook look, ushort itemGId, List<EffectInstance> effects)
        {
            this.Id = id;
            this.Name = name;
            this.Look = look;
            this.ItemGId = itemGId;
            this.Effects = effects;
        }

        public static MountRecord GetMount(ushort itemGID)
        {
            return Mounts.Find(x => x.ItemGId == itemGID);
        }
        public static MountRecord GetMount(int modelId)
        {
            return Mounts.Find(x => x.Id == modelId);
        }

    }
}
