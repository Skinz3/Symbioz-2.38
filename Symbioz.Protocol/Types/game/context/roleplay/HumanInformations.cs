


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class HumanInformations
{

public const short Id = 157;
public virtual short TypeId
{
    get { return Id; }
}

public Types.ActorRestrictionsInformations restrictions;
        public bool sex;
        public HumanOption[] options;
        

public HumanInformations()
{
}

public HumanInformations(Types.ActorRestrictionsInformations restrictions, bool sex, HumanOption[] options)
        {
            this.restrictions = restrictions;
            this.sex = sex;
            this.options = options;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

restrictions.Serialize(writer);
            writer.WriteBoolean(sex);
            writer.WriteUShort((ushort)options.Length);
            foreach (var entry in options)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

restrictions = new Types.ActorRestrictionsInformations();
            restrictions.Deserialize(reader);
            sex = reader.ReadBoolean();
            var limit = reader.ReadUShort();
            options = new HumanOption[limit];
            for (int i = 0; i < limit; i++)
            {
                 options[i] = Types.ProtocolTypeManager.GetInstance<HumanOption>(reader.ReadShort());
                 options[i].Deserialize(reader);
            }
            

}


}


}