


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class IndexedEntityLook
{

public const short Id = 405;
public virtual short TypeId
{
    get { return Id; }
}

public EntityLook look;
        public sbyte index;
        

public IndexedEntityLook()
{
}

public IndexedEntityLook(EntityLook look, sbyte index)
        {
            this.look = look;
            this.index = index;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

look.Serialize(writer);
            writer.WriteSByte(index);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

look = new EntityLook();
            look.Deserialize(reader);
            index = reader.ReadSByte();
            if (index < 0)
                throw new Exception("Forbidden value on index = " + index + ", it doesn't respect the following condition : index < 0");
            

}


}


}