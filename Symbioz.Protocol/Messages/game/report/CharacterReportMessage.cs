


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class CharacterReportMessage : Message
{

public const ushort Id = 6079;
public override ushort MessageId
{
    get { return Id; }
}

public ulong reportedId;
        public sbyte reason;
        

public CharacterReportMessage()
{
}

public CharacterReportMessage(ulong reportedId, sbyte reason)
        {
            this.reportedId = reportedId;
            this.reason = reason;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(reportedId);
            writer.WriteSByte(reason);
            

}

public override void Deserialize(ICustomDataInput reader)
{

reportedId = reader.ReadVarUhLong();
            if (reportedId < 0 || reportedId > 9007199254740990)
                throw new Exception("Forbidden value on reportedId = " + reportedId + ", it doesn't respect the following condition : reportedId < 0 || reportedId > 9007199254740990");
            reason = reader.ReadSByte();
            if (reason < 0)
                throw new Exception("Forbidden value on reason = " + reason + ", it doesn't respect the following condition : reason < 0");
            

}


}


}