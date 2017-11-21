using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.FightModels;

namespace Symbioz.World.Models.Fights.Fighters
{
    public class DoubleStaticFighter : DoubleFighter
    {
        public DoubleStaticFighter(CharacterFighter owner, FightTeam team, short cellId) : base(owner, team, cellId)
        {
        }
        public override void OnTurnStarted()
        {
            this.PassTurn();
        }
        public override bool InsertInTimeline()
        {
            return false;
        }
    }
}
