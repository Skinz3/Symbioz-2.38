


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ArenaFighterLeaveMessage : Message
{

public const ushort Id = 6700;
public override ushort MessageId
{
    get { return Id; }
}

public Types.CharacterBasicMinimalInformations leaver;
        

public ArenaFighterLeaveMessage()
{
}

public ArenaFighterLeaveMessage(Types.CharacterBasicMinimalInformations leaver)
        {
            this.leaver = leaver;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

leaver.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

leaver = new Types.CharacterBasicMinimalInformations();
            leaver.Deserialize(reader);
            

}


}


}