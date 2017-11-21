


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PrismsInfoValidMessage : Message
{

public const ushort Id = 6451;
public override ushort MessageId
{
    get { return Id; }
}

public Types.PrismFightersInformation[] fights;
        

public PrismsInfoValidMessage()
{
}

public PrismsInfoValidMessage(Types.PrismFightersInformation[] fights)
        {
            this.fights = fights;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)fights.Length);
            foreach (var entry in fights)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            fights = new Types.PrismFightersInformation[limit];
            for (int i = 0; i < limit; i++)
            {
                 fights[i] = new Types.PrismFightersInformation();
                 fights[i].Deserialize(reader);
            }
            

}


}


}