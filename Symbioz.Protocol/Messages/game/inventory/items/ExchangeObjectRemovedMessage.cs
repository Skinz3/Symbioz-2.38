


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeObjectRemovedMessage : ExchangeObjectMessage
{

public const ushort Id = 5517;
public override ushort MessageId
{
    get { return Id; }
}

public uint objectUID;
        

public ExchangeObjectRemovedMessage()
{
}

public ExchangeObjectRemovedMessage(bool remote, uint objectUID)
         : base(remote)
        {
            this.objectUID = objectUID;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(objectUID);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            objectUID = reader.ReadVarUhInt();
            if (objectUID < 0)
                throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
            

}


}


}