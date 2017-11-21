using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Guilds;

namespace Symbioz.World.Models.Entities.HumanOptions
{
    public class CharacterHumanOptionGuild : CharacterHumanOption
    {
        public uint GuildId
        {
            get;
            set;
        }
        public byte GuildLevel // problème, jamais update
        {
            get;
            set;
        }
        public string GuildName
        {
            get;
            set;
        }
        public ContextGuildEmblem Emblem
        {
            get;
            set;
        }
        public CharacterHumanOptionGuild()
        {

        }
        public CharacterHumanOptionGuild(GuildInformations informations)
        {
            this.GuildId = informations.guildId;
            this.GuildLevel = informations.guildLevel;
            this.GuildName = informations.guildName;
            this.Emblem = ContextGuildEmblem.New(informations.guildEmblem);
        }
        public override HumanOption GetHumanOption()
        {
            return new HumanOptionGuild(new GuildInformations(GuildId, GuildName, GuildLevel, Emblem.ToGuildEmblem()));
        }
    }
}
