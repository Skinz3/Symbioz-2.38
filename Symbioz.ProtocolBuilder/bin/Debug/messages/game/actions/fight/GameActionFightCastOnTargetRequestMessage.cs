


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameActionFightCastOnTargetRequestMessage : Message
{

public const ushort Id = 6330;
public override ushort MessageId
{
    get { return Id; }
}

public ushort spellId;
        public double targetId;
        

public GameActionFightCastOnTargetRequestMessage()
{
}

public GameActionFightCastOnTargetRequestMessage(ushort spellId, double targetId)
        {
            this.spellId = spellId;
            this.targetId = targetId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(spellId);
            writer.WriteDouble(targetId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

spellId = reader.ReadVarUhShort();
            if (spellId < 0)
                throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
            targetId = reader.ReadDouble();
            if (targetId < -9007199254740990 || targetId > 9007199254740990)
                throw new Exception("Forbidden value on targetId = " + targetId + ", it doesn't respect the following condition : targetId < -9007199254740990 || targetId > 9007199254740990");
            

}


}


}