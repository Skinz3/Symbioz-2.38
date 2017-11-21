


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DareCreatedMessage : Message
{

public const ushort Id = 6668;
public override ushort MessageId
{
    get { return Id; }
}

public Types.DareInformations dareInfos;
        public bool needNotifications;
        

public DareCreatedMessage()
{
}

public DareCreatedMessage(Types.DareInformations dareInfos, bool needNotifications)
        {
            this.dareInfos = dareInfos;
            this.needNotifications = needNotifications;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

dareInfos.Serialize(writer);
            writer.WriteBoolean(needNotifications);
            

}

public override void Deserialize(ICustomDataInput reader)
{

dareInfos = new Types.DareInformations();
            dareInfos.Deserialize(reader);
            needNotifications = reader.ReadBoolean();
            

}


}


}