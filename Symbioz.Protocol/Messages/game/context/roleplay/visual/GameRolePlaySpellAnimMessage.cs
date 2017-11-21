


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlaySpellAnimMessage : Message
{

public const ushort Id = 6114;
public override ushort MessageId
{
    get { return Id; }
}

public ulong casterId;
        public ushort targetCellId;
        public ushort spellId;
        public sbyte spellLevel;
        

public GameRolePlaySpellAnimMessage()
{
}

public GameRolePlaySpellAnimMessage(ulong casterId, ushort targetCellId, ushort spellId, sbyte spellLevel)
        {
            this.casterId = casterId;
            this.targetCellId = targetCellId;
            this.spellId = spellId;
            this.spellLevel = spellLevel;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(casterId);
            writer.WriteVarUhShort(targetCellId);
            writer.WriteVarUhShort(spellId);
            writer.WriteSByte(spellLevel);
            

}

public override void Deserialize(ICustomDataInput reader)
{

casterId = reader.ReadVarUhLong();
            if (casterId < 0 || casterId > 9007199254740990)
                throw new Exception("Forbidden value on casterId = " + casterId + ", it doesn't respect the following condition : casterId < 0 || casterId > 9007199254740990");
            targetCellId = reader.ReadVarUhShort();
            if (targetCellId < 0 || targetCellId > 559)
                throw new Exception("Forbidden value on targetCellId = " + targetCellId + ", it doesn't respect the following condition : targetCellId < 0 || targetCellId > 559");
            spellId = reader.ReadVarUhShort();
            if (spellId < 0)
                throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
            spellLevel = reader.ReadSByte();
            if (spellLevel < 1 || spellLevel > 6)
                throw new Exception("Forbidden value on spellLevel = " + spellLevel + ", it doesn't respect the following condition : spellLevel < 1 || spellLevel > 6");
            

}


}


}