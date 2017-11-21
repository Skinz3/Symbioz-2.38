using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Actions
{
    [Brain(ActionType.Flee)]
    public class FleeAction : BrainAction
    {
        public FleeAction(BrainFighter fighter)
            : base(fighter)
        {

        }
        public override void Analyse()
        {
        }

        public override void Execute()
        {
            var path = Fighter.Team.LowerFighter().FindPathTo(Fighter).ToList();
            if (path.Count > 0)
                Fighter.Move(path);
        }
    }
}
