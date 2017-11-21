


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class NpcGenericActionRequestMessage : Message
{

public const ushort Id = 5898;
public override ushort MessageId
{
    get { return Id; }
}

public int npcId;
        public sbyte npcActionId;
        public int npcMapId;
        

public NpcGenericActionRequestMessage()
{
}

public NpcGenericActionRequestMessage(int npcId, sbyte npcActionId, int npcMapId)
        {
            this.npcId = npcId;
            this.npcActionId = npcActionId;
            this.npcMapId = npcMapId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(npcId);
            writer.WriteSByte(npcActionId);
            writer.WriteInt(npcMapId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

npcId = reader.ReadInt();
            npcActionId = reader.ReadSByte();
            if (npcActionId < 0)
                throw new Exception("Forbidden value on npcActionId = " + npcActionId + ", it doesn't respect the following condition : npcActionId < 0");
            npcMapId = reader.ReadInt();
            

}


}


}