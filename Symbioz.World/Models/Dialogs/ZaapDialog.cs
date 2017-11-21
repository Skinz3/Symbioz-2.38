using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Maps;
using Symbioz.World.Network;
using Symbioz.World.Providers;
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
    public class ZaapDialog : TeleporterDialog
    {
        public override TeleporterTypeEnum TeleporterType
        {
            get
            {
                return TeleporterTypeEnum.TELEPORTER_ZAAP;
            }
        }

        public ZaapDialog(Character character, MapInteractiveElementSkill skill)
            : base(character, skill)
        {

        }

        public override void Open()
        {
            this.Character.Client.Send(new ZaapListMessage((sbyte)TeleporterTypeEnum.TELEPORTER_ZAAP
                  , Maps, SubAreaIds, Costs, DestTeleporterType.Select(x => (sbyte)x).ToArray(), Character.Record.SpawnPointMapId));
        }

        public override void Teleport(MapRecord map)
        {
            if (this.Maps.Contains(map.Id))
            {
                int cost = Costs[Maps.ToList().IndexOf(map.Id)];
                ushort cellId;

                var zaap = map.Zaap;
                cellId = zaap != null ? GetTeleporterCell(map, zaap) : map.RandomWalkableCell();

                if (this.Character.RemoveKamas(cost))
                {
                    this.Close();
                    this.Character.Teleport(map.Id, cellId);
                }
            }
        }

        public override ushort GetCost(int teleporterMapId, int mapIdCurrent)
        {
            return FormulasProvider.Instance.GetZaapCost(MapRecord.GetMap(teleporterMapId), MapRecord.GetMap(mapIdCurrent));
        }
    }
}
