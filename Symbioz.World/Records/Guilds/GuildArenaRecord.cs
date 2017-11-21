using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;
using Symbioz.World.Models.Guilds;

namespace Symbioz.World.Records.Guilds
{
    [Table("guildsarena")]
    public class GuildArenaRecord : ITable
    {
        public static List<GuildArenaRecord> GuildsArena = new List<GuildArenaRecord>();

        [Primary]
        public int Id;

        public int FirstGuildId;

        public int SecondGuildId;

        public double FightDate;

        public GuildArenaRecord(int id,int firstGuildId,int secondGuildId,double fightDate)
        {
            this.Id = id;
            this.FirstGuildId = firstGuildId;
            this.SecondGuildId = secondGuildId;
            this.FightDate = fightDate;
        }

        public static GuildArenaRecord CreateGuildArena(int firstGuildId,int secondGuildId)
        {
            int newId = GuildsArena.DynamicPop(x => x.Id);

            return new GuildArenaRecord(newId, firstGuildId, secondGuildId,0);
        }
        public static bool Sorted()
        {
            return GuildsArena.Any(x => x.SecondGuildId != -1);
        }
        public static GuildArenaRecord GetGuildArena(int guildId)
        {
            return GuildsArena.Find(x => x.FirstGuildId == guildId);
        }

       
    }
}
