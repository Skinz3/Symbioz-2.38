using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Enums
{
    public enum GuildCreationResultEnum
    {
        GUILD_CREATE_OK = 1,
        GUILD_CREATE_ERROR_NAME_INVALID,
        GUILD_CREATE_ERROR_ALREADY_IN_GUILD,
        GUILD_CREATE_ERROR_NAME_ALREADY_EXISTS,
        GUILD_CREATE_ERROR_EMBLEM_ALREADY_EXISTS,
        GUILD_CREATE_ERROR_LEAVE,
        GUILD_CREATE_ERROR_CANCEL,
        GUILD_CREATE_ERROR_REQUIREMENT_UNMET,
        GUILD_CREATE_ERROR_EMBLEM_INVALID
    }
}
