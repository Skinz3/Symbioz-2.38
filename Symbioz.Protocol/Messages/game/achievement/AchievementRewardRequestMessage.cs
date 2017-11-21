


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AchievementRewardRequestMessage : Message
{

public const ushort Id = 6377;
public override ushort MessageId
{
    get { return Id; }
}

public short achievementId;
        

public AchievementRewardRequestMessage()
{
}

public AchievementRewardRequestMessage(short achievementId)
        {
            this.achievementId = achievementId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteShort(achievementId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

achievementId = reader.ReadShort();
            

}


}


}