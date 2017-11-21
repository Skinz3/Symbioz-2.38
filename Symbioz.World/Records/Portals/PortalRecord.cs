using Symbioz.ORM;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities.Look;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Portals
{
    [Table("Portals")]
    public class PortalRecord : ITable
    {
        public static List<PortalRecord> Portals = new List<PortalRecord>();

        [Primary]
        public int Id;

        public string Name;

        public ContextActorLook Look;

        public ushort AreaId;

        public int TeleportMapId;

        public ushort TeleportCellId;

        public PortalRecord(int id, string name, ContextActorLook look, ushort areaId, int TeleportMapId,
            ushort TeleportCellId)
        {
            this.Id = id;
            this.Name = name;
            this.Look = look;
            this.AreaId = areaId;
            this.TeleportMapId = TeleportMapId;
            this.TeleportCellId = TeleportCellId;
        }

        public PortalInformation GetPortalInformation()
        {
            return new PortalInformation(Id, (short)AreaId);
        }

        public static PortalRecord GetPortal(int id)
        {
            return Portals.Find(x => x.Id == id);
        }
        public static PortalRecord GetPortal(string name)
        {
            return Portals.Find(x => x.Name == name);
        }
    }
}
