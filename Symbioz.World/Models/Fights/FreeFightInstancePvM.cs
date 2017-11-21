using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights
{
    public class FreeFightInstancePvM : FightInstancePvM
    {
        public override FightTypeEnum FightType
        {
            get
            {
                return FightTypeEnum.FIGHT_TYPE_MXvM;
            }
        }
        public FreeFightInstancePvM(MapRecord map, FightTeam blueTeam, FightTeam redTeam, MonsterRecord[] templates)
            : base(map, blueTeam, redTeam, templates)
        {

        }
        public override int GetPreparationDelay()
        {
            return 0;
        }
    }
}
