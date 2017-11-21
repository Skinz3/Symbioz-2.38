


















// Generated on 04/27/2016 01:13:11
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class FightResultMutantListEntry : FightResultFighterListEntry
{

public const short Id = 216;
public override short TypeId
{
    get { return Id; }
}

public ushort level;
        

public FightResultMutantListEntry()
{
}

public FightResultMutantListEntry(ushort outcome, sbyte wave, FightLoot rewards, double id, bool alive, ushort level)
         : base(outcome, wave, rewards, id, alive)
        {
            this.level = level;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhShort(level);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            level = reader.ReadVarUhShort();
            if (level < 0)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 0");
            

}


}


}