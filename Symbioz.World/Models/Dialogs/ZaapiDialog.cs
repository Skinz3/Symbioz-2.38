using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records;
using Symbioz.World.Records.Interactives;
using Symbioz.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Dialogs
{
    public class ZaapiDialog : TeleporterDialog
    {
        /// <summary>
        /// Coût de téléportation, en kamas
        /// </summary>
        public const ushort ZaapiTeleportCost = 20;

        public override TeleporterTypeEnum TeleporterType
        {
            get
            {
                return TeleporterTypeEnum.TELEPORTER_SUBWAY;
            }
        }

        public ZaapiDialog(Character character, MapInteractiveElementSkill skill)
            : base(character, skill)
        {

        }

        public override void Open()
        {
            this.Character.Client.Send(new TeleportDestinationsListMessage((sbyte)TeleporterType,
                Maps, SubAreaIds, Costs, DestTeleporterType.Select(x => (sbyte)x).ToArray()));
        }

        public override void Teleport(MapRecord map)
        {
            if (this.Maps.Contains(map.Id))
            {
                int cost = Costs[Maps.ToList().IndexOf(map.Id)];

                var zaapi = map.GetInteractiveByElementType(106); // IT Type Enum

                ushort cellId = zaapi != null ? GetTeleporterCell(map, zaapi) : map.RandomWalkableCell();

                if (this.Character.RemoveKamas(cost))
                {
                    this.Close();
                    this.Character.Teleport(map.Id, cellId);
                }
            }
        }
        public override ushort GetCost(int teleporterMapId, int mapIdCurrent)
        {
            return ZaapiTeleportCost;
        }


    }
}
