


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AggregateStatWithDataMessage : AggregateStatMessage
{

public const ushort Id = 6662;
public override ushort MessageId
{
    get { return Id; }
}

public Types.StatisticData[] datas;
        

public AggregateStatWithDataMessage()
{
}

public AggregateStatWithDataMessage(ushort statId, Types.StatisticData[] datas)
         : base(statId)
        {
            this.datas = datas;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)datas.Length);
            foreach (var entry in datas)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            datas = new Types.StatisticData[limit];
            for (int i = 0; i < limit; i++)
            {
                 datas[i] = Types.ProtocolTypeManager.GetInstance<Types.StatisticData>(reader.ReadShort());
                 datas[i].Deserialize(reader);
            }
            

}


}


}