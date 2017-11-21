


















// Generated on 04/27/2016 01:13:10
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ActorOrientation
{

public const short Id = 353;
public virtual short TypeId
{
    get { return Id; }
}

public double id;
        public sbyte direction;
        

public ActorOrientation()
{
}

public ActorOrientation(double id, sbyte direction)
        {
            this.id = id;
            this.direction = direction;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(id);
            writer.WriteSByte(direction);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

id = reader.ReadDouble();
            if (id < -9007199254740990 || id > 9007199254740990)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < -9007199254740990 || id > 9007199254740990");
            direction = reader.ReadSByte();
            if (direction < 0)
                throw new Exception("Forbidden value on direction = " + direction + ", it doesn't respect the following condition : direction < 0");
            

}


}


}