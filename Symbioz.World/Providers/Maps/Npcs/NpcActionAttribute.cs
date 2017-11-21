using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Maps.Npcs
{
    public class NpcActionAttribute : Attribute
    {
        public NpcActionTypeEnum ActionType;

        public NpcActionAttribute(NpcActionTypeEnum actionType)
        {
            this.ActionType = actionType;
        }
    }
}
