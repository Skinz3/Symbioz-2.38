


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class MountSetXpRatioRequestMessage : Message
{

public const ushort Id = 5989;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte xpRatio;
        

public MountSetXpRatioRequestMessage()
{
}

public MountSetXpRatioRequestMessage(sbyte xpRatio)
        {
            this.xpRatio = xpRatio;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(xpRatio);
            

}

public override void Deserialize(ICustomDataInput reader)
{

xpRatio = reader.ReadSByte();
            if (xpRatio < 0)
                throw new Exception("Forbidden value on xpRatio = " + xpRatio + ", it doesn't respect the following condition : xpRatio < 0");
            

}


}


}