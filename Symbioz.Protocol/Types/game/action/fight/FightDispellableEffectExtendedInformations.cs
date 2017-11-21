


















// Generated on 04/27/2016 01:13:08
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class FightDispellableEffectExtendedInformations
{

public const short Id = 208;
public virtual short TypeId
{
    get { return Id; }
}

public ushort actionId;
        public double sourceId;
        public Types.AbstractFightDispellableEffect effect;
        

public FightDispellableEffectExtendedInformations()
{
}

public FightDispellableEffectExtendedInformations(ushort actionId, double sourceId, Types.AbstractFightDispellableEffect effect)
        {
            this.actionId = actionId;
            this.sourceId = sourceId;
            this.effect = effect;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(actionId);
            writer.WriteDouble(sourceId);
            writer.WriteShort(effect.TypeId);
            effect.Serialize(writer);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

actionId = reader.ReadVarUhShort();
            if (actionId < 0)
                throw new Exception("Forbidden value on actionId = " + actionId + ", it doesn't respect the following condition : actionId < 0");
            sourceId = reader.ReadDouble();
            if (sourceId < -9007199254740990 || sourceId > 9007199254740990)
                throw new Exception("Forbidden value on sourceId = " + sourceId + ", it doesn't respect the following condition : sourceId < -9007199254740990 || sourceId > 9007199254740990");
            effect = Types.ProtocolTypeManager.GetInstance<Types.AbstractFightDispellableEffect>(reader.ReadShort());
            effect.Deserialize(reader);
            

}


}


}