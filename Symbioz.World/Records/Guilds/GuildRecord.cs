using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Core;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Guilds;
using Symbioz.Protocol.Types;
using Symbioz.World.Records.Characters;
using Symbioz.Core.DesignPattern.StartupEngine;

namespace Symbioz.World.Records.Guilds
{
    [Table("Guilds"), Resettable]
    public class GuildRecord : ITable
    {
        public static List<GuildRecord> Guilds = new List<GuildRecord>();

        [Primary]
        public int Id;

        public string Name;

        [Xml]
        public ContextGuildEmblem Emblem;

        [Update]
        public ulong Experience;

        public int MaxTaxCollectors;

        [Xml, Update]
        public List<ContextGuildMember> Members;

        [Xml, Update]
        public GuildMotd Motd;

        public GuildRecord(int id, string name, ContextGuildEmblem emblem, ulong experience,
            int maxTaxCollectors, List<ContextGuildMember> members, GuildMotd motd)
        {
            this.Id = id;
            this.Name = name;
            this.Emblem = emblem;
            this.Experience = experience;
            this.MaxTaxCollectors = maxTaxCollectors;
            this.Members = members;
            this.Motd = motd;
        }
        public static bool Exist(ContextGuildEmblem emblem)
        {
            return Guilds.FirstOrDefault(x => x.Emblem == emblem) != null;
        }
        public static bool Exist(string name)
        {
            return Guilds.FirstOrDefault(x => x.Name == name) != null;
        }
        public ContextGuildMember GetContextGuildMember(long id)
        {
            return Members.FirstOrDefault(x => x.CharacterId == id);
        }
        public static GuildRecord GetGuild(int id)
        {
            return Guilds.FirstOrDefault(x => x.Id == id);
        }
        public static GuildRecord New(string name, ContextGuildEmblem emblem, int maxTaxCollector)
        {
            return new GuildRecord(Guilds.DynamicPop(x => x.Id), name, emblem, 0, maxTaxCollector, new List<ContextGuildMember>(), new GuildMotd());
        }

        public GuildInformations GetGuildInformations()
        {
            return new GuildInformations((uint)Id, Name, (byte)ExperienceRecord.GetLevelFromGuildExperience(Experience), Emblem.ToGuildEmblem());
        }
        public static void RemoveWhereId(CharacterRecord record)
        {
            var guildRecord = GuildRecord.GetGuild(record.GuildId);

            if (guildRecord != null)
            {
                var member = guildRecord.GetContextGuildMember(record.Id);

                if (member != null)
                {
                    guildRecord.Members.Remove(member);
                    guildRecord.UpdateElement();
                }
            }
        }
    }
}
