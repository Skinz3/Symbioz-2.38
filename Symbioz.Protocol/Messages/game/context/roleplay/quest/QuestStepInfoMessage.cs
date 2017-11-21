


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class QuestStepInfoMessage : Message
{

public const ushort Id = 5625;
public override ushort MessageId
{
    get { return Id; }
}

public Types.QuestActiveInformations infos;
        

public QuestStepInfoMessage()
{
}

public QuestStepInfoMessage(Types.QuestActiveInformations infos)
        {
            this.infos = infos;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteShort(infos.TypeId);
            infos.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

infos = Types.ProtocolTypeManager.GetInstance<Types.QuestActiveInformations>(reader.ReadShort());
            infos.Deserialize(reader);
            

}


}


}