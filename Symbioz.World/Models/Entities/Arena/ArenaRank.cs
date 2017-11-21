using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Entities.Arena
{
    public class ArenaRank
    {
        public ushort Rank
        {
            get;
            set;
        }
        public ushort BestRank
        {
            get;
            set;
        }
        public ushort VictoryCount
        {
            get;
            set;
        }
        public ushort FightCount
        {
            get;
            set;
        }
        public ArenaRankInfos GetArenaRankInfos()
        {
            return new ArenaRankInfos(Rank, BestRank, VictoryCount, FightCount);
        }

        public static ArenaRank New()
        {
            return new ArenaRank()
            {
                BestRank = 0,
                FightCount = 0,
                Rank = 0,
                VictoryCount = 0,
            };
        }
    }
}
