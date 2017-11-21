


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class UpdateSelfAgressableStatusMessage : Message
{

public const ushort Id = 6456;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte status;
        public int probationTime;
        

public UpdateSelfAgressableStatusMessage()
{
}

public UpdateSelfAgressableStatusMessage(sbyte status, int probationTime)
        {
            this.status = status;
            this.probationTime = probationTime;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(status);
            writer.WriteInt(probationTime);
            

}

public override void Deserialize(ICustomDataInput reader)
{

status = reader.ReadSByte();
            if (status < 0)
                throw new Exception("Forbidden value on status = " + status + ", it doesn't respect the following condition : status < 0");
            probationTime = reader.ReadInt();
            if (probationTime < 0)
                throw new Exception("Forbidden value on probationTime = " + probationTime + ", it doesn't respect the following condition : probationTime < 0");
            

}


}


}