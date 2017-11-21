


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameActionFightTackledMessage : AbstractGameActionMessage
{

public const ushort Id = 1004;
public override ushort MessageId
{
    get { return Id; }
}

public double[] tacklersIds;
        

public GameActionFightTackledMessage()
{
}

public GameActionFightTackledMessage(ushort actionId, double sourceId, double[] tacklersIds)
         : base(actionId, sourceId)
        {
            this.tacklersIds = tacklersIds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)tacklersIds.Length);
            foreach (var entry in tacklersIds)
            {
                 writer.WriteDouble(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            tacklersIds = new double[limit];
            for (int i = 0; i < limit; i++)
            {
                 tacklersIds[i] = reader.ReadDouble();
            }
            

}


}


}