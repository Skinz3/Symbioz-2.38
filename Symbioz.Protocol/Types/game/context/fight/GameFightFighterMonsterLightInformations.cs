


















// Generated on 04/27/2016 01:13:12
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameFightFighterMonsterLightInformations : GameFightFighterLightInformations
{

public const short Id = 455;
public override short TypeId
{
    get { return Id; }
}

public ushort creatureGenericId;
        

public GameFightFighterMonsterLightInformations()
{
}

public GameFightFighterMonsterLightInformations(double id, sbyte wave, ushort level, sbyte breed, ushort creatureGenericId)
         : base(id, wave, level, breed)
        {
            this.creatureGenericId = creatureGenericId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhShort(creatureGenericId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            creatureGenericId = reader.ReadVarUhShort();
            if (creatureGenericId < 0)
                throw new Exception("Forbidden value on creatureGenericId = " + creatureGenericId + ", it doesn't respect the following condition : creatureGenericId < 0");
            

}


}


}