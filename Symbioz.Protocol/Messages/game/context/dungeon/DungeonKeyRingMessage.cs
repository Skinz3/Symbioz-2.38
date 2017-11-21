


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DungeonKeyRingMessage : Message
{

public const ushort Id = 6299;
public override ushort MessageId
{
    get { return Id; }
}

public ushort[] availables;
        public ushort[] unavailables;
        

public DungeonKeyRingMessage()
{
}

public DungeonKeyRingMessage(ushort[] availables, ushort[] unavailables)
        {
            this.availables = availables;
            this.unavailables = unavailables;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)availables.Length);
            foreach (var entry in availables)
            {
                 writer.WriteVarUhShort(entry);
            }
            writer.WriteUShort((ushort)unavailables.Length);
            foreach (var entry in unavailables)
            {
                 writer.WriteVarUhShort(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            availables = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 availables[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            unavailables = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 unavailables[i] = reader.ReadVarUhShort();
            }
            

}


}


}