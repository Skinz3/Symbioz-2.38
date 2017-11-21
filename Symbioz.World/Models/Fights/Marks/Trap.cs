using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Marks.Shapes;
using Symbioz.World.Models.Fights.Spells;
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
    public class Trap : Mark
    {
        public override GameActionMarkTypeEnum Type
        {
            get
            {
                return GameActionMarkTypeEnum.TRAP;
            }
        }
        public override bool BreakMove
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// Obtenir le MarkCellsTypeEnum en fonction de l'effet => C : lozenge, G : Cross, sinon Square cells par cells
        /// </summary>
        /// <param name="id"></param>
        /// <param name="source"></param>
        /// <param name="spellLevel"></param>
        /// <param name="effect"></param>
        /// <param name="centerPoint"></param>
        /// <param name="size"></param>
        public Trap(short id, Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
            MapPoint centerPoint, Zone zone, Color color)
            : base(id, source, spellLevel, effect, centerPoint, zone, color, MarkTriggerTypeEnum.AFTER_MOVE)
        {

        }


        public override GameActionMark GetHiddenGameActionMark()
        {
            return new GameActionMark(Source.Id, Source.Team.Id, 1, 1, Id, (sbyte)Type,
               -1, new GameActionMarkedCell[0], true);
        }

        public override bool IsVisibleFor(Fighter fighter)
        {
            return fighter.IsFriendly(Source);
        }
        public override void Trigger(Fighter source, MarkTriggerTypeEnum type, object token)
        {
            SpellLevelRecord triggerLevel = TriggerSpell.GetLevel(SpellLevel.Grade);
            this.Fight.RemoveMark(source, this);
            SpellEffectsManager.Instance.HandleEffects(Source, triggerLevel, CenterPoint, false);
        }


    }
}
