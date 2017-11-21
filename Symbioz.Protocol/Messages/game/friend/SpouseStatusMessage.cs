


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class SpouseStatusMessage : Message
{

public const ushort Id = 6265;
public override ushort MessageId
{
    get { return Id; }
}

public bool hasSpouse;
        

public SpouseStatusMessage()
{
}

public SpouseStatusMessage(bool hasSpouse)
        {
            this.hasSpouse = hasSpouse;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(hasSpouse);
            

}

public override void Deserialize(ICustomDataInput reader)
{

hasSpouse = reader.ReadBoolean();
            

}


}


}