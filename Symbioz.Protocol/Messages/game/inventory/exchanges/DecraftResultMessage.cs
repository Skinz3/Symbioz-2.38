


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DecraftResultMessage : Message
{

public const ushort Id = 6569;
public override ushort MessageId
{
    get { return Id; }
}

public Types.DecraftedItemStackInfo[] results;
        

public DecraftResultMessage()
{
}

public DecraftResultMessage(Types.DecraftedItemStackInfo[] results)
        {
            this.results = results;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)results.Length);
            foreach (var entry in results)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            results = new Types.DecraftedItemStackInfo[limit];
            for (int i = 0; i < limit; i++)
            {
                 results[i] = new Types.DecraftedItemStackInfo();
                 results[i].Deserialize(reader);
            }
            

}


}


}