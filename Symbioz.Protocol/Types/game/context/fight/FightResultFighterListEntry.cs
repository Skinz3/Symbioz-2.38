


















// Generated on 04/27/2016 01:13:11
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class FightResultFighterListEntry : FightResultListEntry
{

public const short Id = 189;
public override short TypeId
{
    get { return Id; }
}

public double id;
        public bool alive;
        

public FightResultFighterListEntry()
{
}

public FightResultFighterListEntry(ushort outcome, sbyte wave, FightLoot rewards, double id, bool alive)
         : base(outcome, wave, rewards)
        {
            this.id = id;
            this.alive = alive;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteDouble(id);
            writer.WriteBoolean(alive);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            id = reader.ReadDouble();
            if (id < -9007199254740990 || id > 9007199254740990)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < -9007199254740990 || id > 9007199254740990");
            alive = reader.ReadBoolean();
            

}


}


}