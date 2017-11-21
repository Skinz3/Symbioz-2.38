


















// Generated on 04/27/2016 01:13:13
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameRolePlayMerchantInformations : GameRolePlayNamedActorInformations
{

public const short Id = 129;
public override short TypeId
{
    get { return Id; }
}

public sbyte sellType;
        public HumanOption[] options;
        

public GameRolePlayMerchantInformations()
{
}

public GameRolePlayMerchantInformations(double contextualId, Types.EntityLook look, EntityDispositionInformations disposition, string name, sbyte sellType, HumanOption[] options)
         : base(contextualId, look, disposition, name)
        {
            this.sellType = sellType;
            this.options = options;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(sellType);
            writer.WriteUShort((ushort)options.Length);
            foreach (var entry in options)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            sellType = reader.ReadSByte();
            if (sellType < 0)
                throw new Exception("Forbidden value on sellType = " + sellType + ", it doesn't respect the following condition : sellType < 0");
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