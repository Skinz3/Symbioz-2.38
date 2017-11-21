using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Records.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Challenges.Repertory
{
    [Challenge(ChallengeTypeEnum.Le_cheat_des_devs)]
    public class DevCheat : Challenge
    {
        public override int XpBonusPercent
        {
            get
            {
                return 1;
            }
        }

        public override int DropBonusPercent
        {
            get
            {
                return 1;
            }
        }

        // Nothing to do :D
        public DevCheat(ChallengeRecord template, FightTeam team)
            : base(template, team)
        {

        }

        public override void BindEvents()
        {

        }

        public override void UnBindEvents()
        {

        }
    }
}
