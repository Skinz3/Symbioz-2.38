


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class MapRunningFightListMessage : Message
{

public const ushort Id = 5743;
public override ushort MessageId
{
    get { return Id; }
}

public Types.FightExternalInformations[] fights;
        

public MapRunningFightListMessage()
{
}

public MapRunningFightListMessage(Types.FightExternalInformations[] fights)
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
            fights = new Types.FightExternalInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 fights[i] = new Types.FightExternalInformations();
                 fights[i].Deserialize(reader);
            }
            

}


}


}