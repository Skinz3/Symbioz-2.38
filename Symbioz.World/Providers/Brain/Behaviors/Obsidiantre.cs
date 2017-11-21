using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.Protocol.Enums;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("Obsidiantre")]
    public class Obsidiantre : Behavior
    {
        public Obsidiantre(BrainFighter fighter) : base(fighter)
        {
            fighter.Fight.FightStartEvt += Fight_FightStartEvt;

        }

        private void Enemy_AfterSlideEvt(Fighter target, Fighter source, short startCellId, short endCellId)
        {
        }

        private void Fight_FightStartEvt(Models.Fights.Fight obj)
        {
            this.MakeInvulnerable(this.Fighter, 3);
        }

    }
}
