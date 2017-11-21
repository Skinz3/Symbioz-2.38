


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class InteractiveUsedMessage : Message
{

public const ushort Id = 5745;
public override ushort MessageId
{
    get { return Id; }
}

public ulong entityId;
        public uint elemId;
        public ushort skillId;
        public ushort duration;
        public bool canMove;
        

public InteractiveUsedMessage()
{
}

public InteractiveUsedMessage(ulong entityId, uint elemId, ushort skillId, ushort duration, bool canMove)
        {
            this.entityId = entityId;
            this.elemId = elemId;
            this.skillId = skillId;
            this.duration = duration;
            this.canMove = canMove;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(entityId);
            writer.WriteVarUhInt(elemId);
            writer.WriteVarUhShort(skillId);
            writer.WriteVarUhShort(duration);
            writer.WriteBoolean(canMove);
            

}

public override void Deserialize(ICustomDataInput reader)
{

entityId = reader.ReadVarUhLong();
            if (entityId < 0 || entityId > 9007199254740990)
                throw new Exception("Forbidden value on entityId = " + entityId + ", it doesn't respect the following condition : entityId < 0 || entityId > 9007199254740990");
            elemId = reader.ReadVarUhInt();
            if (elemId < 0)
                throw new Exception("Forbidden value on elemId = " + elemId + ", it doesn't respect the following condition : elemId < 0");
            skillId = reader.ReadVarUhShort();
            if (skillId < 0)
                throw new Exception("Forbidden value on skillId = " + skillId + ", it doesn't respect the following condition : skillId < 0");
            duration = reader.ReadVarUhShort();
            if (duration < 0)
                throw new Exception("Forbidden value on duration = " + duration + ", it doesn't respect the following condition : duration < 0");
            canMove = reader.ReadBoolean();
            

}


}


}