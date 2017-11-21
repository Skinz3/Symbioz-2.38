


















// Generated on 04/27/2016 01:13:09
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class AbstractCharacterInformation
{

public const short Id = 400;
public virtual short TypeId
{
    get { return Id; }
}

public ulong id;
        

public AbstractCharacterInformation()
{
}

public AbstractCharacterInformation(ulong id)
        {
            this.id = id;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(id);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

id = reader.ReadVarUhLong();
            if (id < 0 || id > 9007199254740990)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0 || id > 9007199254740990");
            

}


}


}