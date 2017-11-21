


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class SpellListMessage : Message
{

public const ushort Id = 1200;
public override ushort MessageId
{
    get { return Id; }
}

public bool spellPrevisualization;
        public Types.SpellItem[] spells;
        

public SpellListMessage()
{
}

public SpellListMessage(bool spellPrevisualization, Types.SpellItem[] spells)
        {
            this.spellPrevisualization = spellPrevisualization;
            this.spells = spells;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(spellPrevisualization);
            writer.WriteUShort((ushort)spells.Length);
            foreach (var entry in spells)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

spellPrevisualization = reader.ReadBoolean();
            var limit = reader.ReadUShort();
            spells = new Types.SpellItem[limit];
            for (int i = 0; i < limit; i++)
            {
                 spells[i] = new Types.SpellItem();
                 spells[i].Deserialize(reader);
            }
            

}


}


}