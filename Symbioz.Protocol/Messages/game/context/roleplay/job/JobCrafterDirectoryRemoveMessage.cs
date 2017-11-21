


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class JobCrafterDirectoryRemoveMessage : Message
{

public const ushort Id = 5653;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte jobId;
        public ulong playerId;
        

public JobCrafterDirectoryRemoveMessage()
{
}

public JobCrafterDirectoryRemoveMessage(sbyte jobId, ulong playerId)
        {
            this.jobId = jobId;
            this.playerId = playerId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(jobId);
            writer.WriteVarUhLong(playerId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

jobId = reader.ReadSByte();
            if (jobId < 0)
                throw new Exception("Forbidden value on jobId = " + jobId + ", it doesn't respect the following condition : jobId < 0");
            playerId = reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740990)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0 || playerId > 9007199254740990");
            

}


}


}