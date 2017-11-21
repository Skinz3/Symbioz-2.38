


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class MountInformationsForPaddock
{

public const short Id = 184;
public virtual short TypeId
{
    get { return Id; }
}

public sbyte modelId;
        public string name;
        public string ownerName;
        

public MountInformationsForPaddock()
{
}

public MountInformationsForPaddock(sbyte modelId, string name, string ownerName)
        {
            this.modelId = modelId;
            this.name = name;
            this.ownerName = ownerName;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(modelId);
            writer.WriteUTF(name);
            writer.WriteUTF(ownerName);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

modelId = reader.ReadSByte();
            if (modelId < 0)
                throw new Exception("Forbidden value on modelId = " + modelId + ", it doesn't respect the following condition : modelId < 0");
            name = reader.ReadUTF();
            ownerName = reader.ReadUTF();
            

}


}


}