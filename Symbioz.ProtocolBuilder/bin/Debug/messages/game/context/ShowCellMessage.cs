


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ShowCellMessage : Message
{

public const ushort Id = 5612;
public override ushort MessageId
{
    get { return Id; }
}

public double sourceId;
        public ushort cellId;
        

public ShowCellMessage()
{
}

public ShowCellMessage(double sourceId, ushort cellId)
        {
            this.sourceId = sourceId;
            this.cellId = cellId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(sourceId);
            writer.WriteVarUhShort(cellId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

sourceId = reader.ReadDouble();
            if (sourceId < -9007199254740990 || sourceId > 9007199254740990)
                throw new Exception("Forbidden value on sourceId = " + sourceId + ", it doesn't respect the following condition : sourceId < -9007199254740990 || sourceId > 9007199254740990");
            cellId = reader.ReadVarUhShort();
            if (cellId < 0 || cellId > 559)
                throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < 0 || cellId > 559");
            

}


}


}