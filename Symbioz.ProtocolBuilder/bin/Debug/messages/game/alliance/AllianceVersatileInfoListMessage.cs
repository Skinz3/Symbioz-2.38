


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceVersatileInfoListMessage : Message
{

public const ushort Id = 6436;
public override ushort MessageId
{
    get { return Id; }
}

public Types.AllianceVersatileInformations[] alliances;
        

public AllianceVersatileInfoListMessage()
{
}

public AllianceVersatileInfoListMessage(Types.AllianceVersatileInformations[] alliances)
        {
            this.alliances = alliances;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)alliances.Length);
            foreach (var entry in alliances)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            alliances = new Types.AllianceVersatileInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 alliances[i] = new Types.AllianceVersatileInformations();
                 alliances[i].Deserialize(reader);
            }
            

}


}


}