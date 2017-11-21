


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameActionFightSpellCastMessage : AbstractGameActionFightTargetedAbilityMessage
{

public const ushort Id = 1010;
public override ushort MessageId
{
    get { return Id; }
}

public ushort spellId;
        public sbyte spellLevel;
        public short[] portalsIds;
        

public GameActionFightSpellCastMessage()
{
}

public GameActionFightSpellCastMessage(ushort actionId, double sourceId, bool silentCast, bool verboseCast, double targetId, short destinationCellId, sbyte critical, ushort spellId, sbyte spellLevel, short[] portalsIds)
         : base(actionId, sourceId, silentCast, verboseCast, targetId, destinationCellId, critical)
        {
            this.spellId = spellId;
            this.spellLevel = spellLevel;
            this.portalsIds = portalsIds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhShort(spellId);
            writer.WriteSByte(spellLevel);
            writer.WriteUShort((ushort)portalsIds.Length);
            foreach (var entry in portalsIds)
            {
                 writer.WriteShort(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            spellId = reader.ReadVarUhShort();
            if (spellId < 0)
                throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
            spellLevel = reader.ReadSByte();
            if (spellLevel < 1 || spellLevel > 6)
                throw new Exception("Forbidden value on spellLevel = " + spellLevel + ", it doesn't respect the following condition : spellLevel < 1 || spellLevel > 6");
            var limit = reader.ReadUShort();
            portalsIds = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 portalsIds[i] = reader.ReadShort();
            }
            

}


}


}