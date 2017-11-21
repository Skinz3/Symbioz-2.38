using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Damages;
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
    /// Ne pas perdre de point de vie ou de point de bouclier pendant toute la durée du combat.
    /// </summary>
    [Challenge(ChallengeTypeEnum.Intouchable)]
    public class Untouchable : Challenge
    {
        public override int XpBonusPercent
        {
            get
            {
                return 100;
            }
        }

        public override int DropBonusPercent
        {
            get
            {
                return 100;
            }
        }
        public Untouchable(ChallengeRecord template, FightTeam team)
            : base(template, team)
        {

        }
        void OnDamageTaken(Fighter arg1, Damage arg2)
        {
            OnChallengeResulted(ChallengeResultEnum.FAILED);
        }
        public override void BindEvents()
        {
            foreach (var fighter in Team.GetFighters(false))
            {
                fighter.OnDamageTaken += OnDamageTaken;
            }
        }
        public override void UnBindEvents()
        {
            foreach (var fighter in Team.GetFighters(false))
            {
                fighter.OnDamageTaken -= OnDamageTaken;
            }
        }

    }
}
