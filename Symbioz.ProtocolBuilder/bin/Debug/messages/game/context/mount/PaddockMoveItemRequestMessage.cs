


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PaddockMoveItemRequestMessage : Message
{

public const ushort Id = 6052;
public override ushort MessageId
{
    get { return Id; }
}

public ushort oldCellId;
        public ushort newCellId;
        

public PaddockMoveItemRequestMessage()
{
}

public PaddockMoveItemRequestMessage(ushort oldCellId, ushort newCellId)
        {
            this.oldCellId = oldCellId;
            this.newCellId = newCellId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(oldCellId);
            writer.WriteVarUhShort(newCellId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

oldCellId = reader.ReadVarUhShort();
            if (oldCellId < 0 || oldCellId > 559)
                throw new Exception("Forbidden value on oldCellId = " + oldCellId + ", it doesn't respect the following condition : oldCellId < 0 || oldCellId > 559");
            newCellId = reader.ReadVarUhShort();
            if (newCellId < 0 || newCellId > 559)
                throw new Exception("Forbidden value on newCellId = " + newCellId + ", it doesn't respect the following condition : newCellId < 0 || newCellId > 559");
            

}


}


}