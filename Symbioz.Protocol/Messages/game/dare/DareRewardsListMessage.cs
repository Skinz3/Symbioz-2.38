


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DareRewardsListMessage : Message
{

public const ushort Id = 6677;
public override ushort MessageId
{
    get { return Id; }
}

public Types.DareReward[] rewards;
        

public DareRewardsListMessage()
{
}

public DareRewardsListMessage(Types.DareReward[] rewards)
        {
            this.rewards = rewards;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)rewards.Length);
            foreach (var entry in rewards)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            rewards = new Types.DareReward[limit];
            for (int i = 0; i < limit; i++)
            {
                 rewards[i] = new Types.DareReward();
                 rewards[i].Deserialize(reader);
            }
            

}


}


}