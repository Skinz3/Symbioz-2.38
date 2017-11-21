


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceJoinedMessage : Message
{

public const ushort Id = 6402;
public override ushort MessageId
{
    get { return Id; }
}

public Types.AllianceInformations allianceInfo;
        public bool enabled;
        

public AllianceJoinedMessage()
{
}

public AllianceJoinedMessage(Types.AllianceInformations allianceInfo, bool enabled)
        {
            this.allianceInfo = allianceInfo;
            this.enabled = enabled;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

allianceInfo.Serialize(writer);
            writer.WriteBoolean(enabled);
            

}

public override void Deserialize(ICustomDataInput reader)
{

allianceInfo = new Types.AllianceInformations();
            allianceInfo.Deserialize(reader);
            enabled = reader.ReadBoolean();
            

}


}


}