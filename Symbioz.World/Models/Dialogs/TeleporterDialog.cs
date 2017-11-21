using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Maps;
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
    public abstract class TeleporterDialog : Dialog
    {
        public override DialogTypeEnum DialogType
        {
            get
            {
                return DialogTypeEnum.DIALOG_TELEPORTER;
            }
        }
        public abstract TeleporterTypeEnum TeleporterType
        {
            get;
        }

        public int[] Maps;

        public ushort[] SubAreaIds;

        public ushort[] Costs;

        public TeleporterTypeEnum[] DestTeleporterType;

        public TeleporterDialog(Character character, MapInteractiveElementSkill skill)
            : base(character)
        {
            var zaapElements = InteractiveElementRecord.GetByElementType(skill.Element.Record.ElementType).ToArray();

            zaapElements = zaapElements.Where(x => x.GetSkillByActionType(skill.ActionType).Value1 == skill.Record.Value1).ToArray();

            this.Maps = new int[zaapElements.Length];
            this.SubAreaIds = new ushort[zaapElements.Length];
            this.Costs = new ushort[zaapElements.Length];
            this.DestTeleporterType = new TeleporterTypeEnum[zaapElements.Length];

            for (int i = 0; i < zaapElements.Length; i++)
            {
                var element = zaapElements[i];
                this.Maps[i] = element.MapId;
                this.SubAreaIds[i] = MapRecord.GetSubAreaId(element.MapId);
                this.Costs[i] = GetCost(skill.Element.Record.MapId, element.MapId);
                this.DestTeleporterType[i] = TeleporterType;
            }
        }
        public override void Close()
        {
            base.Close();
            LeaveDialogMessage();
        }
        public abstract void Teleport(MapRecord map);

        public abstract ushort GetCost(int teleporterMapId, int mapIdCurrent);

        public static ushort GetTeleporterCell(MapRecord map, InteractiveElementRecord ele)
        {
            ushort cellId = (ushort)ele.Point.GetCellInDirection(DirectionsEnum.DIRECTION_SOUTH_WEST, 1).CellId;

            if (!map.Walkable(cellId))
            {
                ushort[] array = map.CloseCells(ele.CellId);
                cellId = array.Length == 0 ? map.RandomWalkableCell() : array[0];
            }
            return cellId;
        }
    }
}
