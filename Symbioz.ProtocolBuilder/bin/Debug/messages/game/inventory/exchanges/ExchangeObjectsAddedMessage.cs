


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeObjectsAddedMessage : ExchangeObjectMessage
{

public const ushort Id = 6535;
public override ushort MessageId
{
    get { return Id; }
}

public Types.ObjectItem[] @object;
        

public ExchangeObjectsAddedMessage()
{
}

public ExchangeObjectsAddedMessage(bool remote, Types.ObjectItem[] @object)
         : base(remote)
        {
            this.@object = @object;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)@object.Length);
            foreach (var entry in @object)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            @object = new Types.ObjectItem[limit];
            for (int i = 0; i < limit; i++)
            {
                 @object[i] = new Types.ObjectItem();
                 @object[i].Deserialize(reader);
            }
            

}


}


}