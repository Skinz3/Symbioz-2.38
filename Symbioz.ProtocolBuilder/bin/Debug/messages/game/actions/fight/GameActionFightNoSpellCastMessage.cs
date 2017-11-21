


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameActionFightNoSpellCastMessage : Message
{

public const ushort Id = 6132;
public override ushort MessageId
{
    get { return Id; }
}

public uint spellLevelId;
        

public GameActionFightNoSpellCastMessage()
{
}

public GameActionFightNoSpellCastMessage(uint spellLevelId)
        {
            this.spellLevelId = spellLevelId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(spellLevelId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

spellLevelId = reader.ReadVarUhInt();
            if (spellLevelId < 0)
                throw new Exception("Forbidden value on spellLevelId = " + spellLevelId + ", it doesn't respect the following condition : spellLevelId < 0");
            

}


}


}