


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameFightTurnListMessage : Message
{

public const ushort Id = 713;
public override ushort MessageId
{
    get { return Id; }
}

public double[] ids;
        public double[] deadsIds;
        

public GameFightTurnListMessage()
{
}

public GameFightTurnListMessage(double[] ids, double[] deadsIds)
        {
            this.ids = ids;
            this.deadsIds = deadsIds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)ids.Length);
            foreach (var entry in ids)
            {
                 writer.WriteDouble(entry);
            }
            writer.WriteUShort((ushort)deadsIds.Length);
            foreach (var entry in deadsIds)
            {
                 writer.WriteDouble(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            ids = new double[limit];
            for (int i = 0; i < limit; i++)
            {
                 ids[i] = reader.ReadDouble();
            }
            limit = reader.ReadUShort();
            deadsIds = new double[limit];
            for (int i = 0; i < limit; i++)
            {
                 deadsIds[i] = reader.ReadDouble();
            }
            

}


}


}