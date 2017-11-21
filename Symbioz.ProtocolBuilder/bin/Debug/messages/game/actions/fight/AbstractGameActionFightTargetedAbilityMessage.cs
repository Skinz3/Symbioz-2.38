


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AbstractGameActionFightTargetedAbilityMessage : AbstractGameActionMessage
{

public const ushort Id = 6118;
public override ushort MessageId
{
    get { return Id; }
}

public bool silentCast;
        public bool verboseCast;
        public double targetId;
        public short destinationCellId;
        public sbyte critical;
        

public AbstractGameActionFightTargetedAbilityMessage()
{
}

public AbstractGameActionFightTargetedAbilityMessage(ushort actionId, double sourceId, bool silentCast, bool verboseCast, double targetId, short destinationCellId, sbyte critical)
         : base(actionId, sourceId)
        {
            this.silentCast = silentCast;
            this.verboseCast = verboseCast;
            this.targetId = targetId;
            this.destinationCellId = destinationCellId;
            this.critical = critical;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            byte flag1 = 0;
            flag1 = BooleanByteWrapper.SetFlag(flag1, 0, silentCast);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 1, verboseCast);
            writer.WriteByte(flag1);
            writer.WriteDouble(targetId);
            writer.WriteShort(destinationCellId);
            writer.WriteSByte(critical);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            byte flag1 = reader.ReadByte();
            silentCast = BooleanByteWrapper.GetFlag(flag1, 0);
            verboseCast = BooleanByteWrapper.GetFlag(flag1, 1);
            targetId = reader.ReadDouble();
            if (targetId < -9007199254740990 || targetId > 9007199254740990)
                throw new Exception("Forbidden value on targetId = " + targetId + ", it doesn't respect the following condition : targetId < -9007199254740990 || targetId > 9007199254740990");
            destinationCellId = reader.ReadShort();
            if (destinationCellId < -1 || destinationCellId > 559)
                throw new Exception("Forbidden value on destinationCellId = " + destinationCellId + ", it doesn't respect the following condition : destinationCellId < -1 || destinationCellId > 559");
            critical = reader.ReadSByte();
            if (critical < 0)
                throw new Exception("Forbidden value on critical = " + critical + ", it doesn't respect the following condition : critical < 0");
            

}


}


}