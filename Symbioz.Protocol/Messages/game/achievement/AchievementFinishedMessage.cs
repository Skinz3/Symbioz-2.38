


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AchievementFinishedMessage : Message
{

public const ushort Id = 6208;
public override ushort MessageId
{
    get { return Id; }
}

public ushort id;
        public byte finishedlevel;
        

public AchievementFinishedMessage()
{
}

public AchievementFinishedMessage(ushort id, byte finishedlevel)
        {
            this.id = id;
            this.finishedlevel = finishedlevel;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(id);
            writer.WriteByte(finishedlevel);
            

}

public override void Deserialize(ICustomDataInput reader)
{

id = reader.ReadVarUhShort();
            if (id < 0)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
            finishedlevel = reader.ReadByte();
            if (finishedlevel < 0 || finishedlevel > 200)
                throw new Exception("Forbidden value on finishedlevel = " + finishedlevel + ", it doesn't respect the following condition : finishedlevel < 0 || finishedlevel > 200");
            

}


}


}