


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class IdolFightPreparationUpdateMessage : Message
{

public const ushort Id = 6586;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte idolSource;
        public Types.Idol[] idols;
        

public IdolFightPreparationUpdateMessage()
{
}

public IdolFightPreparationUpdateMessage(sbyte idolSource, Types.Idol[] idols)
        {
            this.idolSource = idolSource;
            this.idols = idols;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(idolSource);
            writer.WriteUShort((ushort)idols.Length);
            foreach (var entry in idols)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

idolSource = reader.ReadSByte();
            if (idolSource < 0)
                throw new Exception("Forbidden value on idolSource = " + idolSource + ", it doesn't respect the following condition : idolSource < 0");
            var limit = reader.ReadUShort();
            idols = new Types.Idol[limit];
            for (int i = 0; i < limit; i++)
            {
                 idols[i] = ProtocolTypeManager.GetInstance<Types.Idol>(reader.ReadShort());
                 idols[i].Deserialize(reader);
            }
            

}


}


}