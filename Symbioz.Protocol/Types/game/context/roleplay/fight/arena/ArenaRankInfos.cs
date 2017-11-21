


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ArenaRankInfos
{

public const short Id = 499;
public virtual short TypeId
{
    get { return Id; }
}

public ushort rank;
        public ushort bestRank;
        public ushort victoryCount;
        public ushort fightcount;
        

public ArenaRankInfos()
{
}

public ArenaRankInfos(ushort rank, ushort bestRank, ushort victoryCount, ushort fightcount)
        {
            this.rank = rank;
            this.bestRank = bestRank;
            this.victoryCount = victoryCount;
            this.fightcount = fightcount;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(rank);
            writer.WriteVarUhShort(bestRank);
            writer.WriteVarUhShort(victoryCount);
            writer.WriteVarUhShort(fightcount);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

rank = reader.ReadVarUhShort();
            if (rank < 0 || rank > 2300)
                throw new Exception("Forbidden value on rank = " + rank + ", it doesn't respect the following condition : rank < 0 || rank > 2300");
            bestRank = reader.ReadVarUhShort();
            if (bestRank < 0 || bestRank > 2300)
                throw new Exception("Forbidden value on bestRank = " + bestRank + ", it doesn't respect the following condition : bestRank < 0 || bestRank > 2300");
            victoryCount = reader.ReadVarUhShort();
            if (victoryCount < 0)
                throw new Exception("Forbidden value on victoryCount = " + victoryCount + ", it doesn't respect the following condition : victoryCount < 0");
            fightcount = reader.ReadVarUhShort();
            if (fightcount < 0)
                throw new Exception("Forbidden value on fightcount = " + fightcount + ", it doesn't respect the following condition : fightcount < 0");
            

}


}


}