


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class HavenBagFurnituresMessage : Message
{

public const ushort Id = 6634;
public override ushort MessageId
{
    get { return Id; }
}

public Types.HavenBagFurnitureInformation[] furnituresInfos;
        

public HavenBagFurnituresMessage()
{
}

public HavenBagFurnituresMessage(Types.HavenBagFurnitureInformation[] furnituresInfos)
        {
            this.furnituresInfos = furnituresInfos;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)furnituresInfos.Length);
            foreach (var entry in furnituresInfos)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            furnituresInfos = new Types.HavenBagFurnitureInformation[limit];
            for (int i = 0; i < limit; i++)
            {
                 furnituresInfos[i] = new Types.HavenBagFurnitureInformation();
                 furnituresInfos[i].Deserialize(reader);
            }
            

}


}


}