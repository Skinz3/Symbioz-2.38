using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights
{
    public class FightInstancePvM : FightPvM
    {
        public override bool ShowBlades
        {
            get
            {
                return false;
            }
        }
        public FightInstancePvM(MapRecord map, FightTeam blueTeam, FightTeam redTeam, MonsterRecord[] templates)
            : base(map, blueTeam, redTeam, 0, MonsterGroup.FromTemplates(map, templates))
        {

        }

    }
}
