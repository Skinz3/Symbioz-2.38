using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Arena
{
    public class ArenaMember
    {
        public Character Character
        {
            get;
            private set;
        }
        public ArenaGroup Group
        {
            get
            {
                return Team.Group;
            }
        }
        public ArenaMemberCollection Team
        {
            get;
            private set;
        }
        public PvpArenaStepEnum Step
        {
            get;
            private set;
        }
        public bool Accepted
        {
            get;
            private set;
        }
        public ArenaMember(Character character, ArenaMemberCollection team)
        {
            this.Character = character;
            this.Team = team;
            this.Accepted = false;
        }
        private void UpdateRegistrationStatus(bool registred)
        {
            Send(new GameRolePlayArenaRegistrationStatusMessage(registred, (sbyte)Step, (int)Group.Type));
        }
        public void UpdateStep(bool registred, PvpArenaStepEnum step)
        {
            this.Step = step;
            this.UpdateRegistrationStatus(registred);
        }
        public void Send(Message message)
        {
            Character.Client.Send(message);
        }
        public void Request()
        {
            Character.Client.Send(new GameRolePlayArenaFightPropositionMessage(0, Array.ConvertAll(Team.GetMemberIds(), x => (double)x), Group.RequestDuration));
        }

        public void RejoinMap()
        {
            this.Character.RejoinMap(FightTypeEnum.FIGHT_TYPE_PVP_ARENA, false, true);
        }

        private void UpdateFighterStatus()
        {
            Group.Send(new GameRolePlayArenaFighterStatusMessage(0, (int)Character.Id, Accepted));
        }
        public void Anwser(bool accept)
        {
            this.Accepted = accept;
            this.UpdateFighterStatus();

            if (Accepted)
            {
                if (Group.Accepted && Group.Ready)
                {
                    Group.StartFighting();
                }
            }
            else
            {
                Character.UnregisterArena();
                Team.ForEach(x => x.UpdateStep(true, PvpArenaStepEnum.ARENA_STEP_REGISTRED));
                Team.ForEach(x => x.Accepted = false);
            }
        }
    }
}
