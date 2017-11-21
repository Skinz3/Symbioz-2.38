using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Protocol.Enums;
using System.Threading.Tasks;
using Symbioz.World.Models.Entities;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Fighters;

namespace Symbioz.World.Models.Fights.FightModels
{
    public class FightTeamOptions
    {
        private FightTeam Team { get; set; }

        private bool IsSecret { get; set; }

        private bool RestrictedToParty { get; set; }

        private bool Closed { get; set; }

        private bool AskingForHelp { get; set; }

        public FightTeamOptions(FightTeam team)
        {
            this.Team = team;
            this.IsSecret = false;
            this.RestrictedToParty = false;
            this.Closed = false;
            this.AskingForHelp = false;
          
        }
        public FightOptionsInformations GetFightOptionsInformations()
        {
            return new FightOptionsInformations(IsSecret, RestrictedToParty, Closed, AskingForHelp);
        }
        public bool CanJoin(Character character)
        {
            if (Team.Type == TeamTypeEnum.TEAM_TYPE_MONSTER &&
               character.Client.Account.Role <= ServerRoleEnum.Administrator) // é_é anarchie?
            {
                return false;
            }
         
            if (Closed)
            {
                OnRefused(character, FighterRefusedReasonEnum.TEAM_LIMITED_BY_MAINCHARACTER);
                return false;
            }
            if (RestrictedToParty)
            {
                if (Team.LeaderAsParty)
                {
                    if (((CharacterFighter)Team.Leader).Character.Party != character.Party)
                    {
                        return false;
                    }
                }
            }
            if (!Team.IsPlacementCellsFree(character.FighterCount))
            {
                OnRefused(character, FighterRefusedReasonEnum.TEAM_FULL);
                return false;
            }

            if (Team.Fight.Map.Id != character.Map.Id)
            {
                OnRefused(character, FighterRefusedReasonEnum.FIGHTER_REFUSED);
                return false;
            }
            if (character.Busy)
            {
                OnRefused(character, FighterRefusedReasonEnum.IM_OCCUPIED);
                return false;
            }
            if (Team.Side != AlignmentSideEnum.ALIGNMENT_WITHOUT && Team.Side != character.Record.Alignment.Side)
            {
                OnRefused(character, FighterRefusedReasonEnum.WRONG_ALIGNMENT);
                return false;
            }
            if (Team.Fight.Started)
            {
                OnRefused(character, FighterRefusedReasonEnum.TOO_LATE);
                return false;
            }
            if (Team.Fight.GetFighters<CharacterFighter>().FirstOrDefault(x => x.Character.Id == character.Id) != null)
            {
                OnRefused(character, FighterRefusedReasonEnum.FIGHTER_REFUSED);
                return false;
            }
            return true;
        }
        private void OnRefused(Character character, FighterRefusedReasonEnum reason)
        {
            character.Client.Send(new ChallengeFightJoinRefusedMessage((ulong)character.Id, (sbyte)reason));
        }
        public void ToggleOption(FightOptionsEnum option)
        {
            bool state;
            switch (option)
            {
                case FightOptionsEnum.FIGHT_OPTION_SET_SECRET:
                    IsSecret = !IsSecret;
                    state = IsSecret;
                    break;
                case FightOptionsEnum.FIGHT_OPTION_SET_TO_PARTY_ONLY:
                    RestrictedToParty = !RestrictedToParty;
                    state = RestrictedToParty;
                    break;
                case FightOptionsEnum.FIGHT_OPTION_SET_CLOSED:
                    Closed = !Closed;
                    state = Closed;
                    break;
                case FightOptionsEnum.FIGHT_OPTION_ASK_FOR_HELP:
                    AskingForHelp = !AskingForHelp;
                    state = AskingForHelp;
                    break;
                default:
                    state = false;
                    break;
            }

            GameFightOptionStateUpdateMessage message = new GameFightOptionStateUpdateMessage((short)Team.Fight.Id, Team.Id, (sbyte)option, state);
            Team.Fight.Map.Instance.Send(message);
            Team.Send(message);
        }
    }
}
