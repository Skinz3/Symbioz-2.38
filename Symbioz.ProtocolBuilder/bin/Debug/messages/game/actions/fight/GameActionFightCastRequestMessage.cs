


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameActionFightCastRequestMessage : Message
{

public const ushort Id = 1005;
public override ushort MessageId
{
    get { return Id; }
}

public ushort spellId;
        public short cellId;
        

public GameActionFightCastRequestMessage()
{
}

public GameActionFightCastRequestMessage(ushort spellId, short cellId)
        {
            this.spellId = spellId;
            this.cellId = cellId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(spellId);
            writer.WriteShort(cellId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

spellId = reader.ReadVarUhShort();
            if (spellId < 0)
                throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
            cellId = reader.ReadShort();
            if (cellId < -1 || cellId > 559)
                throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < -1 || cellId > 559");
            

}


}


}