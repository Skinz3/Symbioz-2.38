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
    /// N'utiliser que des sorts pendant toute la durée du combat.
    /// </summary>
    [Challenge(ChallengeTypeEnum.Mystique)]
    public class Mystic : Challenge
    {
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
        public Mystic(ChallengeRecord template, FightTeam team)
            : base(template, team)
        {

        }
        public override void BindEvents()
        {
            foreach (CharacterFighter fighter in Team.GetFighters<CharacterFighter>(false))
            {
                fighter.OnWeaponUsedEvt += OnWeaponUsed;
            }
        }

        void OnWeaponUsed(Fighter obj)
        {
            OnChallengeResulted(ChallengeResultEnum.FAILED);
        }

        public override void UnBindEvents()
        {
            foreach (CharacterFighter fighter in Team.GetFighters<CharacterFighter>(false))
            {
                fighter.OnWeaponUsedEvt -= OnWeaponUsed;
            }
        }
    }
}
