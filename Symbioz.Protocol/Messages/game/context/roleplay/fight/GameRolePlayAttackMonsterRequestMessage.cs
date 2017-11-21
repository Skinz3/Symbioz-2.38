


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlayAttackMonsterRequestMessage : Message
{

public const ushort Id = 6191;
public override ushort MessageId
{
    get { return Id; }
}

public double monsterGroupId;
        

public GameRolePlayAttackMonsterRequestMessage()
{
}

public GameRolePlayAttackMonsterRequestMessage(double monsterGroupId)
        {
            this.monsterGroupId = monsterGroupId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(monsterGroupId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

monsterGroupId = reader.ReadDouble();
            if (monsterGroupId < -9007199254740990 || monsterGroupId > 9007199254740990)
                throw new Exception("Forbidden value on monsterGroupId = " + monsterGroupId + ", it doesn't respect the following condition : monsterGroupId < -9007199254740990 || monsterGroupId > 9007199254740990");
            

}


}


}