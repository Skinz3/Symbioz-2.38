using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Results
{
    public class FightResult<T> : IFightResult where T : Fighter
    {
        public T Fighter
        {
            get;
            protected set;
        }
        public bool Alive
        {
            get
            {
                T fighter = this.Fighter;
                bool arg_30_0;
                if (fighter.Alive)
                {
                    fighter = this.Fighter;
                    arg_30_0 = !fighter.Left;
                }
                else
                {
                    arg_30_0 = false;
                }
                return arg_30_0;
            }
        }
        public int Id
        {
            get
            {
                T fighter = this.Fighter;
                return fighter.Id;
            }
        }
        public int Prospecting
        {
            get
            {
                T fighter = this.Fighter;
                return fighter.Stats.Prospecting.TotalInContext();
            }
        }
        public int Wisdom
        {
            get
            {
                T fighter = this.Fighter;
                return fighter.Stats.Prospecting.TotalInContext();
            }
        }
        public int Level
        {
            get
            {
                T fighter = this.Fighter;
                return (int)fighter.Level;
            }
        }
        public Fight Fight
        {
            get
            {
                T fighter = this.Fighter;
                return fighter.Fight;
            }
        }
        public Loot Loot
        {
            get;
            protected set;
        }
        public FightOutcomeEnum Outcome
        {
            get;
            protected set;
        }
        public FightResult(T fighter, FightOutcomeEnum outcome, Loot loot)
        {
            this.Fighter = fighter;
            this.Outcome = outcome;
            this.Loot = loot;
        }
        public virtual bool CanLoot(FightTeam team)
        {
            return false;
        }
        public virtual FightResultListEntry GetFightResultListEntry()
        {
            return new FightResultFighterListEntry((ushort)this.Outcome, 0, this.Loot.GetFightLoot(), this.Id, this.Alive);
        }
        public virtual void Apply()
        {
        }
    }
    public class FightResult : FightResult<Fighter>
    {
        public FightResult(Fighter fighter, FightOutcomeEnum outcome, Loot loot)
            : base(fighter, outcome, loot)
        {
        }
    }
}
