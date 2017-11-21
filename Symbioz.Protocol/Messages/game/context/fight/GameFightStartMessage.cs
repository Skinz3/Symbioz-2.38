


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameFightStartMessage : Message
{

public const ushort Id = 712;
public override ushort MessageId
{
    get { return Id; }
}

public Types.Idol[] idols;
        

public GameFightStartMessage()
{
}

public GameFightStartMessage(Types.Idol[] idols)
        {
            this.idols = idols;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)idols.Length);
            foreach (var entry in idols)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            idols = new Types.Idol[limit];
            for (int i = 0; i < limit; i++)
            {
                 idols[i] = new Types.Idol();
                 idols[i].Deserialize(reader);
            }
            

}


}


}