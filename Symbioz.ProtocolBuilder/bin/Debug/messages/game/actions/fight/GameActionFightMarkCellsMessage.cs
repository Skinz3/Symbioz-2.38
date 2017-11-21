


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameActionFightMarkCellsMessage : AbstractGameActionMessage
{

public const ushort Id = 5540;
public override ushort MessageId
{
    get { return Id; }
}

public Types.GameActionMark mark;
        

public GameActionFightMarkCellsMessage()
{
}

public GameActionFightMarkCellsMessage(ushort actionId, double sourceId, Types.GameActionMark mark)
         : base(actionId, sourceId)
        {
            this.mark = mark;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            mark.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            mark = new Types.GameActionMark();
            mark.Deserialize(reader);
            

}


}


}