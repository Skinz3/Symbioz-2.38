


















// Generated on 04/27/2016 01:13:16
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ObjectEffect
{

public const short Id = 76;
public virtual short TypeId
{
    get { return Id; }
}

public ushort actionId;
        

public ObjectEffect()
{
}

public ObjectEffect(ushort actionId)
        {
            this.actionId = actionId;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(actionId);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

actionId = reader.ReadVarUhShort();
            if (actionId < 0)
                throw new Exception("Forbidden value on actionId = " + actionId + ", it doesn't respect the following condition : actionId < 0");
            

}


}


}