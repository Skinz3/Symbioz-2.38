


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PrismsListMessage : Message
{

public const ushort Id = 6440;
public override ushort MessageId
{
    get { return Id; }
}

public Types.PrismSubareaEmptyInfo[] prisms;
        

public PrismsListMessage()
{
}

public PrismsListMessage(Types.PrismSubareaEmptyInfo[] prisms)
        {
            this.prisms = prisms;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)prisms.Length);
            foreach (var entry in prisms)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            prisms = new Types.PrismSubareaEmptyInfo[limit];
            for (int i = 0; i < limit; i++)
            {
                 prisms[i] = Types.ProtocolTypeManager.GetInstance<Types.PrismSubareaEmptyInfo>(reader.ReadShort());
                 prisms[i].Deserialize(reader);
            }
            

}


}


}