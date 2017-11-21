


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PrismFightDefenderAddMessage : Message
{

public const ushort Id = 5895;
public override ushort MessageId
{
    get { return Id; }
}

public ushort subAreaId;
        public ushort fightId;
        public Types.CharacterMinimalPlusLookInformations defender;
        

public PrismFightDefenderAddMessage()
{
}

public PrismFightDefenderAddMessage(ushort subAreaId, ushort fightId, Types.CharacterMinimalPlusLookInformations defender)
        {
            this.subAreaId = subAreaId;
            this.fightId = fightId;
            this.defender = defender;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(subAreaId);
            writer.WriteVarUhShort(fightId);
            writer.WriteShort(defender.TypeId);
            defender.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            fightId = reader.ReadVarUhShort();
            if (fightId < 0)
                throw new Exception("Forbidden value on fightId = " + fightId + ", it doesn't respect the following condition : fightId < 0");
            defender = ProtocolTypeManager.GetInstance<Types.CharacterMinimalPlusLookInformations>(reader.ReadShort());
            defender.Deserialize(reader);
            

}


}


}