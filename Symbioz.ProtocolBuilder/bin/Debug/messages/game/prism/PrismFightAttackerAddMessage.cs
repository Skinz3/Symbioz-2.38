


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PrismFightAttackerAddMessage : Message
{

public const ushort Id = 5893;
public override ushort MessageId
{
    get { return Id; }
}

public ushort subAreaId;
        public ushort fightId;
        public Types.CharacterMinimalPlusLookInformations attacker;
        

public PrismFightAttackerAddMessage()
{
}

public PrismFightAttackerAddMessage(ushort subAreaId, ushort fightId, Types.CharacterMinimalPlusLookInformations attacker)
        {
            this.subAreaId = subAreaId;
            this.fightId = fightId;
            this.attacker = attacker;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(subAreaId);
            writer.WriteVarUhShort(fightId);
            writer.WriteShort(attacker.TypeId);
            attacker.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            fightId = reader.ReadVarUhShort();
            if (fightId < 0)
                throw new Exception("Forbidden value on fightId = " + fightId + ", it doesn't respect the following condition : fightId < 0");
            attacker = ProtocolTypeManager.GetInstance<Types.CharacterMinimalPlusLookInformations>(reader.ReadShort());
            attacker.Deserialize(reader);
            

}


}


}