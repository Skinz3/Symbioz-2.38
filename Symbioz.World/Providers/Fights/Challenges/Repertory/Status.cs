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
    /// Finir son tour sur la même case que celle où vous l'avez commencé, pendant toute la durée du combat.
    /// </summary>
    [Challenge(ChallengeTypeEnum.Statue)]
    public class Status : Challenge
    {
        public override int XpBonusPercent
        {
            get
            {
                return 50;
            }
        }

        public override int DropBonusPercent
        {
            get
            {
                return 50;
            }
        }
        public Status(ChallengeRecord template, FightTeam team)
            : base(template, team)
        {

        }
  


        void OnTurnEnded(Fighter obj)
        {
            if (obj.CellId != obj.TurnStartCell)
            {
                OnChallengeResulted(ChallengeResultEnum.FAILED);
            }
        }

        public override void BindEvents()
        {
            foreach (var fighter in Team.GetFighters(false))
            {
                fighter.OnTurnEndEvt += OnTurnEnded;
            }
        }
        public override void UnBindEvents()
        {
            foreach (var fighter in Team.GetFighters(false))
            {
                fighter.OnTurnEndEvt -= OnTurnEnded;
            }
        }
    }
}
