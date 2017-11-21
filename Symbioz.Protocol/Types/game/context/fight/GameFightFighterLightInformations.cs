


















// Generated on 04/27/2016 01:13:12
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameFightFighterLightInformations
{

public const short Id = 413;
public virtual short TypeId
{
    get { return Id; }
}

public double id;
        public sbyte wave;
        public ushort level;
        public sbyte breed;
        

public GameFightFighterLightInformations()
{
}

public GameFightFighterLightInformations(double id, sbyte wave, ushort level, sbyte breed)
        {
            this.id = id;
            this.wave = wave;
            this.level = level;
            this.breed = breed;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(id);
            writer.WriteSByte(wave);
            writer.WriteVarUhShort(level);
            writer.WriteSByte(breed);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

id = reader.ReadDouble();
            if (id < -9007199254740990 || id > 9007199254740990)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < -9007199254740990 || id > 9007199254740990");
            wave = reader.ReadSByte();
            if (wave < 0)
                throw new Exception("Forbidden value on wave = " + wave + ", it doesn't respect the following condition : wave < 0");
            level = reader.ReadVarUhShort();
            if (level < 0)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 0");
            breed = reader.ReadSByte();
            

}


}


}