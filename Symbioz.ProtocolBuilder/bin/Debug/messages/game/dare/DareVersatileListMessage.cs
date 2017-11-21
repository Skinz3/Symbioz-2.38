


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DareVersatileListMessage : Message
{

public const ushort Id = 6657;
public override ushort MessageId
{
    get { return Id; }
}

public Types.DareVersatileInformations[] dares;
        

public DareVersatileListMessage()
{
}

public DareVersatileListMessage(Types.DareVersatileInformations[] dares)
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
            dares = new Types.DareVersatileInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 dares[i] = new Types.DareVersatileInformations();
                 dares[i].Deserialize(reader);
            }
            

}


}


}