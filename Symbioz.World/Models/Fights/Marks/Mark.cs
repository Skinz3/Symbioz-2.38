using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Marks.Shapes;
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
    public abstract class Mark
    {
        public abstract bool BreakMove
        {
            get;
        }
        public abstract GameActionMarkTypeEnum Type
        {
            get;
        }
        public short Id
        {
            get;
            private set;
        }
        public Fighter Source
        {
            get;
            private set;
        }
        public Fight Fight
        {
            get
            {
                return Source.Fight;
            }
        }
        public SpellLevelRecord SpellLevel
        {
            get;
            private set;
        }
        public EffectInstance BaseEffect
        {
            get;
            private set;
        }
        public MapPoint CenterPoint
        {
            get;
            private set;
        }
        public Zone Zone
        {
            get;
            private set;
        }
        public MarkShape[] Shapes
        {
            get;
            private set;
        }
        protected Color Color
        {
            get;
            set;
        }
        protected SpellRecord TriggerSpell
        {
            get;
            private set;
        }
        public MarkTriggerTypeEnum TriggerType
        {
            get;
            private set;
        }
        public virtual bool Active
        {
            get;
            protected set;
        }
        public bool ContainsCell(short cellId)
        {
            return this.Shapes.Any((MarkShape entry) => entry.Point.CellId == cellId);
        }
        protected Mark(short id, Fighter source, SpellLevelRecord spellLevel, EffectInstance effect, MapPoint centerPoint, Zone zone,
            Color color, MarkTriggerTypeEnum triggerType)
        {
            this.Id = id;
            this.Source = source;
            this.SpellLevel = spellLevel;
            this.BaseEffect = effect;
            this.CenterPoint = centerPoint;
            this.Zone = zone;
            this.Color = color;
            this.TriggerSpell = SpellRecord.GetSpellRecord(effect.DiceMin);
            this.BuildShapes();
            this.TriggerType = triggerType;
            this.Active = true;
        }
        private void BuildShapes()
        {
            short[] cells = GetCells();

            Shapes = new MarkShape[cells.Length];

            for (int i = 0; i < cells.Length; i++)
            {
                Shapes[i] = new MarkShape(Source.Fight, new MapPoint(cells[i]), Color);
            }
        }
        public short[] GetCells()
        {
            return Zone.GetCells(CenterPoint.CellId, Source.Fight.Map);
        }
        public virtual GameActionMark GetGameActionMark()
        {
            return new GameActionMark(Source.Id, Source.Team.Id, SpellLevel.SpellId, SpellLevel.Grade, Id, (sbyte)Type,
              CenterPoint.CellId, (from entry in this.Shapes
                                   select entry.GetGameActionMarkedCell()).ToArray(), Active);
        }

        public virtual void OnFighterEnter(Fighter fighter)
        {

        }
        public virtual void OnFighterLeave(Fighter fighter)
        {

        }
        public abstract void Trigger(Fighter source, MarkTriggerTypeEnum type, object token = null);

        public virtual GameActionMark GetHiddenGameActionMark()
        {
            return null;
        }
        public virtual bool IsVisibleFor(Fighter fighter)
        {
            return true;
        }
    }

}
