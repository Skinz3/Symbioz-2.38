


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceCreationValidMessage : Message
{

public const ushort Id = 6393;
public override ushort MessageId
{
    get { return Id; }
}

public string allianceName;
        public string allianceTag;
        public Types.GuildEmblem allianceEmblem;
        

public AllianceCreationValidMessage()
{
}

public AllianceCreationValidMessage(string allianceName, string allianceTag, Types.GuildEmblem allianceEmblem)
        {
            this.allianceName = allianceName;
            this.allianceTag = allianceTag;
            this.allianceEmblem = allianceEmblem;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(allianceName);
            writer.WriteUTF(allianceTag);
            allianceEmblem.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

allianceName = reader.ReadUTF();
            allianceTag = reader.ReadUTF();
            allianceEmblem = new Types.GuildEmblem();
            allianceEmblem.Deserialize(reader);
            

}


}


}