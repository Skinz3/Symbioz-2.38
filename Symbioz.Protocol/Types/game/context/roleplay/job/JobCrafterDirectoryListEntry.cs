


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class JobCrafterDirectoryListEntry
{

public const short Id = 196;
public virtual short TypeId
{
    get { return Id; }
}

public JobCrafterDirectoryEntryPlayerInfo playerInfo;
        public JobCrafterDirectoryEntryJobInfo jobInfo;
        

public JobCrafterDirectoryListEntry()
{
}

public JobCrafterDirectoryListEntry(JobCrafterDirectoryEntryPlayerInfo playerInfo, JobCrafterDirectoryEntryJobInfo jobInfo)
        {
            this.playerInfo = playerInfo;
            this.jobInfo = jobInfo;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

playerInfo.Serialize(writer);
            jobInfo.Serialize(writer);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

playerInfo = new JobCrafterDirectoryEntryPlayerInfo();
            playerInfo.Deserialize(reader);
            jobInfo = new JobCrafterDirectoryEntryJobInfo();
            jobInfo.Deserialize(reader);
            

}


}


}