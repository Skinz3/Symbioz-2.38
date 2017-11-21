


















// Generated on 04/27/2016 01:13:10
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameContextActorInformations
{

public const short Id = 150;
public virtual short TypeId
{
    get { return Id; }
}

public double contextualId;
        public Types.EntityLook look;
        public EntityDispositionInformations disposition;
        

public GameContextActorInformations()
{
}

public GameContextActorInformations(double contextualId, Types.EntityLook look, EntityDispositionInformations disposition)
        {
            this.contextualId = contextualId;
            this.look = look;
            this.disposition = disposition;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(contextualId);
            look.Serialize(writer);
            writer.WriteShort(disposition.TypeId);
            disposition.Serialize(writer);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

contextualId = reader.ReadDouble();
            if (contextualId < -9007199254740990 || contextualId > 9007199254740990)
                throw new Exception("Forbidden value on contextualId = " + contextualId + ", it doesn't respect the following condition : contextualId < -9007199254740990 || contextualId > 9007199254740990");
            look = new Types.EntityLook();
            look.Deserialize(reader);
            disposition = Types.ProtocolTypeManager.GetInstance<EntityDispositionInformations>(reader.ReadShort());
            disposition.Deserialize(reader);
            

}


}


}