


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class KrosmasterInventoryMessage : Message
{

public const ushort Id = 6350;
public override ushort MessageId
{
    get { return Id; }
}

public Types.KrosmasterFigure[] figures;
        

public KrosmasterInventoryMessage()
{
}

public KrosmasterInventoryMessage(Types.KrosmasterFigure[] figures)
        {
            this.figures = figures;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)figures.Length);
            foreach (var entry in figures)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            figures = new Types.KrosmasterFigure[limit];
            for (int i = 0; i < limit; i++)
            {
                 figures[i] = new Types.KrosmasterFigure();
                 figures[i].Deserialize(reader);
            }
            

}


}


}