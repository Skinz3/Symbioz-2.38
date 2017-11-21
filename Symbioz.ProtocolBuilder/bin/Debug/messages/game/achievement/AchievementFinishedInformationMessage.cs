


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AchievementFinishedInformationMessage : AchievementFinishedMessage
{

public const ushort Id = 6381;
public override ushort MessageId
{
    get { return Id; }
}

public string name;
        public ulong playerId;
        

public AchievementFinishedInformationMessage()
{
}

public AchievementFinishedInformationMessage(ushort id, byte finishedlevel, string name, ulong playerId)
         : base(id, finishedlevel)
        {
            this.name = name;
            this.playerId = playerId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUTF(name);
            writer.WriteVarUhLong(playerId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            name = reader.ReadUTF();
            playerId = reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740990)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0 || playerId > 9007199254740990");
            

}


}


}