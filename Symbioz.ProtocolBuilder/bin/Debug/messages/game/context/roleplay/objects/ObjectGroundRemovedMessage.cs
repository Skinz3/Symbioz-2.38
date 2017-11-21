


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ObjectGroundRemovedMessage : Message
{

public const ushort Id = 3014;
public override ushort MessageId
{
    get { return Id; }
}

public ushort cell;
        

public ObjectGroundRemovedMessage()
{
}

public ObjectGroundRemovedMessage(ushort cell)
        {
            this.cell = cell;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(cell);
            

}

public override void Deserialize(ICustomDataInput reader)
{

cell = reader.ReadVarUhShort();
            if (cell < 0 || cell > 559)
                throw new Exception("Forbidden value on cell = " + cell + ", it doesn't respect the following condition : cell < 0 || cell > 559");
            

}


}


}