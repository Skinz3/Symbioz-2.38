


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class IdolsPreset
{

public const short Id = 491;
public virtual short TypeId
{
    get { return Id; }
}

public sbyte presetId;
        public sbyte symbolId;
        public ushort[] idolId;
        

public IdolsPreset()
{
}

public IdolsPreset(sbyte presetId, sbyte symbolId, ushort[] idolId)
        {
            this.presetId = presetId;
            this.symbolId = symbolId;
            this.idolId = idolId;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(presetId);
            writer.WriteSByte(symbolId);
            writer.WriteUShort((ushort)idolId.Length);
            foreach (var entry in idolId)
            {
                 writer.WriteVarUhShort(entry);
            }
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

presetId = reader.ReadSByte();
            if (presetId < 0)
                throw new Exception("Forbidden value on presetId = " + presetId + ", it doesn't respect the following condition : presetId < 0");
            symbolId = reader.ReadSByte();
            if (symbolId < 0)
                throw new Exception("Forbidden value on symbolId = " + symbolId + ", it doesn't respect the following condition : symbolId < 0");
            var limit = reader.ReadUShort();
            idolId = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 idolId[i] = reader.ReadVarUhShort();
            }
            

}


}


}