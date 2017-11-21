


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRefreshMonsterBoostsMessage : Message
{

public const ushort Id = 6618;
public override ushort MessageId
{
    get { return Id; }
}

public Types.MonsterBoosts[] monsterBoosts;
        public Types.MonsterBoosts[] familyBoosts;
        

public GameRefreshMonsterBoostsMessage()
{
}

public GameRefreshMonsterBoostsMessage(Types.MonsterBoosts[] monsterBoosts, Types.MonsterBoosts[] familyBoosts)
        {
            this.monsterBoosts = monsterBoosts;
            this.familyBoosts = familyBoosts;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)monsterBoosts.Length);
            foreach (var entry in monsterBoosts)
            {
                 entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)familyBoosts.Length);
            foreach (var entry in familyBoosts)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            monsterBoosts = new Types.MonsterBoosts[limit];
            for (int i = 0; i < limit; i++)
            {
                 monsterBoosts[i] = new Types.MonsterBoosts();
                 monsterBoosts[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            familyBoosts = new Types.MonsterBoosts[limit];
            for (int i = 0; i < limit; i++)
            {
                 familyBoosts[i] = new Types.MonsterBoosts();
                 familyBoosts[i].Deserialize(reader);
            }
            

}


}


}