using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Commands
{
    public class ChatCommand : Attribute
    {
        public ServerRoleEnum Role { get; set; }

        public string Name { get; set; }

        public ChatCommand(string name, ServerRoleEnum role)
        {
            this.Name = name;
            this.Role = role;
        }
        public ChatCommand(string name)
        {
            this.Name = name;
            this.Role = ServerRoleEnum.Player;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
