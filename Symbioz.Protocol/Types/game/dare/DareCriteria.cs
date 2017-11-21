


















// Generated on 04/27/2016 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class DareCriteria
{

public const short Id = 501;
public virtual short TypeId
{
    get { return Id; }
}

public sbyte type;
        public int[] @param;
        

public DareCriteria()
{
}

public DareCriteria(sbyte type, int[] @param)
        {
            this.type = type;
            this.@param = @param;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(type);
            writer.WriteUShort((ushort)@param.Length);
            foreach (var entry in @param)
            {
                 writer.WriteInt(entry);
            }
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

type = reader.ReadSByte();
            if (type < 0)
                throw new Exception("Forbidden value on type = " + type + ", it doesn't respect the following condition : type < 0");
            var limit = reader.ReadUShort();
            @param = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 @param[i] = reader.ReadInt();
            }
            

}


}


}