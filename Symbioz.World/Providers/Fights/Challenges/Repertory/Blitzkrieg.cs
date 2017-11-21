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
    /// Dès qu'un adversaire est attaqué, il doit être achevé avant le début de son tour de jeu.
    /// </summary>
    [Challenge(ChallengeTypeEnum.Blitzkrieg)]
    public class Blitzkrieg : Challenge
    {
        public override int XpBonusPercent
        {
            get
            {
                return 120;
            }
        }

        public override int DropBonusPercent
        {
            get
            {
                return 120;
            }
        }
        public Blitzkrieg(ChallengeRecord template, FightTeam team)
            : base(template, team)
        {

        }
        public override void BindEvents()
        {
            foreach (var fighter in Team.OposedTeam().GetFighters(false))
            {
                fighter.OnTurnStartEvt += OnTurnStartEvt;
            }
        }

        void OnTurnStartEvt(Fighter obj)
        {
            if (obj.Stats.CurrentLifePoints != obj.Stats.CurrentMaxLifePoints)
            {
                OnChallengeResulted(ChallengeResultEnum.FAILED);
            }
        }

        public override void UnBindEvents()
        {
            foreach (var fighter in Team.OposedTeam().GetFighters(false))
            {
                fighter.OnTurnStartEvt -= OnTurnStartEvt;
            }
        }
    }
}
