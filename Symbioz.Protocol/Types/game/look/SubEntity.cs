


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class SubEntity
{

public const short Id = 54;
public virtual short TypeId
{
    get { return Id; }
}

public sbyte bindingPointCategory;
        public sbyte bindingPointIndex;
        public EntityLook subEntityLook;
        

public SubEntity()
{
}

public SubEntity(sbyte bindingPointCategory, sbyte bindingPointIndex, EntityLook subEntityLook)
        {
            this.bindingPointCategory = bindingPointCategory;
            this.bindingPointIndex = bindingPointIndex;
            this.subEntityLook = subEntityLook;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(bindingPointCategory);
            writer.WriteSByte(bindingPointIndex);
            subEntityLook.Serialize(writer);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

bindingPointCategory = reader.ReadSByte();
            if (bindingPointCategory < 0)
                throw new Exception("Forbidden value on bindingPointCategory = " + bindingPointCategory + ", it doesn't respect the following condition : bindingPointCategory < 0");
            bindingPointIndex = reader.ReadSByte();
            if (bindingPointIndex < 0)
                throw new Exception("Forbidden value on bindingPointIndex = " + bindingPointIndex + ", it doesn't respect the following condition : bindingPointIndex < 0");
            subEntityLook = new EntityLook();
            subEntityLook.Deserialize(reader);
            

}


}


}