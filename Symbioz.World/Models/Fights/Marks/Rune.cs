using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Marks
{
    public class Rune : Mark
    {
        public override GameActionMarkTypeEnum Type
        {
            get
            {
                return GameActionMarkTypeEnum.RUNE;
            }
        }
        public override bool BreakMove
        {
            get
            {
                return false;
            }
        }
        public Rune(short id, Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
          MapPoint centerPoint, Zone zone, Color color)
            : base(id, source, spellLevel, effect, centerPoint, zone, color, MarkTriggerTypeEnum.NONE)
        {

        }


        public override void Trigger(Fighter source, MarkTriggerTypeEnum type, object token)
        {
        }
        internal void Activate(Fighter source)
        {
            this.Source.ForceSpellCast(TriggerSpell, SpellLevel.Grade, this.CenterPoint.CellId);
            this.Fight.RemoveMark(source, this);
        }
    }
}
