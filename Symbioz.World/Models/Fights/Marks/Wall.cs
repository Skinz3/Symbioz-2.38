using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
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
    public class Wall : Mark
    {
        public const char WALL_SHAPE = 'L';

        public override GameActionMarkTypeEnum Type
        {
            get
            {
                return GameActionMarkTypeEnum.WALL;
            }
        }
        public override bool BreakMove
        {
            get
            {
                return true;
            }
        }
        public BombFighter FirstBomb
        {
            get;
            private set;
        }
        public BombFighter SecondBomb
        {
            get;
            private set;
        }
        public Wall(short id, Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
            MapPoint startPoint, Color color, BombFighter firstBomb, BombFighter secondBomb, byte delta, DirectionsEnum direction)
            : base(id, source, spellLevel, effect, startPoint, new Zone(WALL_SHAPE, delta, direction), color,
            MarkTriggerTypeEnum.ON_TURN_STARTED | MarkTriggerTypeEnum.ON_CAST | MarkTriggerTypeEnum.AFTER_MOVE)
        {
            this.FirstBomb = firstBomb;
            this.SecondBomb = secondBomb;
        }

        public bool Valid()
        {
            var cells = this.Zone.GetCells(CenterPoint.CellId, Fight.Map);

            var firstDirection = this.FirstBomb.Point.OrientationTo(SecondBomb.Point);
            var secondDirection = this.SecondBomb.Point.OrientationTo(FirstBomb.Point);

            var firstPoint = FirstBomb.Point.GetCellInDirection(firstDirection, 1);

            var secondPoint = SecondBomb.Point.GetCellInDirection(secondDirection, 1);


            if (!cells.Contains(firstPoint.CellId) || !cells.Contains(secondPoint.CellId))
            {
                return false;
            }

            foreach (var cell in cells)
            {
                var bomb = Fight.GetFighter(cell) as BombFighter;

                if (bomb != null && bomb.Owner == this.Source)
                {
                    return false;
                }
            }

            return true;
        }

        public override void Trigger(Fighter source, MarkTriggerTypeEnum type, object token)
        {
            bool seq = Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL);
            SpellEffectsManager.Instance.HandleEffects(Source, SpellLevel, source.Point, false);

            if (seq)
                Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);
        }
        public void Destroy()
        {
            FirstBomb.Walls.Remove(this);
            SecondBomb.Walls.Remove(this);
            Fight.RemoveMark(FirstBomb, this);
        }
    }
}
