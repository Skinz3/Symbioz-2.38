


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceBulletinSetRequestMessage : SocialNoticeSetRequestMessage
{

public const ushort Id = 6693;
public override ushort MessageId
{
    get { return Id; }
}

public string content;
        public bool notifyMembers;
        

public AllianceBulletinSetRequestMessage()
{
}

public AllianceBulletinSetRequestMessage(string content, bool notifyMembers)
        {
            this.content = content;
            this.notifyMembers = notifyMembers;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUTF(content);
            writer.WriteBoolean(notifyMembers);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            content = reader.ReadUTF();
            notifyMembers = reader.ReadBoolean();
            

}


}


}