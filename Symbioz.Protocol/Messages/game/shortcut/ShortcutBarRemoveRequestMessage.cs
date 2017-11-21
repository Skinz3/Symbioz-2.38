


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ShortcutBarRemoveRequestMessage : Message
{

public const ushort Id = 6228;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte barType;
        public sbyte slot;
        

public ShortcutBarRemoveRequestMessage()
{
}

public ShortcutBarRemoveRequestMessage(sbyte barType, sbyte slot)
        {
            this.barType = barType;
            this.slot = slot;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(barType);
            writer.WriteSByte(slot);
            

}

public override void Deserialize(ICustomDataInput reader)
{

barType = reader.ReadSByte();
            if (barType < 0)
                throw new Exception("Forbidden value on barType = " + barType + ", it doesn't respect the following condition : barType < 0");
            slot = reader.ReadSByte();
            if (slot < 0 || slot > 99)
                throw new Exception("Forbidden value on slot = " + slot + ", it doesn't respect the following condition : slot < 0 || slot > 99");
            

}


}


}