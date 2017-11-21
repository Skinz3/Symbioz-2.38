using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights;
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
    /// Utiliser exactement un point de mouvement par tour de jeu.
    /// </summary>
    [Challenge(ChallengeTypeEnum.Zombie)]
    public class Zombie : Challenge
    {
        public const short MP_USED_PER_TURN_REQUIRED = 1;

        public Zombie(ChallengeRecord template, FightTeam team)
            : base(template, team)
        {

        }
        public override int XpBonusPercent
        {
            get
            {
                return 70;
            }

        }

        public override int DropBonusPercent
        {
            get
            {
                return 70;
            }
        }


        public override void BindEvents()
        {
            foreach (var fighter in base.Team.GetFighters(false))
            {
                fighter.OnMpUsedEvt += OnMpUsed;
                fighter.OnTurnEndEvt += OnTurnEnded;
            }
        }
        public override void UnBindEvents()
        {
            foreach (var fighter in base.Team.GetFighters(false))
            {
                fighter.OnMpUsedEvt -= OnMpUsed;
                fighter.OnTurnEndEvt -= OnTurnEnded;
            }
        }
        void OnTurnEnded(Fighter obj)
        {
            if (obj.Stats.MpUsed != MP_USED_PER_TURN_REQUIRED)
            {
                OnChallengeResulted(ChallengeResultEnum.FAILED);
            }
        }

        void OnMpUsed(Fighter arg1, short arg2)
        {
            if (arg1.Stats.MpUsed > MP_USED_PER_TURN_REQUIRED)
            {
                OnChallengeResulted(ChallengeResultEnum.FAILED);
            }
        }
    }
}

