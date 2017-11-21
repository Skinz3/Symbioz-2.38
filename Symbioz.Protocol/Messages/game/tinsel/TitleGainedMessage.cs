


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class TitleGainedMessage : Message
{

public const ushort Id = 6364;
public override ushort MessageId
{
    get { return Id; }
}

public ushort titleId;
        

public TitleGainedMessage()
{
}

public TitleGainedMessage(ushort titleId)
        {
            this.titleId = titleId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(titleId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

titleId = reader.ReadVarUhShort();
            if (titleId < 0)
                throw new Exception("Forbidden value on titleId = " + titleId + ", it doesn't respect the following condition : titleId < 0");
            

}


}


}