using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("AngelRush")]
    public class AngelRush : Behavior
    {
        public const ushort SpellId = 7355;

        private bool IsAngel
        {
            get;
            set;
        }
        private SpellRecord SpellRecord
        {
            get;
            set;
        }
        public AngelRush(BrainFighter fighter)
            : base(fighter)
        {
            this.SpellRecord = SpellRecord.GetSpellRecord(SpellId);
            this.Fighter.AfterSlideEvt += Fighter_AfterSlideEvt;
            this.Fighter.AfterDeadEvt += Fighter_AfterDeadEvt;
            this.Fighter.OnDamageTaken += Fighter_OnDamageTaken;
            this.IsAngel = false;
        }

        private void Fighter_OnDamageTaken(Fighter arg1, Models.Fights.Damages.Damage arg2)
        {
            if (!IsAngel)
            {
                Fighter.ForceSpellCast(SpellRecord.GetLastLevel(), Fighter.CellId);
                this.IsAngel = true;
            }
        }

        private void Fighter_AfterDeadEvt(Fighter obj,bool recursiveCall)
        {
            if (IsAngel)
            {
                foreach (var ally in Fighter.Team.GetFighters())
                {
                    ally.ForceSpellCast(SpellRecord.GetLastLevel(), ally.CellId);
                }
            }
        }

        private void Fighter_AfterSlideEvt(Fighter target, Fighter source, short startCellId, short endCellId)
        {
            Fighter.ForceSpellCast(SpellRecord.GetLastLevel(), Fighter.CellId);
            this.IsAngel = true;
        }



    }
}
