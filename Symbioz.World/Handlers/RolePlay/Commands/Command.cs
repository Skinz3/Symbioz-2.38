using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Commands
{
    public class Command 
    {
        public Command(string value, ServerRoleEnum role)
        {
            this.Value = value;
            this.MinimumRoleRequired = role;
        }
        public string Value { get; set; }
        public ServerRoleEnum MinimumRoleRequired { get; set; }
    }
}
