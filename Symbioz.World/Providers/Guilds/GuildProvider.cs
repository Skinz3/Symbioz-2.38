using Symbioz.Core.DesignPattern;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records.Guilds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Protocol.Enums;
using System.Threading.Tasks;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Guilds;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Core.DesignPattern.StartupEngine;

namespace Symbioz.World.Providers.Guilds
{
    public class GuildProvider : Singleton<GuildProvider>
    {
        public const int MAX_MEMBERS_COUNT = 100;

        public const int DEFAULT_MAX_TAX_COLLECTOR = 1;

        public static int MAX_XP = 300000;

        public const int MOTD_MAX_LENGHT = 256;

        public static readonly double[][] XP_PER_GAP = new double[][]
        {
            new double[]
            {
                0.0,
                10.0
            },
            new double[]
            {
                10.0,
                8.0
            },
            new double[]
            {
                20.0,
                6.0
            },
            new double[]
            {
                30.0,
                4.0
            },
            new double[]
            {
                40.0,
                3.0
            },
            new double[]
            {
                50.0,
                2.0
            },
            new double[]
            {
                60.0,
                1.5
            },
            new double[]
            {
                70.0,
                1.0
            }
        };

        private List<GuildInstance> Guilds = new List<GuildInstance>();

        [StartupInvoke("Guilds Instance", StartupInvokePriority.Eighth)]
        public void Initialize()
        {
            foreach (var record in GuildRecord.Guilds)
            {
                Guilds.Add(new GuildInstance(record));
            }
        }
        public GuildCreationResultEnum CreateGuild(Character owner, string name, GuildEmblem emblem)
        {
            ContextGuildEmblem contextEmblem = ContextGuildEmblem.New(emblem);

            if (name.Contains('\'') ||  GuildRecord.Exist(name))
            {
                return GuildCreationResultEnum.GUILD_CREATE_ERROR_NAME_ALREADY_EXISTS;
            }
            if (GuildRecord.Exist(contextEmblem))
            {
                return GuildCreationResultEnum.GUILD_CREATE_ERROR_EMBLEM_ALREADY_EXISTS;
            }

            GuildRecord record = GuildRecord.New(name, contextEmblem, DEFAULT_MAX_TAX_COLLECTOR);
            record.AddElement();
            GuildInstance instance = new GuildInstance(record);
            instance.Join(owner, true);
            Guilds.Add(instance);
            return GuildCreationResultEnum.GUILD_CREATE_OK;
        }
        public GuildInstance[] GetGuilds()
        {
            return Guilds.ToArray();
        }
        public GuildInstance GetGuild(int guildId)
        {
            return Guilds.FirstOrDefault(x => x.Id == guildId);
        }

        public void RemoveGuild(GuildInstance guildInstance)
        {
            Guilds.Remove(guildInstance);
            guildInstance.Record.RemoveElement();
        }
    }
}
