


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class CharacterSelectedSuccessMessage : Message
{

public const ushort Id = 153;
public override ushort MessageId
{
    get { return Id; }
}

public Types.CharacterBaseInformations infos;
        public bool isCollectingStats;
        

public CharacterSelectedSuccessMessage()
{
}

public CharacterSelectedSuccessMessage(Types.CharacterBaseInformations infos, bool isCollectingStats)
        {
            this.infos = infos;
            this.isCollectingStats = isCollectingStats;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

infos.Serialize(writer);
            writer.WriteBoolean(isCollectingStats);
            

}

public override void Deserialize(ICustomDataInput reader)
{

infos = new Types.CharacterBaseInformations();
            infos.Deserialize(reader);
            isCollectingStats = reader.ReadBoolean();
            

}


}


}