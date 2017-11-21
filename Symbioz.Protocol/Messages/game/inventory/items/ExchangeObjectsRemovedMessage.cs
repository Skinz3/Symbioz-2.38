


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeObjectsRemovedMessage : ExchangeObjectMessage
{

public const ushort Id = 6532;
public override ushort MessageId
{
    get { return Id; }
}

public uint[] objectUID;
        

public ExchangeObjectsRemovedMessage()
{
}

public ExchangeObjectsRemovedMessage(bool remote, uint[] objectUID)
         : base(remote)
        {
            this.objectUID = objectUID;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)objectUID.Length);
            foreach (var entry in objectUID)
            {
                 writer.WriteVarUhInt(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            objectUID = new uint[limit];
            for (int i = 0; i < limit; i++)
            {
                 objectUID[i] = reader.ReadVarUhInt();
            }
            

}


}


}