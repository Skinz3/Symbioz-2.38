


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceInvitedMessage : Message
{

public const ushort Id = 6397;
public override ushort MessageId
{
    get { return Id; }
}

public ulong recruterId;
        public string recruterName;
        public Types.BasicNamedAllianceInformations allianceInfo;
        

public AllianceInvitedMessage()
{
}

public AllianceInvitedMessage(ulong recruterId, string recruterName, Types.BasicNamedAllianceInformations allianceInfo)
        {
            this.recruterId = recruterId;
            this.recruterName = recruterName;
            this.allianceInfo = allianceInfo;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(recruterId);
            writer.WriteUTF(recruterName);
            allianceInfo.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

recruterId = reader.ReadVarUhLong();
            if (recruterId < 0 || recruterId > 9007199254740990)
                throw new Exception("Forbidden value on recruterId = " + recruterId + ", it doesn't respect the following condition : recruterId < 0 || recruterId > 9007199254740990");
            recruterName = reader.ReadUTF();
            allianceInfo = new Types.BasicNamedAllianceInformations();
            allianceInfo.Deserialize(reader);
            

}


}


}