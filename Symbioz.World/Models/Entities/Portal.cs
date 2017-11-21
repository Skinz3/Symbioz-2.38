using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Maps.Instances;
using Symbioz.World.Records.Portals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Entities
{
    /// <summary>
    /// Représente un portail téléporteur.
    /// </summary>
    public class Portal : Entity
    {
        public override long Id
        {
            get { return m_Id; }
        }

        public override string Name
        {
            get
            {
                return Template.Name;
            }
        }

        public override ushort CellId
        {
            get;
            set;
        }

        public override DirectionsEnum Direction
        {
            get;
            set;
        }

        public override ContextActorLook Look
        {
            get
            {
                return Template.Look;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public PortalRecord Template
        {
            get;
            private set;
        }

        private long m_Id;

        public Portal(PortalRecord template, AbstractMapInstance instance)
        {
            this.Template = template;
            this.m_Id = instance.PopNextNPEntityId();
            this.CellId = instance.Record.RandomNoBorderCell();
            this.Direction = DirectionsEnum.DIRECTION_SOUTH;
            this.Map = instance.Record;
        }

        public void Use(Character character)
        {
            character.Teleport(Template.TeleportMapId, Template.TeleportCellId);
        }
        public override GameRolePlayActorInformations GetActorInformations()
        {
            return new GameRolePlayPortalInformations(Id, Look.ToEntityLook(), new EntityDispositionInformations((short)CellId, (sbyte)Direction),
                Template.GetPortalInformation());
        }
        public override string ToString()
        {
            return "Portal";
        }
    }
}
