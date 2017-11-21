using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Entities;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Arena
{
    public class ArenaProvider : Singleton<ArenaProvider>
    {
        public List<ArenaGroup> ArenaGroups
        {
            get;
            set;
        }
        public ArenaProvider()
        {
            ArenaGroups = new List<ArenaGroup>();
        }
        private ArenaGroup FindGroup(Character character)
        {
            if (character.Client.Account.Role >= ServerRoleEnum.Administrator)
            {
                return ArenaGroups.Find(x => !x.Ready);
            }
            else
            {
                return ArenaGroups.Find(x => !x.Ready && !x.ContainsIp(character.Client.Ip) && x.CanChallenge(character));
            }
        }
        public ArenaMember Register(Character character)
        {
            ArenaGroup group = FindGroup(character);

            if (group != null)
            {
                ArenaMember member = group.AddCharacter(character);

                if (group.Ready)
                {
                    group.Request();

                }
                return member;
            }
            else
            {
                ArenaGroup newGroup = new ArenaGroup();
                ArenaMember member = newGroup.AddCharacter(character);
                ArenaGroups.Add(newGroup);
                return member;
            }


        }
        public void Unregister(Character character)
        {
            character.ArenaMember.Team.RemoveMember(character);

            if (character.ArenaMember.Group.Empty)
            {
                this.ArenaGroups.Remove(character.ArenaMember.Group);
            }


        }
    }
}
