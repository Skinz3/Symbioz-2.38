


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AchievementListMessage : Message
{

public const ushort Id = 6205;
public override ushort MessageId
{
    get { return Id; }
}

public ushort[] finishedAchievementsIds;
        public Types.AchievementRewardable[] rewardableAchievements;
        

public AchievementListMessage()
{
}

public AchievementListMessage(ushort[] finishedAchievementsIds, Types.AchievementRewardable[] rewardableAchievements)
        {
            this.finishedAchievementsIds = finishedAchievementsIds;
            this.rewardableAchievements = rewardableAchievements;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)finishedAchievementsIds.Length);
            foreach (var entry in finishedAchievementsIds)
            {
                 writer.WriteVarUhShort(entry);
            }
            writer.WriteUShort((ushort)rewardableAchievements.Length);
            foreach (var entry in rewardableAchievements)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            finishedAchievementsIds = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 finishedAchievementsIds[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            rewardableAchievements = new Types.AchievementRewardable[limit];
            for (int i = 0; i < limit; i++)
            {
                 rewardableAchievements[i] = new Types.AchievementRewardable();
                 rewardableAchievements[i].Deserialize(reader);
            }
            

}


}


}