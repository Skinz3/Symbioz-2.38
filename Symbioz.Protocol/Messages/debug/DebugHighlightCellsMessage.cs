


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DebugHighlightCellsMessage : Message
{

public const ushort Id = 2001;
public override ushort MessageId
{
    get { return Id; }
}

public int color;
        public ushort[] cells;
        

public DebugHighlightCellsMessage()
{
}

public DebugHighlightCellsMessage(int color, ushort[] cells)
        {
            this.color = color;
            this.cells = cells;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(color);
            writer.WriteUShort((ushort)cells.Length);
            foreach (var entry in cells)
            {
                 writer.WriteVarUhShort(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

color = reader.ReadInt();
            var limit = reader.ReadUShort();
            cells = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 cells[i] = reader.ReadVarUhShort();
            }
            

}


}


}