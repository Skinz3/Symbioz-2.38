


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DareListMessage : Message
{

public const ushort Id = 6661;
public override ushort MessageId
{
    get { return Id; }
}

public Types.DareInformations[] dares;
        

public DareListMessage()
{
}

public DareListMessage(Types.DareInformations[] dares)
        {
            this.dares = dares;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)dares.Length);
            foreach (var entry in dares)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            dares = new Types.DareInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 dares[i] = new Types.DareInformations();
                 dares[i].Deserialize(reader);
            }
            

}


}


}