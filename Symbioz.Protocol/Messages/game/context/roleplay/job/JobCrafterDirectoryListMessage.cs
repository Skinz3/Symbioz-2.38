


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class JobCrafterDirectoryListMessage : Message
{

public const ushort Id = 6046;
public override ushort MessageId
{
    get { return Id; }
}

public Types.JobCrafterDirectoryListEntry[] listEntries;
        

public JobCrafterDirectoryListMessage()
{
}

public JobCrafterDirectoryListMessage(Types.JobCrafterDirectoryListEntry[] listEntries)
        {
            this.listEntries = listEntries;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)listEntries.Length);
            foreach (var entry in listEntries)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            listEntries = new Types.JobCrafterDirectoryListEntry[limit];
            for (int i = 0; i < limit; i++)
            {
                 listEntries[i] = new Types.JobCrafterDirectoryListEntry();
                 listEntries[i].Deserialize(reader);
            }
            

}


}


}