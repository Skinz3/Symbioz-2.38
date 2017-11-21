using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.World.Providers.Fights.Effects;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Marks
{
    public class Portal : Mark
    {
        public Portal(short id, Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
         MapPoint centerPoint, Zone zone, Color color)
            : base(id, source, spellLevel, effect, centerPoint, zone, color,
            MarkTriggerTypeEnum.AFTER_MOVE)
        {
        }
        public override bool BreakMove
        {
            get
            {
                return true;
            }
        }

        public override GameActionMarkTypeEnum Type
        {
            get
            {
                return GameActionMarkTypeEnum.PORTAL;
            }
        }

        public void Unactive(Fighter source)
        {
            Active = false;
        }
      
        public override void Trigger(Fighter source, MarkTriggerTypeEnum type, object token = null)
        {
            if (Active)
            {
                if (this.Source.Team.GetActivePortalCount() >= 2)
                {
                    Tuple<Portal, Portal> pair = PortalProvider.Instance.GetPortalsTuple(Fight, source.CellId);

                    if (Fight.IsCellFree(pair.Item2.CenterPoint.CellId))
                        source.Teleport(Source, pair.Item2.CenterPoint);
                }
            }
        }
    }
}
