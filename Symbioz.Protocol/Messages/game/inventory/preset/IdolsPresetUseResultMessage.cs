


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class IdolsPresetUseResultMessage : Message
{

public const ushort Id = 6614;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte presetId;
        public sbyte code;
        public ushort[] missingIdols;
        

public IdolsPresetUseResultMessage()
{
}

public IdolsPresetUseResultMessage(sbyte presetId, sbyte code, ushort[] missingIdols)
        {
            this.presetId = presetId;
            this.code = code;
            this.missingIdols = missingIdols;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(presetId);
            writer.WriteSByte(code);
            writer.WriteUShort((ushort)missingIdols.Length);
            foreach (var entry in missingIdols)
            {
                 writer.WriteVarUhShort(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

presetId = reader.ReadSByte();
            if (presetId < 0)
                throw new Exception("Forbidden value on presetId = " + presetId + ", it doesn't respect the following condition : presetId < 0");
            code = reader.ReadSByte();
            if (code < 0)
                throw new Exception("Forbidden value on code = " + code + ", it doesn't respect the following condition : code < 0");
            var limit = reader.ReadUShort();
            missingIdols = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 missingIdols[i] = reader.ReadVarUhShort();
            }
            

}


}


}