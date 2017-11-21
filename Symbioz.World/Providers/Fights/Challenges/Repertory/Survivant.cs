using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Records.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Challenges.Repertory
{
    /// <summary>
    /// Aucun allié ne doit mourir.
    /// </summary>
    [Challenge(ChallengeTypeEnum.Survivant)]
    public class Survivant : Challenge
    {
        public override int XpBonusPercent
        {
            get
            {
                return 40;
            }
        }

        public override int DropBonusPercent
        {
            get
            {
                return 40;
            }
        }
        public Survivant(ChallengeRecord template, FightTeam team)
            : base(template, team)
        {

        }
        void OnDead(Fighter obj)
        {
            this.OnChallengeResulted(ChallengeResultEnum.FAILED);
        }

        public override void UnBindEvents()
        {
            foreach (var fighter in Team.GetFighters(false))
            {
                fighter.BeforeDeadEvt -= OnDead;
            }
        }
        public override bool Valid()
        {
            return Team.GetFightersCount(false) > 1;
        }
        public override void BindEvents()
        {
            foreach (var fighter in Team.GetFighters(false))
            {
                fighter.BeforeDeadEvt += OnDead;
            }
        }
    }
}
